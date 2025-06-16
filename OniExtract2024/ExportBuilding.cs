using System.Collections.Generic;
using UnityEngine;
using OniExtract2024;
using System.Linq;

public class ExportBuilding : BaseExport
{
    public override string ExportFileName { get; set; } = "building";
    public List<BuildingDef> buildingDefs = new List<BuildingDef>();
    public List<BBuildingEntity> bBuildingDefList = new List<BBuildingEntity>();
    public List<BuildMenuCategory> buildMenuCategories = new List<BuildMenuCategory>();
    public Dictionary<string, List<KeyValuePair<string, string>>> buildingAndSubcategoryDataPairs = new Dictionary<string, List<KeyValuePair<string, string>>>();
    public List<Tag> roomConstraintTags= new List<Tag>();
    public Dictionary<string, string> requiredSkillPerkMap = new Dictionary<string, string>();

    public ExportBuilding()
    {
    }

    public void AddNewBuildingDef(BuildingDef buildingDef)
    {
        buildingDef.BlockTileMaterial = null;
        this.buildingDefs.Add(buildingDef);
        this.roomConstraintTags = RoomConstraints.ConstraintTags.AllTags;
    }

    public void AddNewBuildingEntity(BuildingDef buildingDef)
    {
        GameObject go = buildingDef.BuildingComplete;
        KPrefabID prefabID = go.GetComponent<KPrefabID>();
        BBuildingEntity bBuild = new BBuildingEntity(buildingDef.Tag.Name, prefabID);
        EnergyGenerator energyGenerator = go.GetComponent<EnergyGenerator>();
        if (energyGenerator != null)
        {
            bBuild.energyGenerator = new OutEnergyGenerator(energyGenerator);
        }
        else
        {
            bBuild.energyGenerator = null;
        }
        ConduitConsumer conduitConsumer = go.GetComponent<ConduitConsumer>();
        if (conduitConsumer != null)
        {
            bBuild.conduitConsumer = new OutConduitConsumer(conduitConsumer);
        }
        else
        {
            bBuild.conduitConsumer = null;
        }
        ConduitDispenser conduitDispenser = go.GetComponent<ConduitDispenser>();
        if (conduitDispenser != null)
        {
            bBuild.conduitDispenser = new OutConduitDispenser(conduitDispenser);
        }
        else
        {
            bBuild.conduitDispenser = null;
        }
        ElementConverter[] elementConverters = go.GetComponents<ElementConverter>();
        if (elementConverters != null && elementConverters.Length > 0)
        {
            foreach (var elementConverter in elementConverters)
            {
                bBuild.elementConverters.Add(new OutElementConverter(elementConverter));
            }
        }
        PlantablePlot plantablePlot = go.GetComponent<PlantablePlot>();
        if (plantablePlot != null)
        {
            bBuild.plantablePlot = new OutPlantablePlot(plantablePlot);
        }
        else
        {
            bBuild.plantablePlot = null;
        }
        ElementConsumer[] elementConsumers = go.GetComponents<ElementConsumer>();
        if (elementConsumers != null && elementConsumers.Length > 0)
        {
            foreach (var elementConsumer in elementConsumers)
            {
                bBuild.elementConsumers.Add(new OutElementConsumer(elementConsumer));
            }
        }
        PassiveElementConsumer[] passiveElementConsumers = go.GetComponents<PassiveElementConsumer>();
        if (passiveElementConsumers != null && passiveElementConsumers.Length > 0)
        {
            foreach (var passiveElementConsumer in passiveElementConsumers)
            {
                bBuild.passiveElementConsumers.Add(new OutPassiveElementConsumer(passiveElementConsumer));
            }
        }
        Storage storage = go.GetComponent<Storage>();
        if (storage != null)
        {
            bBuild.storage = new OutStorage(storage);
        }
        AttachableBuilding attachableBuilding = go.GetComponent<AttachableBuilding>();
        if (attachableBuilding != null)
        {
            bBuild.attachableBuilding = attachableBuilding;
        }
        BuildingAttachPoint buildingAttachPoint = go.GetComponent<BuildingAttachPoint>();
        if (buildingAttachPoint != null)
        {
            bBuild.buildingAttachPoint = buildingAttachPoint;
        }
        RocketModule rocketModule = go.GetComponent<RocketModule>();
        if (rocketModule != null)
        {
            bBuild.rocketModule = rocketModule;
        }
        ReorderableBuilding reorderableBuilding = go.GetComponent<ReorderableBuilding>();
        if (reorderableBuilding != null)
        {
            bBuild.reorderableBuilding = reorderableBuilding;
        }
        RocketEngineCluster rocketEngineCluster = go.GetComponent<RocketEngineCluster>();
        if (rocketEngineCluster != null)
        {
            bBuild.rocketEngineCluster = new OutRocketEngineCluster(rocketEngineCluster);
        }
        RocketModuleCluster rocketModuleCluster = go.GetComponent<RocketModuleCluster>();
        if (rocketModuleCluster != null)
        {
            bBuild.rocketModuleCluster = rocketModuleCluster;
        }
        RocketEngine rocketEngine = go.GetComponent<RocketEngine>();
        if (rocketEngine != null)
        {
            bBuild.rocketEngine = new OutRocketEngine(rocketEngine);
        }
        PassengerRocketModule passengerRocketModule = go.GetComponent<PassengerRocketModule>();
        if (passengerRocketModule != null)
        {
            bBuild.passengerRocketModule = passengerRocketModule;
        }
        CargoBay cargoBay = go.GetComponent<CargoBay>();
        if (cargoBay != null)
        {
            bBuild.cargoBay = new OutCargoBay(cargoBay);
        }
        CargoBayConduit cargoBayConduit = go.GetComponent<CargoBayConduit>();
        if (cargoBayConduit != null)
        {
            bBuild.cargoBayConduit = cargoBayConduit;
        }
        CargoBayCluster cargoBayCluster = go.GetComponent<CargoBayCluster>();
        if (cargoBayCluster != null)
        {
            bBuild.cargoBayCluster = new OutCargoBayCluster(cargoBayCluster);
        }
        TreeFilterable treeFilterable = go.GetComponent<TreeFilterable>();
        if (treeFilterable != null)
        {
            bBuild.treeFilterable = new OutTreeFilterable(treeFilterable);
        }
        Deconstructable deconstructable = go.GetComponent<Deconstructable>();
        if (deconstructable != null)
        {
            bBuild.deconstructable = deconstructable;
        }
        Demolishable demolishable = go.GetComponent<Demolishable>();
        if (demolishable != null)
        {
            bBuild.demolishable = demolishable;
        }   
        Workable[] workableComponents = go.GetComponents<Workable>();
        var derivedWorkables = workableComponents.Where(component => component.GetType() != typeof(Workable) && component.GetType().IsSubclassOf(typeof(Workable)));
        foreach (var workable in derivedWorkables)
        {
            if (workable != null && workable.requiredSkillPerk != null && workable.requiredSkillPerk != "")
            {
                this.requiredSkillPerkMap.Add(buildingDef.Tag.Name, workable.requiredSkillPerk);
            }
        }
        Battery battery = go.GetComponent<Battery>();
        if (battery != null)
        {
            bBuild.battery = new OutBattery(battery);
        }
        RoomTracker roomTracker = go.GetComponent<RoomTracker>();
        if (roomTracker != null)
        {
            bBuild.roomTracker = roomTracker;
        }
        RocketUsageRestriction.Def rocketUsage = go.GetDef<RocketUsageRestriction.Def>();
        if (rocketUsage != null)
        {
            bBuild.rocketUsageRestrictionDef = rocketUsage;
        }

        this.bBuildingDefList.Add(bBuild);
    }

    public void ExportBuildMenu()
    {
        foreach (var planOrder in TUNING.BUILDINGS.PLANORDER)
        {
            string icon_name = PlanScreen.IconNameMap[planOrder.category];
            string categoryName = HashCache.Get().Get(planOrder.category);
            this.buildMenuCategories.Add(new BuildMenuCategory()
            {
                category = planOrder.category.HashValue,
                categoryName = categoryName,
                categoryIcon = icon_name
            });
            this.buildingAndSubcategoryDataPairs[categoryName] = planOrder.buildingAndSubcategoryData;
        }
    }
}
