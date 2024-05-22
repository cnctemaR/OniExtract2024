using HarmonyLib;

namespace OniExtract2024
{
    public class Patches
    {
        [HarmonyPatch(typeof(CodexCache), "CodexCacheInit")]
        internal class OniExtract_Game_Codex
        {
            public static ExportCodex exportCodex = new ExportCodex();
            private static void Postfix()
            {
                //Debug.Log("OniExtract: " + "Export Codex");
                exportCodex.AddCategoryEntry(CodexCache.entries);
                exportCodex.ExportJsonFile();
            }
        }

        [HarmonyPatch(typeof(LegacyModMain), "Load")]
        internal class OniExtract_Game_LegacyModMain
        {
            private static void Postfix()
            {

            }
        }
    }
}
