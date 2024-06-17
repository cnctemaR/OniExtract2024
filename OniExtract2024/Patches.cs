using HarmonyLib;

namespace OniExtract2024
{
    public class Patches
    {
        public static ExportItem exportEquip = new ExportItem();

        [HarmonyPatch(typeof(LegacyModMain), "Load")]
        internal class OniExtract_Game_LegacyModMain
        {
            private static void Postfix()
            {

            }
        }

        [HarmonyPatch(typeof(EquipmentConfigManager), "RegisterEquipment")]
        internal class OniExtract_Game_Equipment
        {
            private static void Postfix(IEquipmentConfig config)
            {
                exportEquip.AddEquipmentDef(config);
                exportEquip.ExportJsonFile();
            }
        }
    }
}
