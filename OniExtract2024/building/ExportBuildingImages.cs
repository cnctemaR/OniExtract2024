using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace OniExtract2024.building
{
    /// <summary>
    /// In-game building-image exporter. Triggered from a pause-screen button
    /// (see ConnectionExportPatches). Iterates Assets.BuildingDefs, spawns each
    /// building off-screen, renders it at 200 px/cell via BuildingImageSnapshotter,
    /// and writes ui_image/{prefabId}.png — overwriting the low-res atlas icons
    /// produced by the main-menu pass.
    ///
    /// Run order: main-menu JSON+icon pass first (all icons), then this in-game
    /// pass (overwrites building icons with hi-res kanim renders).
    /// </summary>
    public static class ExportBuildingImages
    {
        public static bool IsRunning { get; private set; }

        // Deprecated buildings as a class crash the sweep: spawning one without full
        // game context corrupts state and the run dies mid-sweep (the exact culprit was
        // never isolated — see BUILDING_IMAGES_FINDINGS.md "Deprecated buildings"). So we
        // skip Deprecated by default and opt back in only buildings vetted to spawn
        // cleanly that the website still displays and needs a hi-res icon for.
        private static readonly HashSet<string> DeprecatedAllowlist = new HashSet<string>
        {
            "SteamTurbine", // old 5x4 steam turbine; site still shows it, low-res today
        };

        public static string OutputDir =>
            Path.Combine(Util.RootFolder(), "export", "ui_image");

        // Per-building rendered-image rectangle (cells, footprint-relative), keyed by
        // prefab tag name (== building.json `name`). Filled during the sweep, then merged
        // into database/building.json so the website can place each tight-cropped icon
        // without squishing its overhang. See WEBSITE_POSTPROCESSING.md "uiImageRect".
        private static readonly Dictionary<string, UiImageRect> Rects =
            new Dictionary<string, UiImageRect>();

        public static void Start()
        {
            if (Game.Instance == null)
            {
                Debug.LogWarning("OniExtract: building-image export requires a loaded game.");
                return;
            }
            if (IsRunning)
            {
                Debug.LogWarning("OniExtract: building-image export already running.");
                return;
            }
            Game.Instance.StartCoroutine(Run());
        }

        private static IEnumerator Run()
        {
            IsRunning = true;
            Rects.Clear();
            Debug.Log("OniExtract: building-image export started -> " + OutputDir);

            // Rocket modules need a CraftModuleInterface ancestor to find on spawn.
            // Attach the minimum set to the world object so they bind rather than
            // null-ref. Pattern taken from Sgt_Imalas AnimExportTool/Patches.cs.
            var worldGO = ClusterManager.Instance.activeWorld.gameObject;
            worldGO.AddOrGet<ClusterDestinationSelector>();
            worldGO.AddOrGet<ClusterTraveler>();
            worldGO.AddOrGet<Clustercraft>();
            worldGO.AddOrGet<CraftModuleInterface>();

            // Spawn well off-screen to avoid disturbing the live colony.
            int cell = Grid.PosToCell(Camera.main.transform.position);
            for (int i = 0; i < 12; i++)
                cell = Grid.CellDownLeft(cell);
            Vector3 spawnPos = Grid.CellToPos(cell);

            int exported = 0, skipped = 0;
            foreach (var def in Assets.BuildingDefs)
            {
                if (def == null || def.BuildingComplete == null || !def.ShowInBuildMenu)
                {
                    skipped++;
                    continue;
                }
                // Deprecated buildings crash the sweep unless vetted (see DeprecatedAllowlist).
                if (def.Deprecated)
                {
                    var dkpid = def.BuildingComplete.GetComponent<KPrefabID>();
                    if (dkpid == null || !DeprecatedAllowlist.Contains(dkpid.PrefabTag.Name))
                    {
                        skipped++;
                        continue;
                    }
                }
                if (!def.BuildingComplete.TryGetComponent<KBatchedAnimController>(out _))
                {
                    skipped++;
                    continue;
                }
                // RocketModuleCluster buildings crash on spawn without a full rocket context;
                // WireUtilitySemiVirtualNetworkLink crashes are subsumed by this filter.
                if (def.BuildingComplete.TryGetComponent<RocketModuleCluster>(out _))
                {
                    skipped++;
                    continue;
                }

                GameObject temp = def.Create(spawnPos, null,
                    new List<Tag> { SimHashes.Unobtanium.CreateTag() }, null, 100f, def.BuildingComplete);
                if (temp == null)
                {
                    skipped++;
                    continue;
                }

                var snapshotter = temp.AddOrGet<BuildingImageSnapshotter>();
                snapshotter.StartExport(OutputDir, Rects);
                // Wait for the snapshotter to destroy the temp building. Unity defers
                // Destroy to end-of-frame, so IsNullOrDestroyed becomes true one frame
                // after KDestroyGameObject — the while loop handles this robustly.
                while (!temp.IsNullOrDestroyed())
                    yield return null;
                exported++;
            }

            Debug.Log("OniExtract: building-image export complete -> " + exported + " exported, " + skipped + " skipped.");

            PatchBuildingJson();
            IsRunning = false;
        }

        // Merge the measured uiImageRect for each rendered building into the building.json
        // the main-menu pass already wrote. Done as a post-pass (not in ExportBuilding)
        // because the rect can only be measured from a live in-game render, which happens
        // long after the no-save JSON export. The website reads uiImageRect off each
        // bBuildingDefList entry; buildings we did not render keep the legacy
        // stretch-to-footprint fallback (field omitted).
        private static void PatchBuildingJson()
        {
            string dbDir = BaseExport.BuildExportPath(
                Util.RootFolder(), "database", DlcManager.IsExpansion1Active());
            string path = Path.Combine(dbDir, "building.json");
            if (!File.Exists(path))
            {
                Debug.LogWarning("OniExtract: building.json not found at " + path +
                    " — run the main-menu export first so uiImageRect can be merged in.");
                return;
            }

            JObject root = JObject.Parse(File.ReadAllText(path));
            JArray list = root["bBuildingDefList"] as JArray;
            if (list == null)
            {
                Debug.LogWarning("OniExtract: building.json has no bBuildingDefList; skipping uiImageRect merge.");
                return;
            }

            int placed = 0;
            foreach (JObject entry in list)
            {
                string name = (string)entry["name"];
                if (name != null && Rects.TryGetValue(name, out UiImageRect r))
                {
                    entry["uiImageRect"] = new JObject
                    {
                        ["x"] = Round(r.x),
                        ["y"] = Round(r.y),
                        ["w"] = Round(r.w),
                        ["h"] = Round(r.h),
                    };
                    placed++;
                }
            }

            File.WriteAllText(path, root.ToString(Formatting.Indented));
            Debug.Log("OniExtract: buildings with uiImageRect placement: " + placed + " / " + list.Count);
        }

        // 4 decimals ≈ 0.02 px at 200 px/cell — finer than the render — while keeping the
        // JSON readable. Real numbers, never rounded to whole cells (per the contract).
        private static float Round(float v) => Mathf.Round(v * 10000f) / 10000f;
    }
}
