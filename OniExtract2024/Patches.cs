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
            private static readonly MethodInfo InjectBehind = AccessTools.Method(typeof(IEntityConfig), nameof(IEntityConfig.CreatePrefab));
            private static readonly MethodInfo RegisterExportEntityMethod = AccessTools.Method(typeof(OniExtract_Game_EntityConfig), nameof(OniExtract_Game_EntityConfig.RegisterPatch));
            static ExportEntity exportEntity = new ExportEntity();

            public static GameObject RegisterPatch(GameObject gameObject)
            {
                KPrefabID prefabID = gameObject.GetComponent<KPrefabID>();
                BEntity bEntity = new BEntity(prefabID.PrefabID().Name, gameObject.GetComponent<KPrefabID>().Tags);
                ExportEntity.LoadEntityComponent(gameObject, bEntity);
                exportEntity.entities.Add(bEntity);
                exportEntity.ExportJsonFile();
                return gameObject;
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

            }
        }
    }
}
