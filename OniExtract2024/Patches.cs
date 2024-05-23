using HarmonyLib;
using System.Collections.Generic;
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
            private static void Postfix()
            {

            }
        }
    }
}
