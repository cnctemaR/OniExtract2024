using System;
using HarmonyLib;
using static GeyserGenericConfig;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using Klei.AI;
using PeterHan.PLib.Options;
using UnityEngine;
using static STRINGS.UI.UISIDESCREENS;

namespace OniExtract2024
{
    public class Patches
    {
        static ExportEntity exportEntity = new ExportEntity();
        static ExportMultiEntity exportMultiEntity = new ExportMultiEntity();
        static ExportItem exportItem = new ExportItem();

        [HarmonyPatch(typeof(EntityConfigManager), "RegisterEntity")]
        internal class OniExtract_Game_EntityConfig
        {
            private static readonly MethodInfo InjectBehind = AccessTools.Method(typeof(IEntityConfig), nameof(IEntityConfig.CreatePrefab));
            private static readonly MethodInfo RegisterExportEntityMethod = AccessTools.Method(typeof(OniExtract_Game_EntityConfig), nameof(OniExtract_Game_EntityConfig.RegisterPatch));
            static string[] mRequiredDlcIds = null;
            static string[] mForbiddenDlcIds = null;

            public static void Prefix(IEntityConfig config, string[] requiredDlcIds, string[] forbiddenDlcIds)
            {
                mRequiredDlcIds = requiredDlcIds;
                mForbiddenDlcIds = forbiddenDlcIds;
            }

            public static GameObject RegisterPatch(GameObject gameObject)
            {
                if (gameObject == null)
                {
                    return gameObject;
                }
                KPrefabID prefabID = gameObject.GetComponent<KPrefabID>();
                prefabID.requiredDlcIds = mRequiredDlcIds;
                prefabID.forbiddenDlcIds = mForbiddenDlcIds;
                //Debug.Log(prefabID.PrefabID().Name.ToString());
                BEntity bEntity = new BEntity(prefabID.PrefabID().Name, gameObject.GetComponent<KPrefabID>());
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

        [HarmonyPatch(typeof(MeteorShowerEvent))]
        [HarmonyPatch(MethodType.Constructor)]
        [HarmonyPatch(new Type[] { typeof(string), typeof(float), typeof(float), typeof(MathUtil.MinMax), typeof(MathUtil.MinMax), typeof(string), typeof(bool) })]
        public class MeteorShowerEvent_Constructor_Patch
        {
            static void Postfix(string id, float duration, float secondsPerMeteor, MathUtil.MinMax secondsBombardmentOff, MathUtil.MinMax secondsBombardmentOn, string clusterMapMeteorShowerID, bool affectedByDifficulty)
            {
                if (!SingletonOptions<ModOptions>.Instance.MultiEntities) return;
                OutMeteorShowerEvent meteorShowEvent = new OutMeteorShowerEvent(id, (float)duration, (float)secondsPerMeteor, secondsBombardmentOff, secondsBombardmentOn, clusterMapMeteorShowerID, affectedByDifficulty);
                exportMultiEntity.addNewMeteorShowerEvent(meteorShowEvent);
            }
        }

        [HarmonyPatch(typeof(EntityConfigManager), "RegisterEntities")]
        internal class OniExtract_Game_IMultiEntityConfig
        {
            private static readonly MethodInfo InjectBehind = AccessTools.Method(typeof(IMultiEntityConfig), nameof(IMultiEntityConfig.CreatePrefabs));
            private static readonly MethodInfo RegisterExportEntityMethod = AccessTools.Method(typeof(OniExtract_Game_IMultiEntityConfig), nameof(OniExtract_Game_IMultiEntityConfig.RegisterPatch));
            static string entityType = null;

            public static void Prefix(IMultiEntityConfig config)
            {
                entityType = config.GetType().Name;
            }

            public static List<GameObject> RegisterPatch(List<GameObject> gameObjects)
            {
                foreach (var gameObject in gameObjects)
                {
                    if (gameObject == null)
                    {
                        continue;
                    }
                    KPrefabID prefabID = gameObject.GetComponent<KPrefabID>();
                    //Debug.Log(prefabID.PrefabID().Name.ToString());
                    BMultiEntity BMultiEntity = new BMultiEntity(prefabID.PrefabID().Name, gameObject.GetComponent<KPrefabID>())
                    {
                        nameString = prefabID.GetProperName(),
                        entityType = entityType
                    };
                    exportMultiEntity.LoadEntityComponent(gameObject, BMultiEntity);
                    exportMultiEntity.multiEntities.Add(BMultiEntity);
                }
                return gameObjects;
            }

            static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
            {
                var code = instructions.ToList();
                var insertionIndex = code.FindIndex(ci => ci.Calls(InjectBehind));

                if (insertionIndex != -1)
                {
                    if (!SingletonOptions<ModOptions>.Instance.MultiEntities) return code;
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
                Debug.Log("OniExtract: " + "Export Food");
                if (SingletonOptions<ModOptions>.Instance.Food)
                {
                    ExportFood exportFood = new ExportFood();
                    exportFood.ExportAllFood();
                    exportFood.ExportJsonFile();
                }

                Debug.Log("OniExtract: " + "Export recipes");
                if (SingletonOptions<ModOptions>.Instance.Recipe)
                {
                    ExportRecipe exportRecipe = new ExportRecipe();
                    exportRecipe.ExportComplexRecipes();
                    exportRecipe.ExportJsonFile();
                }

                Debug.Log("OniExtract: " + "Export Elements");
                if (SingletonOptions<ModOptions>.Instance.Element)
                {
                    ExportElement exportElement = new ExportElement();
                    exportElement.AddAllElement();
                    exportElement.ExportJsonFile();
                }

                Debug.Log("OniExtract: " + "Export PO_string");
                if (SingletonOptions<ModOptions>.Instance.PoString)
                {
                    ExportPOString exportPOString = new ExportPOString();
                    exportPOString.ExportAll();
                    exportPOString.ExportJsonFile();
                }

                Debug.Log("OniExtract: " + "Export Tags");
                if (SingletonOptions<ModOptions>.Instance.Tags)
                {
                    ExportTag exportTag = new ExportTag();
                    exportTag.AddAllGameTags();
                    exportTag.ExportJsonFile();
                }

                Debug.Log("OniExtract: " + "Export Db");
                if (SingletonOptions<ModOptions>.Instance.db)
                {
                    ExportDb exportDb = new ExportDb();
                    exportDb.AddDbResources();
                    exportDb.ExportJsonFile();
                }

                Debug.Log("OniExtract: " + "Export Buildings");
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

                Debug.Log("OniExtract: " + "Export UI Sprite");
                if (SingletonOptions<ModOptions>.Instance.UISprintInfo)
                {
                    ExportUISprite exportUISprite = new ExportUISprite();
                    exportUISprite.ExportAllUISprite();
                    exportUISprite.ExportJsonFile();
                }

                Debug.Log("OniExtract: " + "Export Entity");
                if (SingletonOptions<ModOptions>.Instance.Entities)
                {
                    exportEntity.ExportJsonFile();
                }

                Debug.Log("OniExtract: " + "Export MultiEntity");
                if (SingletonOptions<ModOptions>.Instance.MultiEntities)
                {
                    exportMultiEntity.updateAllMeteorShowEvent();
                    exportMultiEntity.ExportJsonFile();
                }

                Debug.Log("OniExtract: " + "Export Items");
                if (SingletonOptions<ModOptions>.Instance.Item)
                {
                    exportItem.ExportJsonFile();
                }
                Debug.Log("OniExtract: " + "Export Attribute");
                if (SingletonOptions<ModOptions>.Instance.Attr)
                {
                    ExportAttr exportAttr = new ExportAttr();
                    exportAttr.AddAllSicknessModifier();
                    exportAttr.AddAllEnumClass();
                    exportAttr.ExportJsonFile();
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

        [HarmonyPatch(typeof(EggConfig), "CreateEgg")]
        [HarmonyPatch(new Type[] { typeof(string), typeof(string), typeof(string), typeof(Tag), typeof(string), typeof(float), typeof(int), typeof(float), typeof(string[]), typeof(string[]), typeof(bool) })]
        internal class OniExtract_Game_Egg
        {
            private static void Postfix(ref GameObject __result)
            {
                if (!SingletonOptions<ModOptions>.Instance.Item) return;
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
                if (!SingletonOptions<ModOptions>.Instance.Item) return;
                exportItem.AddEquipmentDef(config);
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
