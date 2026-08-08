using System.Collections.Generic;
using HarmonyLib;
using OniExtract2024.building;
using UnityEngine.Events;

namespace OniExtract2024.connection
{
    /// <summary>
    /// Adds export buttons to the in-game pause screen. Kept separate from the
    /// main-menu patches so in-game exports can be triggered from a loaded game.
    /// </summary>
    public class ConnectionExportPatches
    {
        [HarmonyPatch(typeof(PauseScreen), "ConfigureButtonInfos")]
        public static class PauseScreen_ConfigureButtonInfos_Patch
        {
            public static void Postfix(ref KButtonMenu.ButtonInfo[] ___buttons)
            {
                var list = new List<KButtonMenu.ButtonInfo>(___buttons);
                // Insert just before the last entry (typically "Desktop"/quit).
                int index = list.Count > 0 ? list.Count - 1 : 0;

                var buildingImagesButton = new KButtonMenu.ButtonInfo(
                    "Export Building Images",
                    global::Action.NumActions,
                    new UnityAction(ExportBuildingImages.Start));
                buildingImagesButton.isEnabled = true;
                list.Insert(index, buildingImagesButton);

                var connectionSpritesButton = new KButtonMenu.ButtonInfo(
                    "Export Connection Sprites",
                    global::Action.NumActions,
                    new UnityAction(ExportConnectionSprites.Start));
                connectionSpritesButton.isEnabled = true;
                list.Insert(index, connectionSpritesButton);

                ___buttons = list.ToArray();
            }
        }
    }
}
