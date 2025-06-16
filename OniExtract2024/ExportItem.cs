using OniExtract2024;
using System.Collections.Generic;
using UnityEngine;

public class ExportItem : BaseExport
{
    public override string ExportFileName { get; set; } = "items";
    public List<EquipmentDef> EquipmentDefs = new List<EquipmentDef>();
    public List<BEgg> eggs = new List<BEgg>();
    public List<BSeed> seeds = new List<BSeed>();
    public List<BEquipment> equipments = new List<BEquipment>();

    public ExportItem()
    {
    }

    public void AddEquipmentDef(IEquipmentConfig config)
    {
        if (!DlcManager.IsDlcListValidForCurrentContent(config.GetDlcIds()))
        {
            return;
        }
        this.EquipmentDefs.Add(config.CreateEquipmentDef());
    }

    public void AddEgg(GameObject gameObject, BEgg bEntity)
    {
        KBoxCollider2D kBoxCollider2D = gameObject.GetComponent<KBoxCollider2D>();
        if (kBoxCollider2D != null)
        {
            bEntity.kBoxCollider2D = new BVector2(kBoxCollider2D.size);
        }
        KSelectable selectable = gameObject.GetComponent<KSelectable>();
        if (selectable != null)
        {
            bEntity.nameString = selectable.entityName;
        }
        IncubationMonitor.Def incubatorMonitorDef = gameObject.GetDef<IncubationMonitor.Def>();
        if (incubatorMonitorDef != null)
        {
            bEntity.incubatorMonitorDef = new OutIncubationMonitorDef(incubatorMonitorDef);
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
        this.eggs.Add(bEntity);
    }

    public void AddSeed(GameObject gameObject, BSeed bEntity)
    {
        KBoxCollider2D kBoxCollider2D = gameObject.GetComponent<KBoxCollider2D>();
        if (kBoxCollider2D != null)
        {
            bEntity.kBoxCollider2D = new BVector2(kBoxCollider2D.size);
        }
        KSelectable selectable = gameObject.GetComponent<KSelectable>();
        if (selectable != null)
        {
            bEntity.nameString = selectable.entityName;
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
        PlantableSeed plantableSeed = gameObject.GetComponent<PlantableSeed>();
        if (primaryElement != null)
        {
            bEntity.plantableSeed = plantableSeed;
        }
        MutantPlant mutantPlant = gameObject.GetComponent<MutantPlant>();
        if (mutantPlant != null)
        {
            bEntity.mutantPlant = mutantPlant;
        }
        this.seeds.Add(bEntity);
    }

    public void AddEquipment(GameObject gameObject, BEquipment bEntity)
    {
        KBoxCollider2D kBoxCollider2D = gameObject.GetComponent<KBoxCollider2D>();
        if (kBoxCollider2D != null)
        {
            bEntity.kBoxCollider2D = new BVector2(kBoxCollider2D.size);
        }
        KSelectable selectable = gameObject.GetComponent<KSelectable>();
        if (selectable != null)
        {
            bEntity.nameString = selectable.entityName;
        }
        SuitTank suitTank = gameObject.GetComponent<SuitTank>();
        if (suitTank != null)
        {
            bEntity.suitTank = new OutSuitTank(suitTank);
        }
        Durability durability = gameObject.GetComponent<Durability>();
        if (durability != null)
        {
            bEntity.durability = durability;
        }
        LeadSuitTank leadSuitTank = gameObject.GetComponent<LeadSuitTank>();
        if (leadSuitTank != null)
        {
            bEntity.leadSuitTank = leadSuitTank;
        }
        Storage storage = gameObject.GetComponent<Storage>();
        if (storage != null)
        {
            bEntity.storage = new OutStorage(storage);
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
        this.equipments.Add(bEntity);
    }
}
