using HarmonyLib;
using UnityEngine;

namespace OniExtract2024
{
    public class Patches
    {
        [HarmonyPatch(typeof(EntityConfigManager), "RegisterEntity")]
        internal class OniExtract_Game_EntityConfig
        {
            static ExportEntity exportEntity = new ExportEntity();
            static void Postfix(IEntityConfig config)
            {
                Debug.Log("OniExtract: " + "Export Entity");
                GameObject gameObject = config.CreatePrefab();
                KPrefabID prefabID = gameObject.GetComponent<KPrefabID>();
                Debug.Log(prefabID.PrefabID().Name);
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
