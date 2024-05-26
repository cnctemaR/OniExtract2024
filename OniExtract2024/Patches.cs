using System;
using HarmonyLib;
using static GeyserGenericConfig;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using PeterHan.PLib.Options;
using UnityEngine;

namespace OniExtract2024
{
    public class Patches
    {
        static ExportEntity exportEntity = new ExportEntity();

        [HarmonyPatch(typeof(EntityConfigManager), "RegisterEntity")]
        internal class OniExtract_Game_EntityConfig
        {
            private static readonly MethodInfo InjectBehind = AccessTools.Method(typeof(IEntityConfig), nameof(IEntityConfig.CreatePrefab));
            private static readonly MethodInfo RegisterExportEntityMethod = AccessTools.Method(typeof(OniExtract_Game_EntityConfig), nameof(OniExtract_Game_EntityConfig.RegisterPatch));
            static string[] tempDlcs = null;

            public static void Prefix(IEntityConfig config)
            {
                tempDlcs = config.GetDlcIds();
            }

            public static GameObject RegisterPatch(GameObject gameObject)
            {
                KPrefabID prefabID = gameObject.GetComponent<KPrefabID>();
                BEntity bEntity = new BEntity(prefabID.PrefabID().Name, gameObject.GetComponent<KPrefabID>().Tags)
                {
                    dlcIds = tempDlcs
                };
                ExportEntity.LoadEntityComponent(gameObject, bEntity);
                exportEntity.entities.Add(bEntity);
                return gameObject;
            }

            static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
            {
                var code = instructions.ToList();
                var insertionIndex = code.FindIndex(ci => ci.Calls(InjectBehind));

                if (insertionIndex != -1)
                {
                    if (!SingletonOptions<ModOptions>.Instance.Entities) return code;
                    code.Insert(++insertionIndex, new CodeInstruction(OpCodes.Call, RegisterExportEntityMethod));
                }
                return code;
            }
        }

        [HarmonyPatch(typeof(LegacyModMain), "Load")]
        internal class OniExtract_Game_LegacyModMain
        {
            private static void Postfix()
            {
                //Debug.Log("OniExtract: " + "Export Food");
                if (SingletonOptions<ModOptions>.Instance.Food)
                {
                    ExportFood exportFood = new ExportFood();
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
                if (SingletonOptions<ModOptions>.Instance.UISprintInfo)
                {
                    ExportUISprite exportUISprite = new ExportUISprite();
                    exportUISprite.ExportAllUISprite();
                    exportUISprite.ExportJsonFile();
                }

                //Debug.Log("OniExtract: " + "Export Entity");
                if (SingletonOptions<ModOptions>.Instance.Entities)
                {
                    exportEntity.ExportJsonFile();
                }

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
