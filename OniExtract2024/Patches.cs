using System;
using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;
using static STRINGS.UI.UISIDESCREENS;

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
        [HarmonyPatch(new Type[] { typeof(string), typeof(string), typeof(string), typeof(Tag), typeof(string), typeof(float), typeof(int), typeof(float), typeof(string[]), typeof(string[]), typeof(bool) })]
        internal class OniExtract_Game_Egg
        {
            private static void Postfix(ref GameObject __result)
            {
                KPrefabID prefabID = __result.GetComponent<KPrefabID>();
                BEgg bEgg = new BEgg(prefabID.PrefabID().Name, __result.GetComponent<KPrefabID>());
                exportItem.AddEgg(__result, bEgg);
            }
        }

        [HarmonyPatch(typeof(EntityTemplates), "CreateAndRegisterSeedForPlant")]
        [HarmonyPatch(new Type[] { typeof(GameObject), typeof(IHasDlcRestrictions), typeof(SeedProducer.ProductionType), typeof(string), typeof(string), typeof(string), typeof(KAnimFile), typeof(string), typeof(int), typeof(List<Tag>), typeof(SingleEntityReceptacle.ReceptacleDirection), typeof(Tag), typeof(int), typeof(string), typeof(EntityTemplates.CollisionShape), typeof(float), typeof(float), typeof(Recipe.Ingredient[]), typeof(string), typeof(bool) })]
        internal class OniExtract_Game_Seed
        {
            private static void Postfix(ref GameObject __result)
            {
                KPrefabID prefabID = __result.GetComponent<KPrefabID>();
                BSeed bSeed = new BSeed(prefabID.PrefabID().Name, __result.GetComponent<KPrefabID>());
                exportItem.AddSeed(__result, bSeed);
            }
        }

        [HarmonyPatch(typeof(EquipmentConfigManager), "RegisterEquipment")]
        internal class OniExtract_Game_Equipment_Entity
        {
            private static void Postfix(IEquipmentConfig config)
            {
                string[] requiredDlcIds = null;
                string[] forbiddenDlcIds = null;
                if (config.GetDlcIds() != null)
                {
                    DlcManager.ConvertAvailableToRequireAndForbidden(config.GetDlcIds(), out requiredDlcIds, out forbiddenDlcIds);
                    DebugUtil.DevLogError($"{config.GetType()} implements GetDlcIds, which is obsolete.");
                }
                else
                {
                    IHasDlcRestrictions hasDlcRestrictions = config as IHasDlcRestrictions;
                    if (hasDlcRestrictions != null)
                    {
                        requiredDlcIds = hasDlcRestrictions.GetRequiredDlcIds();
                        forbiddenDlcIds = hasDlcRestrictions.GetForbiddenDlcIds();
                    }
                }

                if (!DlcManager.IsCorrectDlcSubscribed(requiredDlcIds, forbiddenDlcIds))
                {
                    return;
                }
                EquipmentDef equipmentDef = config.CreateEquipmentDef();
                GameObject gameObject = EntityTemplates.CreateLooseEntity(equipmentDef.Id, equipmentDef.Name, equipmentDef.RecipeDescription, equipmentDef.Mass, unitMass: true, equipmentDef.Anim, "object", Grid.SceneLayer.Ore, equipmentDef.CollisionShape, equipmentDef.width, equipmentDef.height, isPickupable: true, 0, equipmentDef.OutputElement);
                config.DoPostConfigure(gameObject);
                // Add Equipment
                KPrefabID prefabID = gameObject.AddOrGet<KPrefabID>();
                BEquipment bEquip = new BEquipment(prefabID.PrefabID().Name, gameObject.GetComponent<KPrefabID>());
                exportItem.AddEquipment(gameObject, bEquip);
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
