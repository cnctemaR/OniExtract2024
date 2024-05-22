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
            static ExportFood exportFood = new ExportFood();
            private static void Postfix()
            {
                //Debug.Log("OniExtract: " + "Export Food");
                exportFood.ExportAllFood();
                exportFood.ExportJsonFile();
                //Debug.Log("OniExtract: " + "Export recipes");
                ExportRecipe exportRecipe = new ExportRecipe();
                exportRecipe.ExportComplexRecipes();
                exportRecipe.ExportJsonFile();
                //Debug.Log("OniExtract: " + "Export Elements");
                ExportElement exportElement = new ExportElement();
                exportElement.AddAllElement();
                exportElement.ExportJsonFile();
                //Debug.Log("OniExtract: " + "Export PO_string");
                ExportPOString exportPOString = new ExportPOString();
                exportPOString.ExportAll();
                exportPOString.ExportJsonFile();
            }
        }

        [HarmonyPatch(typeof(GeyserGenericConfig), "GenerateConfigs")]
        internal class OniExtract_Game_Geysers
        {
            static void Postfix(ref List<GeyserPrefabParams> __result)
            {
                //Debug.Log("OniExtract: " + "Export Geysers");
                ExportGeyser exportGeyser = new ExportGeyser();
                exportGeyser.AddGeyserPrefabParams(__result);
                exportGeyser.ExportJsonFile();
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
