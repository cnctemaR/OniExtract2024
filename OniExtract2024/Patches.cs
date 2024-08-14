using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using UnityEngine;
using Klei.AI;
using System;

namespace OniExtract2024
{
    public class Patches
    {
        static ExportMultiEntity exportMultiEntity = new ExportMultiEntity();

        [HarmonyPatch(typeof(MeteorShowerEvent))]
        [HarmonyPatch(MethodType.Constructor)]
        [HarmonyPatch(new Type[] { typeof(string), typeof(float), typeof(float), typeof(MathUtil.MinMax), typeof(MathUtil.MinMax), typeof(string), typeof(bool) })]
        public class MeteorShowerEvent_Constructor_Patch
        {
            static void Postfix(string id, float duration, float secondsPerMeteor, MathUtil.MinMax secondsBombardmentOff, MathUtil.MinMax secondsBombardmentOn, string clusterMapMeteorShowerID, bool affectedByDifficulty)
            {
                OutMeteorShowerEvent meteorShowEvent = new OutMeteorShowerEvent(id, (float) duration, (float) secondsPerMeteor, secondsBombardmentOff, secondsBombardmentOn, clusterMapMeteorShowerID, affectedByDifficulty);
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
                    KPrefabID prefabID = gameObject.GetComponent<KPrefabID>();
                    BMultiEntity BMultiEntity = new BMultiEntity(prefabID.PrefabID().Name, gameObject.GetComponent<KPrefabID>().Tags)
                    {
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
                //Debug.Log("OniExtract: " + "Export MultiEntity");
                exportMultiEntity.updateAllMeteorShowEvent();
                exportMultiEntity.ExportJsonFile();
            }
        }
    }
}
