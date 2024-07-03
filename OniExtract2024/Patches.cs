using HarmonyLib;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace OniExtract2024
{
    public class Patches
    {
        public static ExportCodex exportCodex = new ExportCodex();

        [HarmonyPatch(typeof(Klei.AI.Effect), "AddModifierDescriptions")]
        [HarmonyPatch(new Type[] { typeof(GameObject), typeof(List<Descriptor>), typeof(string), typeof(bool) })]
        internal class OniExtract_Game_EffectDesc
        {
            private static void Prefix(GameObject parent, List<Descriptor> descs, string effect_id, bool increase_indent)
            {
                //Debug.Log("OniExtract: " + "Export EffectDesc");
                exportCodex.AddEffectDesc(parent, descs, effect_id, increase_indent);
                exportCodex.ExportJsonFile();
            }
        }

        [HarmonyPatch(typeof(CodexCache), "CodexCacheInit")]
        internal class OniExtract_Game_Codex
        {
            private static void Postfix()
            {
                //Debug.Log("OniExtract: " + "Export Codex");
                exportCodex.AddCategoryEntry(CodexCache.entries);
                exportCodex.AddCategoryTree(CodexCache.entries);
                exportCodex.ExportJsonFile();
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
