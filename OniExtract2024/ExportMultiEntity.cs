using System;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using OniExtract2024;
using Klei.AI;

public class ExportMultiEntity : BaseExport
{
    public override string ExportFileName { get; set; } = "multiEntities";
    public List<BMultiEntity> multiEntities;

    public ExportMultiEntity()
    {
        multiEntities = new List<BMultiEntity>();
    }

    public static void LoadEntityComponent(GameObject gameObject, BMultiEntity bEntity)
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
        else
        {
            bEntity.primaryElement = null;
        }
        InfoDescription infoDescription = gameObject.GetComponent<InfoDescription>();
        if (infoDescription != null)
        {
            bEntity.infoDescription = infoDescription;
        }
        else
        {
            bEntity.infoDescription = null;
        }
        HarvestablePOIConfigurator harvestablePOIConfigurator = gameObject.GetComponent<HarvestablePOIConfigurator>();
        if (harvestablePOIConfigurator != null)
        {
            bEntity.harvestablePOIConfigurator = harvestablePOIConfigurator;
            bEntity.harvestablePOIType = HarvestablePOIConfigurator.FindType(harvestablePOIConfigurator.presetType);
        }
        else
        {
            bEntity.harvestablePOIConfigurator = null;
            bEntity.harvestablePOIType = null;
        }
        HarvestablePOIClusterGridEntity harvestablePOIClusterGridEntity = gameObject.GetComponent<HarvestablePOIClusterGridEntity>();
        if (harvestablePOIClusterGridEntity != null)
        {
            bEntity.harvestablePOIClusterGridEntity = harvestablePOIClusterGridEntity;
        }
        else
        {
            bEntity.harvestablePOIClusterGridEntity = null;
        }
        ArtifactPOIConfigurator artifactPOIConfigurator = gameObject.GetComponent<ArtifactPOIConfigurator>();
        if (artifactPOIConfigurator != null)
        {
            bEntity.artifactPOIConfigurator = artifactPOIConfigurator;
        }
        else
        {
            bEntity.artifactPOIConfigurator = null;
        }
        ClusterDestinationSelector clusterDestinationSelector = gameObject.GetComponent<ClusterDestinationSelector>();
        if (clusterDestinationSelector != null)
        {
            bEntity.clusterDestinationSelector = clusterDestinationSelector;
        }
        else
        {
            bEntity.clusterDestinationSelector = null;
        }
        ClusterMapMeteorShowerVisualizer clusterMapMeteorShowerVisualizer = gameObject.GetComponent<ClusterMapMeteorShowerVisualizer>();
        if (clusterMapMeteorShowerVisualizer != null)
        {
            bEntity.clusterMapMeteorShowerVisualizer = new OutClusterMapMeteorShowerVisualizer(clusterMapMeteorShowerVisualizer);
        }
        else
        {
            bEntity.clusterMapMeteorShowerVisualizer = null;
        }
        ClusterTraveler clusterTraveler = gameObject.GetComponent<ClusterTraveler>();
        if (clusterTraveler != null)
        {
            bEntity.clusterTraveler = new OutClusterTraveler(clusterTraveler);
        }
        else
        {
            bEntity.clusterTraveler = null;
        }
        ClusterMapMeteorShower.Def clusterMapMeteorShowerDef = gameObject.GetDef<ClusterMapMeteorShower.Def>();
        if (clusterMapMeteorShowerDef != null)
        {
            bEntity.clusterMapMeteorShowerDef = clusterMapMeteorShowerDef;
        }
        else
        {
            bEntity.clusterMapMeteorShowerDef = null;
        }
        ArtifactPOIClusterGridEntity artifactPOIClusterGridEntity = gameObject.GetComponent<ArtifactPOIClusterGridEntity>();
        if (artifactPOIClusterGridEntity != null)
        {
            bEntity.artifactPOIClusterGridEntity = artifactPOIClusterGridEntity;
        }
        else
        {
            bEntity.artifactPOIClusterGridEntity = null;
        }
    }
}
