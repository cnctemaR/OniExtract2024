using HarmonyLib;

namespace OniExtract2024
{
    public class Patches
    {
        [HarmonyPatch(typeof(LegacyModMain), "Load")]
        internal class OniExtract_Game_LegacyModMain
        {
            private static void Postfix()
            {

                //Debug.Log("OniExtract: " + "Export Buildings");
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
        }
    }
}
