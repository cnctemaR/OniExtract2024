using System.Collections.Generic;

namespace OniExtract2024
{
    public class BMultiEntity
    {
        public string name;
        public string nameString;
        public string entityType;
        public BKprefabID kPrefabID;
        public BVector2 kBoxCollider2D = null;
        public OutOccupyArea occupyArea = null;
        public OutDecorProvider decorProvider = null;
        public OutPickupable pickupable = null;
        public OutPrimaryElement primaryElement = null;
        public InfoDescription infoDescription = null;
        public HarvestablePOIConfigurator harvestablePOIConfigurator = null;
        public HarvestablePOIConfigurator.HarvestablePOIType harvestablePOIType = null;
        public HarvestablePOIClusterGridEntity harvestablePOIClusterGridEntity = null;
        public ArtifactPOIConfigurator artifactPOIConfigurator = null;
        public ArtifactPOIConfigurator.ArtifactPOIType artifactPOIType = null;
        public OutSpaceArtifact spaceArtifact = null;
        public OutLight2D light2D = null;
        public GeyserConfigurator.GeyserType geyserType = null;
        public Studyable studyable = null;
        public ClusterDestinationSelector clusterDestinationSelector = null;
        public OutClusterMapMeteorShowerVisualizer clusterMapMeteorShowerVisualizer = null;
        public OutClusterTraveler clusterTraveler = null;
        public ClusterMapMeteorShower.Def clusterMapMeteorShowerDef = null;
        public OutMeteorShowerEvent meteorShowerEvent = null;
        public ArtifactPOIClusterGridEntity artifactPOIClusterGridEntity = null;
        public RadiationEmitter radiationEmitter = null;

        public BMultiEntity(string name, KPrefabID kPrefabID)
        {
            this.name = name;
            this.kPrefabID = new BKprefabID(kPrefabID);
        }
    }
}
