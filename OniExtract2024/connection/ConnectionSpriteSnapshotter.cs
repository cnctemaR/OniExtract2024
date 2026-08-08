// Camera-snapshot rendering adapted from the GPL-2.0 project Sgt_Imalas-Oni-Mods
// (AnimExportTool/AETE_KbacSnapShotter.cs), itself courtesy of Aki
// (https://github.com/aki-art/ONI-Mods). Adapted for OniExtract2024 to render the 16
// utility connection states to PNG.
//
// Difference from the original: OniExtract2024 references the stock (non-publicized)
// Assembly-CSharp, so KBatchedAnimController.batch and KAnimBatchManager.activeBatchSets
// are not directly accessible. We reach them through HarmonyLib.Traverse.

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HarmonyLib;
using UnityEngine;

namespace OniExtract2024.connection
{
    /// <summary>
    /// Attached to a temporary, freshly-created utility building. Plays each of the 16
    /// connection-state animations and snapshots the kanim into one PNG per bitmask,
    /// then destroys the host GameObject. Driven inline by ExportConnectionSprites'
    /// coroutine (no OnSpawn timing dependency).
    /// </summary>
    public class ConnectionSpriteSnapshotter : KMonoBehaviour
    {
        private Camera snapshotCamera;
        private RenderTexture targetTexture;
        private static readonly int DrawLayer = 30;

        public IEnumerator ExportThenDestroy()
        {
            // Give the freshly-created building a frame (and a moment) to initialise its
            // kanim controller and register its batch with the KAnimBatchManager.
            yield return new WaitForSecondsRealtime(0.1f);

            var visualizer = GetComponent<KAnimGraphTileVisualizer>();
            var kbac = GetComponent<KBatchedAnimController>();
            string prefabId = GetComponent<KPrefabID>().PrefabTag.Name;

            if (visualizer != null && visualizer.connectionManager != null && kbac != null)
            {
                yield return ExportStates(prefabId, visualizer.connectionManager, kbac);
            }
            else
            {
                Debug.LogWarning("OniExtract: " + prefabId + " has no usable KAnimGraphTileVisualizer/connectionManager; skipped.");
            }

            Util.KDestroyGameObject(gameObject);
        }

        // No margin: crop tight to the cell-centred content so sprites tile flush
        // edge-to-edge on the website (a 1px anti-aliased fringe is acceptable).
        private static readonly int CropMargin = 0;

        private IEnumerator ExportStates(string prefabId, IUtilityNetworkMgr mgr, KBatchedAnimController kbac)
        {
            string dir = ExportConnectionSprites.OutputDir(prefabId);
            Directory.CreateDirectory(dir);

            // UtilityConnections is Left=1, Right=2, Up=4, Down=8 - identical to the
            // website's required bitmask encoding, so i maps straight to the filename.
            var shots = new Texture2D[16];
            for (int i = 0; i <= 15; i++)
            {
                string animation = mgr.GetVisualizerString((UtilityConnections)i);
                if (!string.IsNullOrEmpty(animation) && kbac.HasAnimation(animation))
                {
                    kbac.Play(animation);
                }
                yield return null;
                shots[i] = SnapShot();
            }

            CropAndWrite(dir, shots);

            foreach (var t in shots)
            {
                if (t != null)
                    Destroy(t);
            }
        }

        // The snapshot frame carries a full extra cell of padding on every side, so a
        // 1x1 wire is stranded in whitespace. Crop all 16 states to one square window,
        // centred on the frame centre (where the cell sits) and just large enough to
        // hold the largest state's content. Cropping the whole set identically - rather
        // than per-state bounding boxes - preserves the cell anchor so the sprites still
        // tile against each other.
        private static void CropAndWrite(string dir, Texture2D[] shots)
        {
            int w = 0, h = 0;
            foreach (var t in shots)
            {
                if (t != null) { w = t.width; h = t.height; break; }
            }
            if (w == 0)
                return;

            int cx = w / 2, cy = h / 2;
            var pixels = new Color[shots.Length][];
            int half = 0;
            for (int k = 0; k < shots.Length; k++)
            {
                if (shots[k] == null)
                    continue;
                Color[] px = shots[k].GetPixels();
                pixels[k] = px;
                for (int y = 0; y < h; y++)
                {
                    int row = y * w;
                    for (int x = 0; x < w; x++)
                    {
                        if (px[row + x].a > 0.03f)
                        {
                            int dx = Mathf.Abs(x - cx);
                            int dy = Mathf.Abs(y - cy);
                            if (dx > half) half = dx;
                            if (dy > half) half = dy;
                        }
                    }
                }
            }

            half += CropMargin;
            half = Mathf.Min(half, Mathf.Min(cx, cy)); // stay within the frame
            if (half < 1)
                return;

            int side = 2 * half;
            int sx0 = cx - half, sy0 = cy - half;
            for (int k = 0; k < shots.Length; k++)
            {
                if (pixels[k] == null)
                    continue;
                Color[] outPx = new Color[side * side];
                for (int oy = 0; oy < side; oy++)
                {
                    int syp = sy0 + oy;
                    for (int ox = 0; ox < side; ox++)
                    {
                        int sxp = sx0 + ox;
                        if (sxp >= 0 && sxp < w && syp >= 0 && syp < h)
                            outPx[oy * side + ox] = pixels[k][syp * w + sxp];
                    }
                }
                Texture2D outTex = new Texture2D(side, side);
                outTex.SetPixels(outPx);
                outTex.Apply();
                File.WriteAllBytes(Path.Combine(dir, k + ".png"), outTex.EncodeToPNG());
                Destroy(outTex);
            }
        }

        private Texture2D SnapShot()
        {
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
            {
                RenderBatch(GetBatch(k), snapshotCamera, DrawLayer);
            }

            snapshotCamera.Render();
            Texture2D tex = new Texture2D(targetTexture.width, targetTexture.height);
            tex.ReadPixels(new Rect(0, 0, targetTexture.width, targetTexture.height), 0, 0);
            tex.Apply();

            RenderTexture.active = previous;
            snapshotCamera.enabled = false;
            Destroy(snapshotCamera.gameObject);
            snapshotCamera = null;
            CameraController.Instance.baseCamera.enabled = true;
            return tex;
        }

        private void InitCamera()
        {
            var building = GetComponent<Building>();
            int widthInt = building != null ? building.Def.WidthInCells : 1;
            int heightInt = building != null ? building.Def.HeightInCells : 1;

            const float pixelsPerUnit = 100f;
            const float paddingPx = 100f;
            int textureWidth = Mathf.CeilToInt(widthInt * pixelsPerUnit + 2 * paddingPx);
            int textureHeight = Mathf.CeilToInt(heightInt * pixelsPerUnit + 2 * paddingPx);

            targetTexture = new RenderTexture(textureWidth, textureHeight, 24);
            targetTexture.Create();

            var reference = CameraController.Instance.baseCamera;
            snapshotCamera = CameraController.CloneCamera(reference, "OniExtract_ConnectionCam");
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
            snapshotCamera.orthographicSize = textureHeight / (2f * pixelsPerUnit);
            snapshotCamera.aspect = (float)textureWidth / textureHeight;
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
