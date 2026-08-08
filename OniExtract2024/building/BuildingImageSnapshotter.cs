// Camera-snapshot rendering adapted from the GPL-2.0 project Sgt_Imalas-Oni-Mods
// (AnimExportTool/AETE_KbacSnapShotter.cs), itself courtesy of Aki
// (https://github.com/aki-art/ONI-Mods). Adapted for OniExtract2024 to render
// building icons at 200 px/cell and trim to the opaque bounding box.
//
// Difference from ConnectionSpriteSnapshotter: one shot per building (build-complete
// idle state), output cropped to the tightest opaque bbox rather than a cell-centred
// square, and written to ui_image/ (same path as the main-menu atlas pass so the
// hi-res file lands exactly on top of the low-res one).

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HarmonyLib;
using UnityEngine;

namespace OniExtract2024.building
{
    /// <summary>
    /// Attached to a temporary off-screen building. Snapshots the live kanim at
    /// 200 px/cell, trims to the opaque bounding box, writes ui_image/{prefabId}.png,
    /// then destroys the host GameObject. Driven by ExportBuildingImages' coroutine.
    /// </summary>
    public class BuildingImageSnapshotter : KMonoBehaviour
    {
        private const float PixelsPerCell = 200f;
        // 2 cells of headroom on each side at 200 px/cell. Buildings like SteamTurbine2
        // have kanim parts that extend ~2 cells below the cell footprint; 1 cell was not
        // enough. Bbox crop removes the empty margin so output size is unaffected.
        private const float PaddingPx = 400f;
        private static readonly int DrawLayer = 30;

        // Where in the chosen animation to snapshot. Frame 0 of a working/generating loop
        // is the "starting / retracting" pose (arm pulled in, nothing emitted) — the
        // "shut down" look. Mid-loop the building is fully deployed and emitting, which
        // reads as "active". 0.5 = halfway through the current animation's timeline.
        private const float PoseFramePercent = 0.5f;

        // Substrings that mark an animation as a non-active state — never snapshot these.
        private static readonly string[] InactiveMarkers =
        {
            "off", "broken", "error", "dead", "disabled", "outofnetwork",
            "no_power", "nopower", "unpowered", "closed", "empty", "ui",
        };

        // Fallback probe order if the build's anim list can't be enumerated. Most-active
        // first; the first one the controller actually has wins. Covers the common ONI
        // naming so we still improve buildings even when group enumeration returns null.
        private static readonly string[] ActiveAnimFallback =
        {
            "working_loop", "generating_loop", "channeling_loop", "working", "generating",
            "channeling", "dispensing", "on_loop", "on", "idle_loop", "idle",
        };

        private Camera snapshotCamera;
        private RenderTexture targetTexture;

        // Render geometry captured in InitCamera, needed afterwards to express the crop
        // as a footprint-relative rectangle (uiImageRect). The camera centres the footprint
        // on the texture centre, so these plus the crop bbox fully locate the art.
        private int texW;
        private int texH;
        private int cellW;
        private int cellH;

        // Called by ExportBuildingImages after AddOrGet. The caller then waits on
        // IsNullOrDestroyed — no coroutine handle needed from outside. The optional
        // rects map collects each building's uiImageRect, keyed by prefab tag name
        // (== building.json `name`), so ExportBuildingImages can patch building.json.
        public void StartExport(string outputDir, IDictionary<string, UiImageRect> rects = null)
        {
            StartCoroutine(DoExport(outputDir, rects));
        }

        private IEnumerator DoExport(string outputDir, IDictionary<string, UiImageRect> rects)
        {
            yield return new WaitForSecondsRealtime(0.1f);

            var kpid = GetComponent<KPrefabID>();
            var kbac = GetComponent<KBatchedAnimController>();

            if (kpid != null && kbac != null)
            {
                // Pose the building in its most "active" state at a representative frame
                // rather than the drab idle/off pose it auto-spawns into. Applied right
                // before SnapShot with no intervening game tick, so a building's own state
                // machine can't revert it before we read pixels. Never use "ui" — it
                // renders at atlas/icon scale, not live-kanim scale (see findings doc).
                PoseActive(kbac);

                string fileName = ExportUISprite.GetFormatedUIImageFileName(kpid);
                Texture2D raw = SnapShot();
                if (raw != null)
                {
                    Texture2D cropped = TrimToOpaqueBBox(raw, out int minX, out int minY, out int cw, out int ch);
                    Destroy(raw);
                    if (cropped != null)
                    {
                        Directory.CreateDirectory(outputDir);
                        string filePath = Path.Combine(outputDir, fileName + ".png");
                        File.WriteAllBytes(filePath, cropped.EncodeToPNG());
                        Destroy(cropped);
                        Debug.Log("OniExtract: building image -> " + filePath);

                        if (rects != null)
                            rects[kpid.PrefabTag.Name] = ComputeRect(minX, minY, cw, ch);
                    }
                }
            }

            Util.KDestroyGameObject(gameObject);
        }

        private Texture2D SnapShot()
        {
            SelectTool.Instance.Select(null);
            if (snapshotCamera != null)
            {
                Destroy(snapshotCamera.gameObject);
                snapshotCamera = null;
            }
            InitCamera();

            KAnimBatchManager.Instance().UpdateActiveArea(new Vector2I(-9999, -9999), new Vector2I(9999, 9999));
            KAnimBatchManager.Instance().UpdateDirty(Time.frameCount);
            CameraController.Instance.baseCamera.enabled = false;
            snapshotCamera.enabled = true;

            RenderTexture previous = RenderTexture.active;
            RenderTexture.active = targetTexture;

            var kbacs = gameObject.GetComponentsInChildren<KBatchedAnimController>()
                .OrderBy(k => k.transform.position.z);
            foreach (var k in kbacs)
                RenderBatch(GetBatch(k), snapshotCamera, DrawLayer);

            snapshotCamera.Render();
            Texture2D tex = new Texture2D(targetTexture.width, targetTexture.height);
            tex.ReadPixels(new Rect(0, 0, targetTexture.width, targetTexture.height), 0, 0);
            tex.Apply();

            RenderTexture.active = previous;
            snapshotCamera.enabled = false;
            Destroy(snapshotCamera.gameObject);
            snapshotCamera = null;
            // Release the RenderTexture immediately — not doing so leaks graphics
            // memory at a rate that crashes the game before the sweep finishes.
            targetTexture.Release();
            Destroy(targetTexture);
            targetTexture = null;
            CameraController.Instance.baseCamera.enabled = true;
            return tex;
        }

        private void InitCamera()
        {
            var building = GetComponent<Building>();
            int widthInt = building != null ? building.Def.WidthInCells : 1;
            int heightInt = building != null ? building.Def.HeightInCells : 1;

            int textureWidth = Mathf.CeilToInt(widthInt * PixelsPerCell + 2 * PaddingPx);
            int textureHeight = Mathf.CeilToInt(heightInt * PixelsPerCell + 2 * PaddingPx);

            // Remember the render geometry so ComputeRect can map the crop back to cells.
            texW = textureWidth;
            texH = textureHeight;
            cellW = widthInt;
            cellH = heightInt;

            targetTexture = new RenderTexture(textureWidth, textureHeight, 24);
            targetTexture.Create();

            var reference = CameraController.Instance.baseCamera;
            snapshotCamera = CameraController.CloneCamera(reference, "OniExtract_BuildingCam");
            snapshotCamera.transform.parent = reference.transform.parent;
            snapshotCamera.targetTexture = targetTexture;
            snapshotCamera.enabled = false;
            snapshotCamera.backgroundColor = Color.clear;
            snapshotCamera.cullingMask = 1 << DrawLayer;

            var pos = transform.GetPosition();
            pos.z = -100f;
            pos.y += heightInt / 2f;
            if (widthInt % 2 == 0)
                pos.x += 0.5f;
            snapshotCamera.transform.position = pos;
            snapshotCamera.orthographicSize = textureHeight / (2f * PixelsPerCell);
            snapshotCamera.aspect = (float)textureWidth / textureHeight;
        }

        // minX/minY are the crop's bottom-left corner in render-texture pixels (y=0 at the
        // bottom, matching GetPixels' bottom-up order), so a building whose art hangs below
        // the footprint yields a small minY → negative uiImageRect.y. cw/ch are the crop size.
        private static Texture2D TrimToOpaqueBBox(Texture2D tex, out int minX, out int minY, out int cw, out int ch)
        {
            int w = tex.width, h = tex.height;
            Color[] px = tex.GetPixels();

            cw = 0; ch = 0;
            if (!ImageCrop.FindOpaqueBBox(px, w, h, out minX, out minY, out int maxX, out int maxY))
                return null;

            cw = maxX - minX + 1;
            ch = maxY - minY + 1;
            Color[] outPx = ImageCrop.CropPixels(px, w, minX, minY, cw, ch);

            Texture2D result = new Texture2D(cw, ch);
            result.SetPixels(outPx);
            result.Apply();
            return result;
        }

        // Express the opaque crop as a footprint-relative rectangle in cells (uiImageRect).
        // Math lives in UiImageRect.FromCrop so it can be unit-tested without loading this
        // KMonoBehaviour (the .NET test host rejects Assembly-CSharp-firstpass types).
        private UiImageRect ComputeRect(int minX, int minY, int cw, int ch)
        {
            return UiImageRect.FromCrop(minX, minY, cw, ch, texW, texH, cellW, cellH, PixelsPerCell);
        }

        // Pose the building's main kanim in the most "active" animation it has, seeked to
        // a representative frame. Picks the best state from the building's real anim list
        // (not a guess), so generators show "generating", arms show their extended
        // "working" pose, etc. — instead of the idle/off frame-0 pose they spawn into.
        //
        // ONLY the root controller is posed. Child controllers (a separate arm/base/part)
        // sit at their own transform offsets and rotations; forcing them to play the main
        // building anim stamps a full extra copy of the building at each child's offset.
        // The snapshot still *renders* every child in its own natural state — we just must
        // not drive them. (This mirrors the original Play("on") which used GetComponent.)
        private void PoseActive(KBatchedAnimController rootKbac)
        {
            string anim = ChooseActiveAnim(rootKbac);
            if (anim == null)
                return;

            rootKbac.Play(anim, KAnim.PlayMode.Paused);
            rootKbac.SetPositionPercent(PoseFramePercent);
        }

        // Returns the highest-scoring active animation the controller actually has, or
        // null to leave the spawned default untouched (no positive candidate). Scans the
        // build's real anim list via the group file rather than probing fixed names, so
        // it adapts to per-building naming (working_loop / generating_loop / channeling…).
        private static string ChooseActiveAnim(KBatchedAnimController kbac)
        {
            List<HashedString> names = GetAnimNames(kbac);
            if (names == null || names.Count == 0)
                return ProbeFallback(kbac);

            string best = null;
            int bestScore = 0; // require a strictly positive score to override the default
            foreach (HashedString hashed in names)
            {
                // ToString resolves back to the source anim name via HashCache; a building
                // whose name didn't register reads as digits and scores 0 (safe fallback).
                string name = hashed.ToString();
                if (string.IsNullOrEmpty(name) || !kbac.HasAnimation(hashed))
                    continue;
                int score = ScoreAnim(name);
                if (score > bestScore)
                {
                    bestScore = score;
                    best = name;
                }
            }
            return best ?? ProbeFallback(kbac);
        }

        // Last resort when the anim list is empty/unscored: try a fixed most-active-first
        // list and take the first animation the controller actually has.
        private static string ProbeFallback(KBatchedAnimController kbac)
        {
            foreach (string name in ActiveAnimFallback)
                if (kbac.HasAnimation(name))
                    return name;
            return null;
        }

        // Higher = more representative of an operating building. Negative = never use.
        private static int ScoreAnim(string name)
        {
            string n = name.ToLowerInvariant();
            foreach (string bad in InactiveMarkers)
                if (n.Contains(bad))
                    return -1;

            int score;
            if (n.Contains("working") || n.Contains("generating") || n.Contains("dispensing")
                || n.Contains("channeling") || n.Contains("emitting") || n.Contains("charged"))
                score = 100;                                   // actively doing its job
            else if (n == "on" || n.StartsWith("on_") || n.EndsWith("_on"))
                score = 50;                                    // powered/idle, lit
            else if (n.Contains("idle"))
                score = 20;                                    // better than off, still calm
            else
                return 0;                                      // unknown state, don't prefer it

            if (n.Contains("loop"))
                score += 10;                                   // the steady operating loop
            if (n.EndsWith("_pre") || n.EndsWith("_pst"))
                score -= 8;                                    // transitions, not the held pose
            return score;
        }

        // The build's animation names, via the global group file keyed by build hash.
        // All public API on stock Assembly-CSharp-firstpass — no reflection needed.
        private static List<HashedString> GetAnimNames(KBatchedAnimController kbac)
        {
            var group = KAnimGroupFile.GetGroup(kbac.GetBuildHash());
            return group != null ? group.animNames : null;
        }

        // --- reflection shims for stock (non-publicized) Assembly-CSharp -------------

        private static KAnimBatch GetBatch(KBatchedAnimController kbac)
        {
            return Traverse.Create(kbac).Field("batch").GetValue<KAnimBatch>();
        }

        private static List<BatchSet> GetActiveBatchSets()
        {
            return Traverse.Create(KAnimBatchManager.Instance()).Field("activeBatchSets").GetValue<List<BatchSet>>();
        }

        private static void RenderBatch(KAnimBatch batch, Camera camera, int layerOverride)
        {
            if (batch == null || batch.size == 0 || !batch.active || batch.materialType == KAnimBatchGroup.MaterialType.UI)
                return;

            BatchSet owningSet = null;
            var sets = GetActiveBatchSets();
            if (sets != null)
            {
                foreach (var set in sets)
                {
                    for (int i = 0; i < set.batchCount; i++)
                    {
                        if (set.GetBatch(i) == batch)
                        {
                            owningSet = set;
                            break;
                        }
                    }
                    if (owningSet != null)
                        break;
                }
            }
            if (owningSet == null)
                return;

            float zNudge = 0.01f / (1 + batch.id % 256);
            Vector3 drawPos = Vector3.zero;
            drawPos.z = batch.position.z + zNudge;
            int layer = layerOverride >= 0 ? layerOverride : batch.layer;

            Graphics.DrawMesh(owningSet.group.mesh, drawPos, Quaternion.identity,
                owningSet.group.GetMaterial(batch.materialType), layer, camera, 0, batch.matProperties);
        }
    }
}
