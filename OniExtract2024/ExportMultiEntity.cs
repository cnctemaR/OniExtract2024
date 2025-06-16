using System.Collections.Generic;
using UnityEngine;
using OniExtract2024;
using Klei.AI;

public class ExportMultiEntity : BaseExport
{
    public override string ExportFileName { get; set; } = "multiEntities";
    public List<BMultiEntity> multiEntities;
    public Dictionary<string, OutMeteorShowerEvent> meteorShowerEventMap = new Dictionary<string, OutMeteorShowerEvent>();

    public ExportMultiEntity()
    {
        multiEntities = new List<BMultiEntity>();
    }

    public void addNewMeteorShowerEvent(OutMeteorShowerEvent obj)
    {
        if(!meteorShowerEventMap.ContainsKey(obj.id))
        {
            meteorShowerEventMap.Add(obj.id, obj);
        }
    }

    public void updateAllMeteorShowEvent()
    {
        foreach(var keyValuePair in meteorShowerEventMap)
        {
            GameplayEvent gplay = Db.Get().GameplayEvents.Get(keyValuePair.Key);
            if (gplay != null)
            {
                var gameplayEvent = gplay as MeteorShowerEvent;
                var meteorShowerEvent = keyValuePair.Value;
                meteorShowerEvent.bombardmentInfo = gameplayEvent.GetMeteorsInfo();
                meteorShowerEvent.animFileName = gameplayEvent.animFileName;
                meteorShowerEvent.tags = gameplayEvent.tags;
                meteorShowerEvent.Name = gameplayEvent.Name;
                meteorShowerEvent.IdHash = gameplayEvent.IdHash;
                meteorShowerEvent.allowMultipleEventInstances = gameplayEvent.allowMultipleEventInstances;
                meteorShowerEvent.numTimesAllowed = gameplayEvent.numTimesAllowed;
            }
        }
    }

    public void LoadEntityComponent(GameObject gameObject, BMultiEntity bEntity)
    {
        KBoxCollider2D kBoxCollider2D = gameObject.GetComponent<KBoxCollider2D>();
        if (kBoxCollider2D != null)
        {
            bEntity.kBoxCollider2D = new BVector2(kBoxCollider2D.size);
        }
        OccupyArea occupyArea = gameObject.GetComponent<OccupyArea>();
        if (occupyArea != null)
        {
            bEntity.occupyArea = new OutOccupyArea(occupyArea);
        }
        DecorProvider decorProvider = gameObject.GetComponent<DecorProvider>();
        if (decorProvider != null)
        {
            bEntity.decorProvider = new OutDecorProvider(decorProvider);
        }
        Pickupable pickupable = gameObject.GetComponent<Pickupable>();
        if (pickupable != null)
        {
            bEntity.pickupable = new OutPickupable(pickupable);
        }
        PrimaryElement primaryElement = gameObject.GetComponent<PrimaryElement>();
        if (primaryElement != null)
        {
            bEntity.primaryElement = new OutPrimaryElement(primaryElement);
        }
        InfoDescription infoDescription = gameObject.GetComponent<InfoDescription>();
        if (infoDescription != null)
        {
            bEntity.infoDescription = infoDescription;
        }
        HarvestablePOIConfigurator harvestablePOIConfigurator = gameObject.GetComponent<HarvestablePOIConfigurator>();
        if (harvestablePOIConfigurator != null)
        {
            bEntity.harvestablePOIConfigurator = harvestablePOIConfigurator;
            bEntity.harvestablePOIType = HarvestablePOIConfigurator.FindType(harvestablePOIConfigurator.presetType);
        }
        HarvestablePOIClusterGridEntity harvestablePOIClusterGridEntity = gameObject.GetComponent<HarvestablePOIClusterGridEntity>();
        if (harvestablePOIClusterGridEntity != null)
        {
            bEntity.harvestablePOIClusterGridEntity = harvestablePOIClusterGridEntity;
        }
        ArtifactPOIConfigurator artifactPOIConfigurator = gameObject.GetComponent<ArtifactPOIConfigurator>();
        if (artifactPOIConfigurator != null)
        {
            bEntity.artifactPOIConfigurator = artifactPOIConfigurator;
            bEntity.artifactPOIType = ArtifactPOIConfigurator.FindType(artifactPOIConfigurator.presetType);
        }
        SpaceArtifact spaceArtifact = gameObject.GetComponent<SpaceArtifact>();
        if (spaceArtifact != null)
        {
            bEntity.spaceArtifact = new OutSpaceArtifact(spaceArtifact);
        }
        Light2D light2D = gameObject.GetComponent<Light2D>();
        if (light2D != null)
        {
            bEntity.light2D = new OutLight2D(light2D);
        }
        GeyserConfigurator geyserConfigurator = gameObject.GetComponent<GeyserConfigurator>();
        if (geyserConfigurator != null)
        {
            bEntity.geyserType = GeyserConfigurator.FindType(geyserConfigurator.presetType);
        }
        Studyable studyable = gameObject.GetComponent<Studyable>();
        if (studyable != null)
        {
            bEntity.studyable = studyable;
        }
        ClusterDestinationSelector clusterDestinationSelector = gameObject.GetComponent<ClusterDestinationSelector>();
        if (clusterDestinationSelector != null)
        {
            bEntity.clusterDestinationSelector = clusterDestinationSelector;
        }
        ClusterMapMeteorShowerVisualizer clusterMapMeteorShowerVisualizer = gameObject.GetComponent<ClusterMapMeteorShowerVisualizer>();
        if (clusterMapMeteorShowerVisualizer != null)
        {
            bEntity.clusterMapMeteorShowerVisualizer = new OutClusterMapMeteorShowerVisualizer(clusterMapMeteorShowerVisualizer);
        }
        ClusterTraveler clusterTraveler = gameObject.GetComponent<ClusterTraveler>();
        if (clusterTraveler != null)
        {
            bEntity.clusterTraveler = new OutClusterTraveler(clusterTraveler);
        }
        ClusterMapMeteorShower.Def clusterMapMeteorShowerDef = gameObject.GetDef<ClusterMapMeteorShower.Def>();
        if (clusterMapMeteorShowerDef != null)
        {
            bEntity.clusterMapMeteorShowerDef = clusterMapMeteorShowerDef;
            if (meteorShowerEventMap.ContainsKey(clusterMapMeteorShowerDef.eventID))
            {
                OutMeteorShowerEvent meteorShowerEvent = meteorShowerEventMap[clusterMapMeteorShowerDef.eventID];
                bEntity.meteorShowerEvent = meteorShowerEvent;
            }
        }
        ArtifactPOIClusterGridEntity artifactPOIClusterGridEntity = gameObject.GetComponent<ArtifactPOIClusterGridEntity>();
        if (artifactPOIClusterGridEntity != null)
        {
            bEntity.artifactPOIClusterGridEntity = artifactPOIClusterGridEntity;
        }
        RadiationEmitter radiationEmitter = gameObject.GetComponent<RadiationEmitter>();
        if (radiationEmitter != null)
        {
            bEntity.radiationEmitter = radiationEmitter;
        }
        
    }
}
