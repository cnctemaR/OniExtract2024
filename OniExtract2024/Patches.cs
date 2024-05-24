using System;
using HarmonyLib;
using static GeyserGenericConfig;
using System.Collections.Generic;
using PeterHan.PLib.Options;
using UnityEngine;

namespace OniExtract2024
{
    public class Patches
    {
        [HarmonyPatch(typeof(ComplexRecipeManager))]
        [HarmonyPatch("Add")]
        public class ComplexRecipeManager_Add_Patch
        {
            // 在调用 Add 方法之前进行前置检查
            static bool Prefix(ComplexRecipe recipe, List<ComplexRecipe> ___recipes)
            {
                foreach (ComplexRecipe recipe2 in ___recipes)
                {
                    if (recipe2.id == recipe.id)
                    {
                        // 在到达 Debug.LogError 之前拦截并跳过该方法的执行
                        return false;
                    }
                }
                // 继续执行原始方法
                return true;
            }
        }

        [HarmonyPatch(typeof(EntityConfigManager), "RegisterEntity")]
        internal class OniExtract_Game_EntityConfig
        {
            static ExportEntity exportEntity = new ExportEntity();
            static void Postfix(IEntityConfig config)
            {
                //Debug.Log("OniExtract: " + "Export Entity");
                GameObject gameObject = config.CreatePrefab();
                KPrefabID prefabID = gameObject.GetComponent<KPrefabID>();
                //Debug.Log(prefabID.PrefabID().Name);
                BEntity bEntity = new BEntity(prefabID.PrefabID().Name, gameObject.GetComponent<KPrefabID>().Tags);
                var dlcIds = config.GetDlcIds();
                ExportEntity.LoadEntityComponent(gameObject, bEntity, dlcIds);
                exportEntity.entities.Add(bEntity);
                exportEntity.ExportJsonFile();
            }
        }

        [HarmonyPatch(typeof(LegacyModMain), "Load")]
        internal class OniExtract_Game_LegacyModMain
        {
            static ExportFood exportFood = new ExportFood();
            private static void Postfix()
            {
                //Debug.Log("OniExtract: " + "Export Food");
                if (SingletonOptions<ModOptions>.Instance.Food)
                {
                    exportFood.ExportAllFood();
                    exportFood.ExportJsonFile();
                }

                //Debug.Log("OniExtract: " + "Export recipes");
                if (SingletonOptions<ModOptions>.Instance.Recipe)
                {
                    ExportRecipe exportRecipe = new ExportRecipe();
                    exportRecipe.ExportComplexRecipes();
                    exportRecipe.ExportJsonFile();
                }

                //Debug.Log("OniExtract: " + "Export Elements");
                if (SingletonOptions<ModOptions>.Instance.Element)
                {
                    ExportElement exportElement = new ExportElement();
                    exportElement.AddAllElement();
                    exportElement.ExportJsonFile();
                }

                //Debug.Log("OniExtract: " + "Export PO_string");
                if (SingletonOptions<ModOptions>.Instance.PoString)
                {
                    ExportPOString exportPOString = new ExportPOString();
                    exportPOString.ExportAll();
                    exportPOString.ExportJsonFile();
                }

                //Debug.Log("OniExtract: " + "Export Tags");
                if (SingletonOptions<ModOptions>.Instance.Tags)
                {
                    ExportTag exportTag = new ExportTag();
                    exportTag.AddAllGameTags();
                    exportTag.ExportJsonFile();
                }

                //Debug.Log("OniExtract: " + "Export Db");
                if (SingletonOptions<ModOptions>.Instance.db)
                {
                    ExportDb exportDb = new ExportDb();
                    exportDb.AddDbResources();
                    exportDb.ExportJsonFile();
                }

                //Debug.Log("OniExtract: " + "Export Buildings");
                if (SingletonOptions<ModOptions>.Instance.Building)
                {
                    ExportBuilding exportBuilding = new ExportBuilding();
                    exportBuilding.ExportBuildMenu();
                    for (int indexBuilding = 0; indexBuilding < Assets.BuildingDefs.Count; ++indexBuilding)
                    {
                        BuildingDef buildingDef = Assets.BuildingDefs[indexBuilding];
                        exportBuilding.AddNewBuildingEntity(buildingDef);
                        exportBuilding.AddNewBuildingDef(buildingDef);
                    }

                    exportBuilding.ExportJsonFile();
                }

                //Debug.Log("OniExtract: " + "Export UI Sprite");
                if (!SingletonOptions<ModOptions>.Instance.UISprintInfo) return;
                ExportUISprite exportUISprite = new ExportUISprite();
                exportUISprite.ExportAllUISprite();
                exportUISprite.ExportJsonFile();

            }
        }

        [HarmonyPatch(typeof(GeyserGenericConfig), "GenerateConfigs")]
        internal class OniExtract_Game_Geysers
        {
            static void Postfix(ref List<GeyserPrefabParams> __result)
            {
                //Debug.Log("OniExtract: " + "Export Geysers");
                if (!SingletonOptions<ModOptions>.Instance.Geyser) return;
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
                if (!SingletonOptions<ModOptions>.Instance.Equipment) return;
                exportEquip.AddEquipmentDef(config);
                exportEquip.ExportJsonFile();
            }
        }

        [HarmonyPatch(typeof(Localization), nameof(Localization.Initialize))]
        internal class Localization_Initialize_Patch
        {
            private static void Postfix()
            {
                LocString.CreateLocStringKeys(typeof(ModStrings.Options));
            }
        }
        
    }
}
