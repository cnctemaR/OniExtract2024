using HarmonyLib;
using static GeyserGenericConfig;
using System.Collections.Generic;

namespace OniExtract2024
{
    public class Patches
    {
        [HarmonyPatch(typeof(LegacyModMain), "Load")]
        internal class OniExtract_Game_LegacyModMain
        {
            private static void Postfix()
            {

            }
        }

        [HarmonyPatch(typeof(GeyserGenericConfig), "GenerateConfigs")]
        internal class OniExtract_Game_Geysers
        {
            static void Postfix(ref List<GeyserPrefabParams> __result)
            {
                Debug.Log("OniExtract: " + "Export Geysers");
                ExportGeyser exportGeyser = new ExportGeyser();
                exportGeyser.AddGeyserPrefabParams(__result);
                exportGeyser.ExportJsonFile();
            }
        }
    }
}
