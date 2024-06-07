using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using UnityEngine;

namespace OniExtract2024
{
    public class Patches
    {
        static ExportMultiEntity exportMultiEntity = new ExportMultiEntity();

        [HarmonyPatch(typeof(EntityConfigManager), "RegisterEntities")]
        internal class OniExtract_Game_IMultiEntityConfig
        {
            private static readonly MethodInfo InjectBehind = AccessTools.Method(typeof(IMultiEntityConfig), nameof(IMultiEntityConfig.CreatePrefabs));
            private static readonly MethodInfo RegisterExportEntityMethod = AccessTools.Method(typeof(OniExtract_Game_IMultiEntityConfig), nameof(OniExtract_Game_IMultiEntityConfig.RegisterPatch));

            public static List<GameObject> RegisterPatch(List<GameObject> gameObjects)
            {
                foreach (var gameObject in gameObjects)
                {
                    KPrefabID prefabID = gameObject.GetComponent<KPrefabID>();
                    BMultiEntity BMultiEntity = new BMultiEntity(prefabID.PrefabID().Name, gameObject.GetComponent<KPrefabID>().Tags);
                    ExportMultiEntity.LoadEntityComponent(gameObject, BMultiEntity);
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
                exportMultiEntity.ExportJsonFile();
            }
        }
    }
}
