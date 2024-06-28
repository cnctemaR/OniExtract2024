using HarmonyLib;

namespace OniExtract2024
{
    public class Patches
    {
        [HarmonyPatch(typeof(LegacyModMain), "Load")]
        internal class OniExtract_Game_LegacyModMain
        {
            private static void Postfix()
            {
                //Debug.Log("OniExtract: " + "Export Attribute");
                ExportAttr exportAttr = new ExportAttr();
                exportAttr.AddAllSicknessModifier();
                exportAttr.AddAllEnumClass();
                exportAttr.ExportJsonFile();
            }
        }
    }
}
