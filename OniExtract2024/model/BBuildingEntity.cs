using System.Collections.Generic;

namespace OniExtract2024
{
    public class BBuildingEntity
    {
        public string name;
        public string nameString;
        public BKprefabID kPrefabID;
        public HashSet<Tag> tags;

        // BuildingDef display fields (missing from original 2024 export)
        public int widthInCells;
        public int heightInCells;
        public string[] materialCategory;
        public float[] materialMass;
        public bool isFoundation;
        public bool isKAnimTile;
        public bool isUtility;
        public bool dragBuild;
        public int buildLocationRule;
        public int permittedRotations;
        public int sceneLayer;
        public int objectLayer;
        public string viewMode;
        public string defaultAnimState;
        public string uiSpriteName;
        public OutEnergyGenerator energyGenerator;
        public OutConduitConsumer conduitConsumer;
        public OutConduitDispenser conduitDispenser;
        public OutPlantablePlot plantablePlot;
        public List<OutElementConverter> elementConverters = new List<OutElementConverter>();
        public List<OutElementConsumer> elementConsumers = new List<OutElementConsumer>();
        public List<OutPassiveElementConsumer> passiveElementConsumers = new List<OutPassiveElementConsumer>();
        public OutStorage storage = null;
        public AttachableBuilding attachableBuilding = null;
        public BuildingAttachPoint buildingAttachPoint = null;
        public RocketModule rocketModule = null;
        public ReorderableBuilding reorderableBuilding = null;
        public OutRocketEngineCluster rocketEngineCluster = null;
        public RocketModuleCluster rocketModuleCluster = null;
        public OutRocketEngine rocketEngine = null;
        public PassengerRocketModule passengerRocketModule = null;
        public OutCargoBay cargoBay = null;
        public CargoBayConduit cargoBayConduit = null;
        public OutCargoBayCluster cargoBayCluster = null;
        public OutTreeFilterable treeFilterable = null;
        public Deconstructable deconstructable = null;
        public Demolishable demolishable = null;
        public OutBattery battery = null;
        public RoomTracker roomTracker = null;
        public RocketUsageRestriction.Def rocketUsageRestrictionDef = null;

        public BBuildingEntity(string name, KPrefabID kPrefabID)
        {
            this.name = name;
            this.nameString = kPrefabID.GetProperName();
            this.tags = kPrefabID.Tags;
            this.kPrefabID = new BKprefabID(kPrefabID);
        }
    }
}
