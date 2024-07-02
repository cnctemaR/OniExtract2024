using System.Collections.Generic;

namespace OniExtract2024
{
    public class BBuildingEntity
    {
        public string name;
        public string nameString;
        public HashSet<Tag> tags;
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

        public BBuildingEntity(string name, HashSet<Tag> tags)
        {
            this.name = name;
            this.tags = tags;
        }
    }
}