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
        static ExportEntity exportEntity = new ExportEntity();

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
                exportEntity.ExportJsonFile();
            }
        }
    }
}
