using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace OniExtract2024.connection
{
    /// <summary>
    /// Separate, in-game connection-sprite exporter. Independent of the main-menu JSON
    /// export pipeline (Patches.OniExtract_Game_LegacyModMain) because connectables can
    /// only be rendered inside a loaded game/sandbox: utilities need a placed instance
    /// plus the live KAnimBatchManager and camera; tiles need their texture atlas loaded.
    ///
    /// Triggered from a button on the in-game pause screen (see ConnectionExportPatches).
    /// Writes one PNG per 4-bit bitmask (left=1, right=2, up=4, down=8) to:
    ///     {RootFolder}/export/connection_sprites/{prefabId}/{bitmask}.png
    ///
    /// Two rendering paths, by building type:
    ///   - isUtility  : ConnectionSpriteSnapshotter (kanim camera snapshot, then a
    ///                  cell-centred crop to remove the snapshot's whitespace).
    ///   - isKAnimTile: TileConnectionExtractor (resamples the BlockTileAtlas into a
    ///                  cell-anchored frame with the game's trim/overhang geometry so
    ///                  tiles join seamlessly).
    ///
    /// TODO (future): expose progress/results in an in-game panel (the user wants this
    /// to grow into a debugging UI), and reinstate the decor "tops"/corner layer for
    /// tiles (dropped for now - its placement is diagonal-dependent and can't be
    /// represented in the website's 4-bit/16-state model; see TileConnectionExtractor).
    /// </summary>
    public static class ExportConnectionSprites
    {
        public static bool IsRunning { get; private set; }

        public static string RootDir =>
            Path.Combine(Util.RootFolder(), "export", "connection_sprites");

        public static string OutputDir(string prefabId) => Path.Combine(RootDir, prefabId);

        public static void Start()
        {
            if (Game.Instance == null)
            {
                Debug.LogWarning("OniExtract: connection-sprite export requires a loaded game.");
                return;
            }
            if (IsRunning)
            {
                Debug.LogWarning("OniExtract: connection-sprite export already running.");
                return;
            }
            Game.Instance.StartCoroutine(Run());
        }

        private static IEnumerator Run()
        {
            IsRunning = true;
            Debug.Log("OniExtract: connection-sprite export started -> " + RootDir);

            int tileBuildings = 0, tileSprites = 0;
            int utilityBuildings = 0;

            // --- 1) Tiles: extracted straight from BlockTileAtlas (no placement) -----
            foreach (var def in Assets.BuildingDefs)
            {
                if (def == null || !def.isKAnimTile)
                    continue;
                int files = TileConnectionExtractor.Export(def);
                if (files > 0)
                {
                    tileBuildings++;
                    tileSprites += files;
                }
                yield return null;
            }
            Debug.Log("OniExtract: tiles exported - " + tileBuildings + " buildings, " + tileSprites + " sprites.");

            // --- 2) Utilities: spawn a temp instance, snapshot 16 kanim states -------
            // Place well away from the camera/colony to avoid disturbing the view.
            int cell = Grid.PosToCell(Camera.main.transform.position);
            for (int i = 0; i < 12; i++)
                cell = Grid.CellDownLeft(cell);
            Vector3 spawnPos = Grid.CellToPos(cell);

            foreach (var def in Assets.BuildingDefs)
            {
                if (def == null || !def.isUtility)
                    continue;
                if (def.BuildingComplete == null || !def.BuildingComplete.TryGetComponent<KBatchedAnimController>(out _))
                    continue;

                GameObject temp = def.Create(spawnPos, null,
                    new List<Tag> { SimHashes.Unobtanium.CreateTag() }, null, 100f, def.BuildingComplete);
                if (temp == null)
                    continue;

                var snapshotter = temp.AddOrGet<ConnectionSpriteSnapshotter>();
                utilityBuildings++;
                yield return snapshotter.ExportThenDestroy();
            }
            Debug.Log("OniExtract: utilities exported - " + utilityBuildings + " buildings.");

            Debug.Log("OniExtract: connection-sprite export complete -> " + RootDir);
            IsRunning = false;
        }
    }
}
