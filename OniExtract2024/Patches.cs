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

            }
        }

        [HarmonyPatch(typeof(EquipmentConfigManager), "RegisterEquipment")]
        internal class OniExtract_Game_Equipment
        {
            public static ExportEquipment exportEquip = new ExportEquipment();
            private static void Postfix(IEquipmentConfig config)
            {
                //Debug.Log("OniExtract: " + "Export Equipments");
                exportEquip.AddEquipmentDef(config);
                exportEquip.ExportJsonFile();
            }
        }
    }
}
