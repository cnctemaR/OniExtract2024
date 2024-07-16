using System;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using OniExtract2024;
using Klei.AI;

public class ExportEntity : BaseExport
{
    public override string ExportFileName { get; set; } = "entities";
    public List<BEntity> entities;

    public ExportEntity()
    {
        entities = new List<BEntity>();
    }

    public static void LoadEntityComponent(GameObject gameObject, BEntity bEntity)
    {
        KBoxCollider2D kBoxCollider2D = gameObject.GetComponent<KBoxCollider2D>();
        if (kBoxCollider2D != null)
        {
            bEntity.kBoxCollider2D = new BVector2(kBoxCollider2D.size);
        }
        TemperatureVulnerable tv = gameObject.GetComponent<TemperatureVulnerable>();
        if (tv != null)
        {
            bEntity.temperatureVulnerable = new OutTemperatureVulnerable(tv);
        }
        PressureVulnerable pressureVulnerable = gameObject.GetComponent<PressureVulnerable>();
        if (pressureVulnerable != null)
        {
            bEntity.pressureVulnerable = new OutPressureVulnerable(pressureVulnerable);
        }
        DrowningMonitor drowningMonitor = gameObject.GetComponent<DrowningMonitor>();
        if (drowningMonitor != null)
        {
            bEntity.drowningMonitor = new OutDrowningMonitor(drowningMonitor);
        }
        DecorProvider decorProvider = gameObject.GetComponent<DecorProvider>();
        if (decorProvider != null)
        {
            bEntity.decorProvider = new OutDecorProvider(decorProvider);
        }
        Crop crop = gameObject.GetComponent<Crop>();
        if (crop != null)
        {
            bEntity.cropVal = crop.cropVal;
        }
        else
        {
            bEntity.cropVal = null;
        }
        MutantPlant mutantPlant = gameObject.GetComponent<MutantPlant>();
        if (mutantPlant != null)
        {
            bEntity.mutantPlant = new OutMutantPlant(mutantPlant);
        }
        else
        {
            bEntity.mutantPlant = null;
        }
        StandardCropPlant standardCropPlant = gameObject.GetComponent<StandardCropPlant>();
        if (standardCropPlant != null)
        {
            bEntity.isStandardCropPlant = true;
        }
        else
        {
            bEntity.isStandardCropPlant = false;
        }
        Storage storage = gameObject.GetComponent<Storage>();
        if (storage != null)
        {
            bEntity.storage = new OutStorage(storage);
        }
        else
        {
            bEntity.storage = null;
        }
        ElementConsumer elementConsumer = gameObject.GetComponent<ElementConsumer>();
        if (elementConsumer != null)
        {
            bEntity.elementConsumer = new OutElementConsumer(elementConsumer);
        }
        else
        {
            bEntity.elementConsumer = null;
        }
        ManualDeliveryKG[] manualDeliveryKGs = gameObject.GetComponents<ManualDeliveryKG>();
        bEntity.manualDeliveryKG_Num = manualDeliveryKGs.Length;
        foreach (ManualDeliveryKG manualDeliveryKG in manualDeliveryKGs)
        {
            OutManualDeliveryKG outManualDeliveryKG = new OutManualDeliveryKG(manualDeliveryKG);
            bEntity.manualDeliveryKGs.Add(outManualDeliveryKG);
        }
        FertilizationMonitor.Def fertilizationDef = gameObject.GetDef<FertilizationMonitor.Def>();
        if (fertilizationDef != null)
        {
            bEntity.fertilizationDef = fertilizationDef;
        }
        else
        {
            bEntity.fertilizationDef = null;
        }
        IrrigationMonitor.Def irrigationDef = gameObject.GetDef<IrrigationMonitor.Def>();
        if (irrigationDef != null)
        {
            bEntity.irrigationDef = irrigationDef;
        }
        else
        {
            bEntity.irrigationDef = null;
        }
        Growing growing = gameObject.GetComponent<Growing>();
        if (growing != null)
        {
            bEntity.growing = new OutGrowing(growing);
        }
        else
        {
            bEntity.growing = null;
        }
        SeedProducer seedProducer = gameObject.GetComponent<SeedProducer>();
        if (seedProducer != null)
        {
            bEntity.seedInfo = seedProducer.seedInfo;
        }
        else
        {
            bEntity.seedInfo = null;
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
        IlluminationVulnerable illuminationVulnerable = gameObject.GetComponent<IlluminationVulnerable>();
        if (illuminationVulnerable != null)
        {
            bEntity.illuminationVulnerable = new OutIlluminationVulnerable(illuminationVulnerable);
        }
        else
        {
            bEntity.illuminationVulnerable = null;
        }
        BlightVulnerable blightVulnerable = gameObject.GetComponent<BlightVulnerable>();
        if (blightVulnerable != null)
        {
            bEntity.blightVulnerable = new OutBlightVulnerable(blightVulnerable);
        }
        else
        {
            bEntity.blightVulnerable = null;
        }
        PrickleGrass prickleGrass = gameObject.GetComponent<PrickleGrass>();
        EvilFlower evilFlower = gameObject.GetComponent<EvilFlower>();
        if (prickleGrass != null)
        {
            bEntity.decorToggler = new OutDecorToggler(prickleGrass);
        }
        else if (evilFlower != null)
        {
            bEntity.decorToggler = new OutDecorToggler(evilFlower);
        }
        else
        {
            bEntity.decorToggler = null;
        }
        WiltCondition wiltable = gameObject.GetComponent<WiltCondition>();
        if (wiltable != null)
        {
            Modifiers wiltConditions = gameObject.GetComponent<Modifiers>();
            bEntity.wiltConditions = new OutWiltConditions(wiltable, wiltConditions);
        }
        else
        {
            bEntity.wiltConditions = null;
        }
        //Critter
        CreatureBrain creatureBrain = gameObject.GetComponent<CreatureBrain>();
        if (creatureBrain != null)
        {
            bEntity.creatureBrain = creatureBrain;
        }
        CritterTemperatureMonitor.Def critterTemperatureMonitorDef = gameObject.GetDef<CritterTemperatureMonitor.Def>();
        if (critterTemperatureMonitorDef != null)
        {
            bEntity.critterTemperatureMonitorDef = critterTemperatureMonitorDef;
        }
        DiseaseSourceVisualizer diseaseSourceVisualizer = gameObject.GetComponent<DiseaseSourceVisualizer>();
        if (diseaseSourceVisualizer != null)
        {
            bEntity.diseaseSourceVisualizer = diseaseSourceVisualizer;
        }
        else
        {
            bEntity.diseaseSourceVisualizer = null;
        }
        LureableMonitor.Def lureableMonitorDef = gameObject.GetDef<LureableMonitor.Def>();
        if (lureableMonitorDef != null)
        {
            bEntity.lureableMonitorDef = lureableMonitorDef;
        }
        else
        {
            bEntity.lureableMonitorDef = null;
        }
        ThreatMonitor.Def threatMonitorDef = gameObject.GetDef<ThreatMonitor.Def>();
        if (threatMonitorDef != null)
        {
            bEntity.threatMonitorDef = threatMonitorDef;
        }
        else
        {
            bEntity.threatMonitorDef = null;
        }
        SubmergedMonitor.Def submergedMonitorDef = gameObject.GetDef<SubmergedMonitor.Def>();
        if (submergedMonitorDef != null)
        {
            bEntity.isHasSubmergedMonitorDef = true;
        }
        else
        {
            bEntity.isHasSubmergedMonitorDef = false;
        }
        GasAndLiquidConsumerMonitor.Def gasAndLiquidConsumerMonitorDef = gameObject.GetDef<GasAndLiquidConsumerMonitor.Def>();
        if (gasAndLiquidConsumerMonitorDef != null)
        {
            bEntity.gasAndLiquidConsumerMonitorDef = gasAndLiquidConsumerMonitorDef;
        }
        else
        {
            bEntity.gasAndLiquidConsumerMonitorDef = null;
        }
        CreatureCalorieMonitor.Def creatureCalorieMonitorDef = gameObject.GetDef<CreatureCalorieMonitor.Def>();
        if (creatureCalorieMonitorDef != null)
        {
            bEntity.creatureCalorieMonitorDef = creatureCalorieMonitorDef;
        }
        else
        {
            bEntity.creatureCalorieMonitorDef = null;
        }
        AgeMonitor.Def ageMonitorDef = gameObject.GetDef<AgeMonitor.Def>();
        if (ageMonitorDef != null)
        {
            bEntity.ageMonitorDef = ageMonitorDef;
        }
        else
        {
            bEntity.ageMonitorDef = null;
        }
        HappinessMonitor.Def happinessMonitorDef = gameObject.GetDef<HappinessMonitor.Def>();
        if (happinessMonitorDef != null)
        {
            bEntity.happinessMonitorDef = happinessMonitorDef;
        }
        else
        {
            bEntity.happinessMonitorDef = null;
        }
        WildnessMonitor.Def wildnessMonitorDef = gameObject.GetDef<WildnessMonitor.Def>();
        if (wildnessMonitorDef != null)
        {
            bEntity.wildnessMonitorDef = wildnessMonitorDef;
        }
        else
        {
            bEntity.wildnessMonitorDef = null;
        }

        OvercrowdingMonitor.Def overcrowdingMonitorDef = gameObject.GetDef<OvercrowdingMonitor.Def>();
        if (overcrowdingMonitorDef != null)
        {
            bEntity.overcrowdingMonitorDef = overcrowdingMonitorDef;
        }
        else
        {
            bEntity.overcrowdingMonitorDef = null;
        }
        FertilityMonitor.Def fertilityMonitorDef = gameObject.GetDef<FertilityMonitor.Def>();
        if (fertilityMonitorDef != null)
        {
            bEntity.fertilityMonitorDef = fertilityMonitorDef;
        }
        else
        {
            bEntity.fertilityMonitorDef = null;
        }
        Navigator navigator = gameObject.GetComponent<Navigator>();
        if (navigator != null)
        {
            bEntity.navigator = new OutNavigator(navigator);
        }
        else
        {
            bEntity.navigator = null;
        }
        RanchableMonitor.Def ranchableMonitorDef = gameObject.GetDef<RanchableMonitor.Def>();
        if (ranchableMonitorDef != null)
        {
            bEntity.isRanchable = true;
        }
        else
        {
            bEntity.isRanchable = false;
        }

        FixedCapturableMonitor.Def fixedCapturableMonitorDef = gameObject.GetDef<FixedCapturableMonitor.Def>();
        if (fixedCapturableMonitorDef != null)
        {
            bEntity.add_fixed_capturable_monitor = true;
        }
        else
        {
            bEntity.add_fixed_capturable_monitor = false;
        }
        FishOvercrowdingMonitor.Def fishOvercrowdingMonitorDef = gameObject.GetDef<FishOvercrowdingMonitor.Def>();
        if (fishOvercrowdingMonitorDef != null)
        {
            bEntity.add_fish_overcrowding_monitor = true;
        }
        else
        {
            bEntity.add_fish_overcrowding_monitor = false;
        }
        BabyMonitor.Def babyMonitorDef = gameObject.GetDef<BabyMonitor.Def>();
        if (babyMonitorDef != null)
        {
            bEntity.babyMonitorDef = babyMonitorDef;
        }
        else
        {
            bEntity.babyMonitorDef = null;
        }
        IncubationMonitor.Def incubatorMonitorDef = gameObject.GetDef<IncubationMonitor.Def>();
        if (incubatorMonitorDef != null)
        {
            bEntity.incubatorMonitorDef = new OutIncubationMonitorDef(incubatorMonitorDef);
        }
        else
        {
            bEntity.incubatorMonitorDef = null;
        }
        CreatureSleepMonitor.Def creatureSleepMonitorDef = gameObject.GetDef<CreatureSleepMonitor.Def>();
        if (creatureSleepMonitorDef != null)
        {
            bEntity.isHasCreatureSleepMonitorDef = true;
        }
        else
        {
            bEntity.isHasCreatureSleepMonitorDef = false;
        }
        CallAdultMonitor.Def callAdultMonitorDef = gameObject.GetDef<CallAdultMonitor.Def>();
        if (callAdultMonitorDef != null)
        {
            bEntity.callAdultMonitorDef = callAdultMonitorDef;
        }
        else
        {
            bEntity.callAdultMonitorDef = null;
        }
        Pickupable pickupable = gameObject.GetComponent<Pickupable>();
        if (pickupable != null)
        {
            bEntity.pickupable = new OutPickupable(pickupable);
        }
        else
        {
            bEntity.pickupable = null;
        }
        Clearable clearable = gameObject.GetComponent<Clearable>();
        if (clearable != null)
        {
            bEntity.clearable = clearable;
        }
        else
        {
            bEntity.clearable = null;
        }
        Health health = gameObject.GetComponent<Health>();
        if (health != null)
        {
            bEntity.health = new OutHealth(health);
        }
        else
        {
            bEntity.health = null;
        }
        CharacterOverlay characterOverlay = gameObject.GetComponent<CharacterOverlay>();
        if (characterOverlay != null)
        {
            bEntity.characterOverlay = characterOverlay;
        }
        else
        {
            bEntity.characterOverlay = null;
        }
        RangedAttackable rangedAttackable = gameObject.GetComponent<RangedAttackable>();
        if (rangedAttackable != null)
        {
            bEntity.rangedAttackable = rangedAttackable;
        }
        else
        {
            bEntity.rangedAttackable = null;
        }
        FactionAlignment factionAlignment = gameObject.GetComponent<FactionAlignment>();
        if (factionAlignment != null)
        {
            bEntity.factionAlignment = new OutFactionAlignment(factionAlignment);
        }
        else
        {
            bEntity.factionAlignment = null;
        }
        Prioritizable prioritizable = gameObject.GetComponent<Prioritizable>();
        if (prioritizable != null)
        {
            bEntity.prioritizable = new OutPrioritizable(prioritizable);
        }
        else
        {
            bEntity.prioritizable = null;
        }
        CreatureDebugGoToMonitor.Def creatureDebugGoToMonitorDef = gameObject.GetDef<CreatureDebugGoToMonitor.Def>();
        if (creatureDebugGoToMonitorDef != null)
        {
            bEntity.creatureDebugGoToMonitorDef = creatureDebugGoToMonitorDef;
        }
        else
        {
            bEntity.creatureDebugGoToMonitorDef = null;
        }
        DeathMonitor.Def deathMonitorDef = gameObject.GetDef<DeathMonitor.Def>();
        if (deathMonitorDef != null)
        {
            bEntity.deathMonitorDef = deathMonitorDef;
        }
        else
        {
            bEntity.deathMonitorDef = null;
        }
        EntombVulnerable entombVulnerable = gameObject.GetComponent<EntombVulnerable>();
        if (entombVulnerable != null)
        {
            bEntity.entombVulnerable = entombVulnerable;
        }
        else
        {
            bEntity.entombVulnerable = null;
        }
        Butcherable butcherable = gameObject.GetComponent<Butcherable>();
        if (butcherable != null)
        {
            bEntity.butcherable = butcherable;
        }
        else
        {
            bEntity.butcherable = null;
        }
        Baggable baggable = gameObject.GetComponent<Baggable>();
        if (baggable != null)
        {
            bEntity.baggable = baggable;
        }
        else
        {
            bEntity.baggable = null;
        }
        Capturable capturable = gameObject.GetComponent<Capturable>();
        if (capturable != null)
        {
            bEntity.capturable = capturable;
        }
        else
        {
            bEntity.capturable = null;
        }
        EggProtectionMonitor.Def eggProtectionMonitorDef = gameObject.GetDef<EggProtectionMonitor.Def>();
        if (eggProtectionMonitorDef != null)
        {
            bEntity.eggProtectionMonitorDef = eggProtectionMonitorDef;
        }
        else
        {
            bEntity.eggProtectionMonitorDef = null;
        }
        Trappable trappable = gameObject.GetComponent<Trappable>();
        if (trappable != null)
        {
            bEntity.trappable = trappable;
        }
        else
        {
            bEntity.trappable = null;
        }
        LoopingSounds loopingSounds = gameObject.GetComponent<LoopingSounds>();
        if (loopingSounds != null)
        {
            bEntity.loopingSounds = loopingSounds;
        }
        else
        {
            bEntity.loopingSounds = null;
        }
        CreatureFallMonitor.Def creatureFallMonitorDef = gameObject.GetDef<CreatureFallMonitor.Def>();
        if (creatureFallMonitorDef != null)
        {
            bEntity.creatureFallMonitorDef = creatureFallMonitorDef;
        }
        else
        {
            bEntity.creatureFallMonitorDef = null;
        }
        SeedPlantingMonitor.Def seedPlantingMonitorDef = gameObject.GetDef<SeedPlantingMonitor.Def>();
        if (seedPlantingMonitorDef != null)
        {
            bEntity.seedPlantingMonitorDef = seedPlantingMonitorDef;
        }
        else
        {
            bEntity.seedPlantingMonitorDef = null;
        }
        ClimbableTreeMonitor.Def climbableTreeMonitorDef = gameObject.GetDef<ClimbableTreeMonitor.Def>();
        if (climbableTreeMonitorDef != null)
        {
            bEntity.climbableTreeMonitorDef = climbableTreeMonitorDef;
        }
        else
        {
            bEntity.climbableTreeMonitorDef = null;
        }
        SymbolOverrideController symbolOverrideController = gameObject.GetComponent<SymbolOverrideController>();
        if (symbolOverrideController != null)
        {
            bEntity.symbolOverrideController = symbolOverrideController;
        }
        else
        {
            bEntity.symbolOverrideController = null;
        }
        SolidConsumerMonitor.Def solidConsumerMonitorDef = gameObject.GetDef<SolidConsumerMonitor.Def>();
        if (solidConsumerMonitorDef != null)
        {
            bEntity.solidConsumerMonitorDef = solidConsumerMonitorDef;
        }
        else
        {
            bEntity.solidConsumerMonitorDef = null;
        }
        ScaleGrowthMonitor.Def scaleGrowthMonitorDef = gameObject.GetDef<ScaleGrowthMonitor.Def>();
        if (scaleGrowthMonitorDef != null)
        {
            bEntity.scaleGrowthMonitorDef = scaleGrowthMonitorDef;
        }
        else
        {
            bEntity.scaleGrowthMonitorDef = null;
        }
        DiggerMonitor.Def diggerMonitorDef = gameObject.GetDef<DiggerMonitor.Def>();
        if (diggerMonitorDef != null)
        {
            bEntity.diggerMonitorDef = diggerMonitorDef;
        }
        else
        {
            bEntity.diggerMonitorDef = null;
        }
        MoltDropperMonitor.Def moltDropperMonitorDef = gameObject.GetDef<MoltDropperMonitor.Def>();
        if (moltDropperMonitorDef != null)
        {
            bEntity.moltDropperMonitorDef = moltDropperMonitorDef;
        }
        else
        {
            bEntity.moltDropperMonitorDef = null;
        }
        HiveGrowthMonitor.Def hiveGrowthMonitorDef = gameObject.GetDef<HiveGrowthMonitor.Def>();
        if (hiveGrowthMonitorDef != null)
        {
            bEntity.hiveGrowthMonitorDef = hiveGrowthMonitorDef;
        }
        else
        {
            bEntity.hiveGrowthMonitorDef = null;
        }
        HiveEatingMonitor.Def hiveEatingMonitorDef = gameObject.GetDef<HiveEatingMonitor.Def>();
        if (hiveEatingMonitorDef != null)
        {
            bEntity.hiveEatingMonitorDef = hiveEatingMonitorDef;
        }
        else
        {
            bEntity.hiveEatingMonitorDef = null;
        }
        HiveHarvestMonitor.Def hiveHarvestMonitorDef = gameObject.GetDef<HiveHarvestMonitor.Def>();
        if (hiveHarvestMonitorDef != null)
        {
            bEntity.hiveHarvestMonitorDef = hiveHarvestMonitorDef;
        }
        else
        {
            bEntity.hiveHarvestMonitorDef = null;
        }
        FoundationMonitor foundationMonitor = gameObject.GetComponent<FoundationMonitor>();
        if (foundationMonitor != null)
        {
            bEntity.foundationMonitor = foundationMonitor;
        }
        else
        {
            bEntity.foundationMonitor = null;
        }
        HiveWorkableEmpty hiveWorkableEmpty = gameObject.GetComponent<HiveWorkableEmpty>();
        if (hiveWorkableEmpty != null)
        {
            bEntity.hiveWorkableEmpty = hiveWorkableEmpty;
        }
        else
        {
            bEntity.hiveWorkableEmpty = null;
        }
        BeeHappinessMonitor.Def beeHappinessMonitorDef = gameObject.GetDef<BeeHappinessMonitor.Def>();
        if (beeHappinessMonitorDef != null)
        {
            bEntity.beeHappinessMonitorDef = beeHappinessMonitorDef;
        }
        else
        {
            bEntity.beeHappinessMonitorDef = null;
        }
        BeeSleepMonitor.Def beeSleepMonitorDef = gameObject.GetDef<BeeSleepMonitor.Def>();
        if (beeSleepMonitorDef != null)
        {
            bEntity.beeSleepMonitorDef = beeSleepMonitorDef;
        }
        else
        {
            bEntity.beeSleepMonitorDef = null;
        }
        ElementConverter[] elementConverters = gameObject.GetComponents<ElementConverter>();
        if (elementConverters != null && elementConverters.Length > 0)
        {
            foreach (var elementConverter in elementConverters)
            {
                bEntity.elementConverters.Add(new OutElementConverter(elementConverter));
            }
        }
        PassiveElementConsumer[] passiveElementConsumers = gameObject.GetComponents<PassiveElementConsumer>();
        if (passiveElementConsumers != null && passiveElementConsumers.Length > 0)
        {
            foreach (var passiveElementConsumer in passiveElementConsumers)
            {
                bEntity.passiveElementConsumers.Add(new OutPassiveElementConsumer(passiveElementConsumer));
            }
        }
        Comet comet = gameObject.GetComponent<Comet>();
        if (comet != null)
        {
            bEntity.comet = new OutComet(comet);
        }
        else
        {
            bEntity.comet = null;
        }
        KSelectable selectable = gameObject.GetComponent<KSelectable>();
        if (selectable != null)
        {
            bEntity.nameString = selectable.entityName;
        }
        else
        {
            bEntity.nameString = null;
        }
        OxygenBreather oxygenBreather = gameObject.GetComponent<OxygenBreather>();
        if (oxygenBreather != null)
        {
            bEntity.oxygenBreather = new OutOxygenBreather(oxygenBreather);
        }
        else
        {
            bEntity.oxygenBreather = null;
        }
        MedicinalPill medicinalPill = gameObject.GetComponent<MedicinalPill>();
        if (medicinalPill != null)
        {
            bEntity.medicinalPill = medicinalPill;
        }
        MedicinalPillWorkable medicinalPillWorkable = gameObject.GetComponent<MedicinalPillWorkable>();
        if (medicinalPillWorkable != null)
        {
            bEntity.medicinalPillWorkable = medicinalPillWorkable;
        }
    }
}
