using System.Collections.Generic;

namespace OniExtract2024
{
    public class BMultiEntity
    {
        public string name;
        public string nameString;
        public string entityType;
        public HashSet<Tag> tags;
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
        public ClusterDestinationSelector clusterDestinationSelector = null;
        public OutClusterMapMeteorShowerVisualizer clusterMapMeteorShowerVisualizer = null;
        public OutClusterTraveler clusterTraveler = null;
        public ClusterMapMeteorShower.Def clusterMapMeteorShowerDef = null;
        public ArtifactPOIClusterGridEntity artifactPOIClusterGridEntity = null;

        public BMultiEntity(string name, HashSet<Tag> tags)
        {
            this.name = name;
            this.tags = tags;
        }
    }
}
