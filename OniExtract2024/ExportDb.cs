using OniExtract2024;
using System.Collections.Generic;
using Database;
using Klei.AI;
using static ModifierSet;
using Attribute = Klei.AI.Attribute;

public class ExportDb : BaseExport
{
    public override string ExportFileName { get; set; } = "db";
    public List<Disease> dieases = new List<Disease>();
    public List<Sickness> sicknesses = new List<Sickness>();
    public List<Urge> urges = new List<Urge>();
    public List<AssignableSlot> assignableSlots = new List<AssignableSlot>();
    public List<StateMachine.Category> stateMachineCategories = new List<StateMachine.Category>();
    public List<Personality> personalities = new List<Personality>();
    public List<Face> faces = new List<Face>();
    public List<Shirt> shirts = new List<Shirt>();
    public List<Expression> expressions = new List<Expression>();
    public List<Emote> minionEmotes = new List<Emote>();
    public List<Emote> critterEmotes = new List<Emote>();
    public List<Thought> thoughts = new List<Thought>();
    public List<Dream> dreams = new List<Dream>();
    public List<StatusItem> buildingStatusItems = new List<StatusItem>();
    public List<StatusItem> miscStatusItems = new List<StatusItem>();
    public List<StatusItem> creatureStatusItems = new List<StatusItem>();
    public List<StatusItem> robotStatusItems = new List<StatusItem>();
    public List<StatusItemCategory> statusItemCategories = new List<StatusItemCategory>();
    public List<Death> deaths = new List<Death>();
    public List<OutChoreType> choreTypes = new List<OutChoreType>();
    public List<OutTechItem> techItems = new List<OutTechItem>();
    public List<AccessorySlot> accessorySlots = new List<AccessorySlot>();
    public List<Accessory> accessories = new List<Accessory>();
    public List<OutScheduleBlockType> scheduleBlockTypes = new List<OutScheduleBlockType>();
    public List<OutScheduleGroup> scheduleGroups = new List<OutScheduleGroup>();
    public List<RoomTypeCategory> roomTypeCategories = new List<RoomTypeCategory>();
    public List<RoomType> roomTypes = new List<RoomType>();
    public List<ArtifactDropRate> artifactDropRates = new List<ArtifactDropRate>();
    public List<SpaceDestinationType> spaceDestinationTypes = new List<SpaceDestinationType>();
    public List<SkillPerk> skillPerks = new List<SkillPerk>();
    public List<SkillGroup> skillGroups = new List<SkillGroup>();
    public List<Skill> skills = new List<Skill>();
    public List<ColonyAchievement> colonyAchievements = new List<ColonyAchievement>();
    public List<Quest> quests = new List<Quest>();
    public List<GameplayEvent> gameplayEvents = new List<GameplayEvent>();
    public List<GameplaySeason> gameplaySeasons = new List<GameplaySeason>();
    public List<PlantMutation> plantMutations = new List<PlantMutation>();
    public List<OutSpice> spices = new List<OutSpice>();
    public List<OutTech> techs = new List<OutTech>();
    public List<TechTreeTitle>  techTreeTitles = new List<TechTreeTitle>();
    public List<OrbitalData> orbitalTypeCategories = new List<OrbitalData>();
    public List<PermitResource> permitResources = new List<PermitResource>();
    public List<ArtableStatusItem> artableStatuses = new List<ArtableStatusItem>();
    public List<OutStory> stories = new List<OutStory>();
    // EntityModifierSet
    public List<OutStatusItem> DuplicantStatusItems = new List<OutStatusItem>();
    public List<OutChoreGroup> ChoreGroups = new List<OutChoreGroup>();
    // ModifierSet
    public List<ModifierInfo> modifierInfos = new List<ModifierInfo>();
    public List<OutTrait> traits = new List<OutTrait>();
    public List<Effect> effects = new List<Effect>();
    public List<OutTraitGroup> traitGroups = new List<OutTraitGroup>();
    public List<FertilityModifier> FertilityModifiers = new List<FertilityModifier>();
    public List<Attribute> Attributes = new List<Attribute>();
    public List<Attribute> buildingAttributes = new List<Attribute>();
    public List<Attribute> critterAttributes = new List<Attribute>();
    public List<Attribute> plantAttributes = new List<Attribute>();
    public List<Amount> amounts = new List<Amount>();
    public List<AttributeConverter> attributeConverters = new List<AttributeConverter>();

    public ExportDb()
    {
    }

    public void AddDbResources()
    {
        foreach (var resource in Db.Get().Diseases.resources)
        {
            this.dieases.Add(resource);
        }
        foreach (var resource in Db.Get().Sicknesses.resources)
        {
            this.sicknesses.Add(resource);
        }
        foreach (var resource in Db.Get().Urges.resources)
        {
            this.urges.Add(resource);
        }
        foreach (var resource in Db.Get().AssignableSlots.resources)
        {
            this.assignableSlots.Add(resource);
        }
        foreach (var resource in Db.Get().StateMachineCategories.resources)
        {
            this.stateMachineCategories.Add(resource);
        }
        foreach (var resource in Db.Get().Personalities.resources)
        {
            this.personalities.Add(resource);
        }
        foreach (var resource in Db.Get().Faces.resources)
        {
            this.faces.Add(resource);
        }
        foreach (var resource in Db.Get().Shirts.resources)
        {
            this.shirts.Add(resource);
        }
        foreach (var resource in Db.Get().Expressions.resources)
        {
            this.expressions.Add(resource);
        }
        foreach (var resource in Db.Get().Emotes.Minion.resources)
        {
            this.minionEmotes.Add(resource);
        }
        foreach (var resource in Db.Get().Emotes.Critter.resources)
        {
            this.critterEmotes.Add(resource);
        }
        foreach (var resource in Db.Get().Thoughts.resources)
        {
            this.thoughts.Add(resource);
        }
        foreach (var resource in Db.Get().Dreams.resources)
        {
            this.dreams.Add(resource);
        }
        foreach (var resource in Db.Get().BuildingStatusItems.resources)
        {
            this.buildingStatusItems.Add(resource);
        }
        foreach (var resource in Db.Get().MiscStatusItems.resources)
        {
            this.miscStatusItems.Add(resource);
        }
        AddCreatureStatusItems(Db.Get().CreatureStatusItems);
        AddRobotStatusItems(Db.Get().RobotStatusItems);
        foreach (var resource in Db.Get().StatusItemCategories.resources)
        {
            this.statusItemCategories.Add(resource);
        }
        foreach (var resource in Db.Get().Deaths.resources)
        {
            this.deaths.Add(resource);
        }
        foreach (var resource in Db.Get().ChoreTypes.resources)
        {
            this.choreTypes.Add(new OutChoreType(resource));
        }
        foreach (var resource in Db.Get().TechItems.resources)
        {
            this.techItems.Add(new OutTechItem(resource));
        }
        foreach (var resource in Db.Get().AccessorySlots.resources)
        {
            this.accessorySlots.Add(resource);
        }
        foreach (var resource in Db.Get().Accessories.resources)
        {
            this.accessories.Add(resource);
        }
        foreach (var resource in Db.Get().ScheduleBlockTypes.resources)
        {
            this.scheduleBlockTypes.Add(new OutScheduleBlockType(resource));
        }
        foreach (var resource in Db.Get().ScheduleGroups.resources)
        {
            this.scheduleGroups.Add(new OutScheduleGroup(resource));
        }
        foreach (var resource in Db.Get().RoomTypeCategories.resources)
        {
            this.roomTypeCategories.Add(resource);
        }
        foreach (var resource in Db.Get().RoomTypes.resources)
        {
            this.roomTypes.Add(resource);
        }
        foreach (var resource in Db.Get().ArtifactDropRates.resources)
        {
            this.artifactDropRates.Add(resource);
        }
        foreach (var resource in Db.Get().SpaceDestinationTypes.resources)
        {
            this.spaceDestinationTypes.Add(resource);
        }
        foreach (var resource in Db.Get().SkillPerks.resources)
        {
            this.skillPerks.Add(resource);
        }
        foreach (var resource in Db.Get().SkillGroups.resources)
        {
            this.skillGroups.Add(resource);
        }
        foreach (var resource in Db.Get().Skills.resources)
        {
            this.skills.Add(resource);
        }
        foreach (var resource in Db.Get().ColonyAchievements.resources)
        {
            this.colonyAchievements.Add(resource);
        }
        foreach (var resource in Db.Get().Quests.resources)
        {
            this.quests.Add(resource);
        }
        foreach (var resource in Db.Get().GameplayEvents.resources)
        {
            this.gameplayEvents.Add(resource);
        }
        foreach (var resource in Db.Get().GameplaySeasons.resources)
        {
            this.gameplaySeasons.Add(resource);
        }

        if (DlcManager.IsExpansion1Active()) {
            foreach (var resource in Db.Get().PlantMutations.resources)
            {
                this.plantMutations.Add(resource);
            }
        }
        
        foreach (var resource in Db.Get().Spices.resources)
        {
            this.spices.Add(new OutSpice(resource));
        }
        foreach (var resource in Db.Get().Techs.resources)
        {
            this.techs.Add(new OutTech(resource));
        }
        foreach (var resource in Db.Get().TechTreeTitles.resources)
        {
            this.techTreeTitles.Add(resource);
        }
        foreach (var resource in Db.Get().OrbitalTypeCategories.resources)
        {
            this.orbitalTypeCategories.Add(resource);
        }
        foreach (var resource in Db.Get().Permits.resources)
        {
            this.permitResources.Add(resource);
        }
        foreach (var resource in Db.Get().ArtableStatuses.resources)
        {
            this.artableStatuses.Add(resource);
        }
        foreach (var resource in Db.Get().Stories.resources)
        {
            this.stories.Add(new OutStory(resource));
        }
        // EntityModifierSet
        foreach (var resource in Db.Get().DuplicantStatusItems.resources)
        {
            this.DuplicantStatusItems.Add(new OutStatusItem(resource));
        }
        foreach (var resource in Db.Get().ChoreGroups.resources)
        {
            this.ChoreGroups.Add(new OutChoreGroup(resource));
        }
        // ModifierSet
        foreach (ModifierInfo modifierInfo in Db.Get().modifierInfos.resources)
        {
            this.modifierInfos.Add(modifierInfo);
        }
        foreach (Trait trait in Db.Get().traits.resources)
        {
            this.traits.Add(new OutTrait(trait));
        }
        foreach (Effect effect in Db.Get().effects.resources)
        {
            this.effects.Add(effect);
        }
        foreach (TraitGroup traitGroup in Db.Get().traitGroups.resources)
        {
            this.traitGroups.Add(new OutTraitGroup(traitGroup));
        }
        foreach (FertilityModifier fertilityModifier in Db.Get().FertilityModifiers.resources)
        {
            this.FertilityModifiers.Add(fertilityModifier);
        }
        foreach (Attribute attribute in Db.Get().Attributes.resources)
        {
            this.Attributes.Add(attribute);
        }
        foreach (var resource in Db.Get().BuildingAttributes.resources)
        {
            this.buildingAttributes.Add(resource);
        }
        foreach (var resource in Db.Get().CritterAttributes.resources)
        {
            this.critterAttributes.Add(resource);
        }
        foreach (var resource in Db.Get().PlantAttributes.resources)
        {
            this.plantAttributes.Add(resource);
        }
        foreach (var resource in Db.Get().Amounts.resources)
        {
            this.amounts.Add(resource);
        }
        foreach (var resource in Db.Get().AttributeConverters.resources)
        {
            this.attributeConverters.Add(resource);
        }
    }

    public void AddCreatureStatusItems(CreatureStatusItems CreatureStatusItems)
    {
        this.creatureStatusItems.Add(CreatureStatusItems.Dead);
        this.creatureStatusItems.Add(CreatureStatusItems.HealthStatus);
        this.creatureStatusItems.Add(CreatureStatusItems.Hot);
        this.creatureStatusItems.Add(CreatureStatusItems.Hot_Crop);
        this.creatureStatusItems.Add(CreatureStatusItems.Scalding);
        this.creatureStatusItems.Add(CreatureStatusItems.Cold);
        this.creatureStatusItems.Add(CreatureStatusItems.Cold_Crop);
        this.creatureStatusItems.Add(CreatureStatusItems.Crop_Too_Dark);
        this.creatureStatusItems.Add(CreatureStatusItems.Crop_Too_Bright);
        this.creatureStatusItems.Add(CreatureStatusItems.Crop_Blighted);
        this.creatureStatusItems.Add(CreatureStatusItems.Hypothermia);
        this.creatureStatusItems.Add(CreatureStatusItems.Hyperthermia);
        this.creatureStatusItems.Add(CreatureStatusItems.Suffocating);
        this.creatureStatusItems.Add(CreatureStatusItems.Hatching);
        this.creatureStatusItems.Add(CreatureStatusItems.Incubating);
        this.creatureStatusItems.Add(CreatureStatusItems.Drowning);
        this.creatureStatusItems.Add(CreatureStatusItems.Saturated);
        this.creatureStatusItems.Add(CreatureStatusItems.DryingOut);
        this.creatureStatusItems.Add(CreatureStatusItems.Growing);
        this.creatureStatusItems.Add(CreatureStatusItems.CropSleeping);
        this.creatureStatusItems.Add(CreatureStatusItems.ReadyForHarvest);
        this.creatureStatusItems.Add(CreatureStatusItems.EnvironmentTooWarm);
        this.creatureStatusItems.Add(CreatureStatusItems.EnvironmentTooCold);
        this.creatureStatusItems.Add(CreatureStatusItems.Entombed);
        this.creatureStatusItems.Add(CreatureStatusItems.Wilting);
        this.creatureStatusItems.Add(CreatureStatusItems.WiltingDomestic);
        this.creatureStatusItems.Add(CreatureStatusItems.WiltingNonGrowing);
        this.creatureStatusItems.Add(CreatureStatusItems.WiltingNonGrowingDomestic);
        this.creatureStatusItems.Add(CreatureStatusItems.WrongAtmosphere);
        this.creatureStatusItems.Add(CreatureStatusItems.AtmosphericPressureTooLow);
        this.creatureStatusItems.Add(CreatureStatusItems.AtmosphericPressureTooHigh);
        this.creatureStatusItems.Add(CreatureStatusItems.Barren);
        this.creatureStatusItems.Add(CreatureStatusItems.NeedsFertilizer);
        this.creatureStatusItems.Add(CreatureStatusItems.NeedsIrrigation);
        this.creatureStatusItems.Add(CreatureStatusItems.WrongTemperature);
        this.creatureStatusItems.Add(CreatureStatusItems.WrongFertilizer);
        this.creatureStatusItems.Add(CreatureStatusItems.WrongIrrigation);
        this.creatureStatusItems.Add(CreatureStatusItems.WrongFertilizerMajor);
        this.creatureStatusItems.Add(CreatureStatusItems.WrongIrrigationMajor);
        this.creatureStatusItems.Add(CreatureStatusItems.CantAcceptFertilizer);
        this.creatureStatusItems.Add(CreatureStatusItems.CantAcceptIrrigation);
        this.creatureStatusItems.Add(CreatureStatusItems.Rotting);
        this.creatureStatusItems.Add(CreatureStatusItems.Fresh);
        this.creatureStatusItems.Add(CreatureStatusItems.Stale);
        this.creatureStatusItems.Add(CreatureStatusItems.Spoiled);
        this.creatureStatusItems.Add(CreatureStatusItems.Refrigerated);
        this.creatureStatusItems.Add(CreatureStatusItems.RefrigeratedFrozen);
        this.creatureStatusItems.Add(CreatureStatusItems.Unrefrigerated);
        this.creatureStatusItems.Add(CreatureStatusItems.SterilizingAtmosphere);
        this.creatureStatusItems.Add(CreatureStatusItems.ContaminatedAtmosphere);
        this.creatureStatusItems.Add(CreatureStatusItems.Old);
        this.creatureStatusItems.Add(CreatureStatusItems.ExchangingElementOutput);
        this.creatureStatusItems.Add(CreatureStatusItems.ExchangingElementConsume);
        this.creatureStatusItems.Add(CreatureStatusItems.Hungry);
        this.creatureStatusItems.Add(CreatureStatusItems.HiveHungry);
        this.creatureStatusItems.Add(CreatureStatusItems.NoSleepSpot);
        this.creatureStatusItems.Add(CreatureStatusItems.OriginalPlantMutation);
        this.creatureStatusItems.Add(CreatureStatusItems.UnknownMutation);
        this.creatureStatusItems.Add(CreatureStatusItems.SpecificPlantMutation);
        this.creatureStatusItems.Add(CreatureStatusItems.Crop_Too_NonRadiated);
        this.creatureStatusItems.Add(CreatureStatusItems.Crop_Too_Radiated);
        this.creatureStatusItems.Add(CreatureStatusItems.ElementGrowthGrowing);
        this.creatureStatusItems.Add(CreatureStatusItems.ElementGrowthStunted);
        this.creatureStatusItems.Add(CreatureStatusItems.ElementGrowthHalted);
        this.creatureStatusItems.Add(CreatureStatusItems.ElementGrowthComplete);
        this.creatureStatusItems.Add(CreatureStatusItems.LookingForFood);
        this.creatureStatusItems.Add(CreatureStatusItems.LookingForGas);
        this.creatureStatusItems.Add(CreatureStatusItems.LookingForLiquid);
        this.creatureStatusItems.Add(CreatureStatusItems.Beckoning);
        this.creatureStatusItems.Add(CreatureStatusItems.BeckoningBlocked);
        this.creatureStatusItems.Add(CreatureStatusItems.MilkProducer);
        this.creatureStatusItems.Add(CreatureStatusItems.MilkFull);
        this.creatureStatusItems.Add(CreatureStatusItems.GettingRanched);
        this.creatureStatusItems.Add(CreatureStatusItems.GettingMilked);
    }
    public void AddRobotStatusItems(RobotStatusItems RobotStatusItems)
    {
        this.robotStatusItems.Add(RobotStatusItems.LowBattery);
        this.robotStatusItems.Add(RobotStatusItems.LowBatteryNoCharge);
        this.robotStatusItems.Add(RobotStatusItems.DeadBattery);
        this.robotStatusItems.Add(RobotStatusItems.CantReachStation);
        this.robotStatusItems.Add(RobotStatusItems.DustBinFull);
        this.robotStatusItems.Add(RobotStatusItems.Working);
        this.robotStatusItems.Add(RobotStatusItems.UnloadingStorage);
        this.robotStatusItems.Add(RobotStatusItems.ReactPositive);
        this.robotStatusItems.Add(RobotStatusItems.ReactNegative);
        this.robotStatusItems.Add(RobotStatusItems.MovingToChargeStation);
    }
}
