using HarmonyLib;
using UnityEngine;

namespace OniExtract2024
{
    public class Patches
    {
        static ExportItem exportItem = new ExportItem();

        [HarmonyPatch(typeof(LegacyModMain), "Load")]
        internal class OniExtract_Game_LegacyModMain
        {
            private static void Postfix()
            {
                //Debug.Log("OniExtract: " + "Export Items");
                exportItem.ExportJsonFile();
            }
        }

        [HarmonyPatch(typeof(EggConfig), "CreateEgg")]
        internal class OniExtract_Game_Egg
        {
            private static void Postfix(ref GameObject __result)
            {
                KPrefabID prefabID = __result.GetComponent<KPrefabID>();
                BEgg bEgg = new BEgg(prefabID.PrefabID().Name, __result.GetComponent<KPrefabID>().Tags);
                exportItem.AddEgg(__result, bEgg);
            }
        }

        [HarmonyPatch(typeof(EntityTemplates), "CreateAndRegisterSeedForPlant")]
        internal class OniExtract_Game_Seed
        {
            private static void Postfix(ref GameObject __result)
            {
                KPrefabID prefabID = __result.GetComponent<KPrefabID>();
                BSeed bSeed = new BSeed(prefabID.PrefabID().Name, __result.GetComponent<KPrefabID>().Tags);
                exportItem.AddSeed(__result, bSeed);
            }
        }

        [HarmonyPatch(typeof(EquipmentConfigManager), "RegisterEquipment")]
        internal class OniExtract_Game_Equipment
        {
            private static void Postfix(IEquipmentConfig config)
            {
                exportItem.AddEquipmentDef(config);
                exportItem.ExportJsonFile();
            }
        }
    }
}
