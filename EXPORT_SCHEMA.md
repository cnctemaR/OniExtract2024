# OniExtract2024 — Export Schema Reference

Field-level reference for the 13 JSON files written by the main-menu export. For the overall
output layout and the connection-sprite tool, see [README.md](README.md).

Documented against game version **U59-737790-SCA**, 4 active DLCs (includes `EXPANSION1_ID` —
Spaced Out). Counts below reflect that version and shift as the game updates.
Export path: `%USERPROFILE%\Documents\Klei\OxygenNotIncluded\export\database\`

Every file shares these root-level metadata fields:

| Field | Type | Example |
|---|---|---|
| `buildVersion` | string | `"U59-737790-SCA"` |
| `dlcs` | string[] | `["EXPANSION1_ID", ...]` |
| `ExportFileName` | string | matches filename stem |
| `DatabaseDirName` | string | `"database"` |

---

## attribute.json

Enumerations and germ/sickness definitions.

| Field | Type | Count | Notes |
|---|---|---|---|
| `ExposureType` | array | 6 | Germ exposure rules |
| `sicknessComponent` | dict | 6 keys | Keyed by sickness name |
| `SicknessType` | dict | 3 keys | Enum values |
| `Severity` | dict | 4 keys | Enum values |
| `InfectionVector` | dict | 4 keys | Enum values |
| `PermittedRotations` | dict | 5 keys | `Unrotatable=0 R90=1 R360=2 FlipH=3 FlipV=4` |
| `UnitClass` | dict | 15 keys | `SimpleFloat SimpleInteger Temperature Mass Calories Percent Distance Disease Radiation Energy Power Lux Time Seconds Cycles` |
| `TimeSlice` | dict | 4 keys | Enum values |

### ExposureType entry

```jsonc
{
  "germ_id": "FoodPoisoning",
  "sickness_id": "FoodSickness",
  "infection_effect": null,          // string | null
  "exposure_threshold": 100,         // int
  "infect_immediately": false,
  "required_traits": null,           // string[] | null
  "excluded_traits": ["IronGut"],    // string[]
  "excluded_effects": ["FoodSicknessRecovery"],
  "base_resistance": 2
}
```

### sicknessComponent entry (keyed by sickness name)

```jsonc
{
  "sickness": "FoodSickness",
  "modifiers": {
    "Modifers": [ /* AttributeModifier objects */ ],
    "symptoms": [ /* symptom descriptor objects */ ]
  },
  "commonSickEffect": {},
  "periodicEmote": {},
  "animated": {}
}
```

---

## building.json

All buildable structures, the build menu hierarchy, and room/skill mappings.

| Field | Type | Count | Notes |
|---|---|---|---|
| `bBuildingDefList` | array | 449 non-null | Building definitions (no leading nulls) |
| `buildMenuCategories` | array | 15 non-null | Top-level build menu tabs |
| `buildingAndSubcategoryDataPairs` | dict | 15 keys | Keyed by category name |
| `roomConstraintTags` | array | 33 non-null | Tag objects |
| `requiredSkillPerkMap` | dict | 28 keys | Keyed by skill perk Tag object |

### bBuildingDefList entry

```jsonc
{
  // Identity
  "name": "ManualGenerator",             // prefab/code ID (was "prefabId" in 2023)
  "nameString": "<link=\"MANUALGENERATOR\">Manual Generator</link>",
  "kPrefabID": {
    "name": "ManualGenerator",
    "nameString": "<link=\"...\">...</link>",
    "SaveLoadTag": { "Name": "ManualGenerator", "IsValid": true },
    "PrefabTag":   { "Name": "ManualGenerator", "IsValid": true },
    "defaultLayer": 0,
    "tags": " ",                          // space-separated tag names (or array in some entries)
    "requiredDlcIds": "EXPANSION1_ID",    // string | null
    "forbiddenDlcIds": null,
    "AdditionalRequirements": null,
    "AdditionalEffects": null
  },
  "tags": [ { "Name": "RoomProberBuilding", "IsValid": true }, ... ],

  // BuildingDef display fields
  "widthInCells": 2,
  "heightInCells": 2,
  "materialCategory": ["Metal"],         // string[] — material type per build slot
  "materialMass": [200.0],              // float[] — kg per build slot (parallel to materialCategory)
  "isFoundation": false,                 // true for tile/floor buildings
  "isKAnimTile": false,                  // true for animated tile buildings
  "isUtility": false,                    // true for pipe/wire utilities
  "dragBuild": false,
  "buildLocationRule": 1,               // BuildLocationRule enum as int
  "permittedRotations": 0,              // PermittedRotations enum as int (0=Unrotatable)
  "sceneLayer": 19,                     // Grid.SceneLayer enum as int
  "objectLayer": 1,                     // ObjectLayer enum as int
  "viewMode": "Default",                // HashedString toString — overlay mode name
  "defaultAnimState": "off",
  "uiSpriteName": "generatormanual_0",  // sprite name; cross-ref uiSpriteInfos[name].spriteName

  // Component fields — null when that component is absent on this building
  "energyGenerator": null,
  "conduitConsumer": null,
  "conduitDispenser": null,
  "plantablePlot": null,
  "elementConverters": [],
  "elementConsumers": [],
  "passiveElementConsumers": [],
  "storage": { /* OutStorage shape — see below */ },
  "rocketEngineCluster": null,
  "rocketEngine": null,
  "cargoBay": null,
  "cargoBayCluster": null,
  "treeFilterable": null,
  "battery": null,
  "rocketUsageRestrictionDef": null
}
```

**OutStorage shape** (when present):

```jsonc
{
  "allowItemRemoval": false,
  "ignoreSourcePriority": false,
  "onlyTransferFromLowerPriority": false,
  "capacityKg": 20000.0,
  "showDescriptor": false,
  "doDiseaseTransfer": true,
  "storageFilters": null,
  "useGunForDelivery": true,
  "sendOnStoreOnSpawn": false,
  "showInUI": true,
  "allowClearable": false,
  "showCapacityStatusItem": false,
  "showCapacityAsMainStatus": false,
  "showUnreachableStatus": false,
  "showSideScreenTitleBar": false,
  "useWideOffsets": false,
  "fetchCategory": 0,
  "storageNetworkID": -1,
  "storageFullMargin": 0.0,
  "items": [],
  "dropOnLoad": false,
  "allowSettingOnlyFetchMarkedItems": true,
  "storageWorkTime": 1.5
}
```

### buildMenuCategories entry

```jsonc
{ "category": -2955855, "categoryName": "base", "categoryIcon": "icon_category_base" }
```

### buildingAndSubcategoryDataPairs entry

Keyed by category name (`"base"`, `"power"`, etc.). Value is an array of `{Key, Value}` pairs:

```jsonc
[
  { "Key": "Ladder", "Value": "ladders" },
  { "Key": "FirePole", "Value": "ladders" },
  ...
]
```

---

## db.json

The largest file (~722 K lines). Contains Duplicant/game-database objects.

> **Warning:** This file contains case-conflicting keys (`id` and `Id`) within the same object,
> which causes case-insensitive JSON parsers (PowerShell's `ConvertFrom-Json`, some .NET parsers)
> to throw. Use a case-sensitive parser (Newtonsoft.Json default, browser `JSON.parse`, `jq`).

### Top-level arrays

| Field | Description |
|---|---|
| `diseases` | Germ types |
| `sicknesses` | Illness definitions |
| `urges` | Duplicant urge priority definitions |
| `assignableSlots` | Slot types (beds, suits, etc.) |
| `stateMachineCategories` | Internal state machine groupings |
| `personalities` | Duplicant personality templates |
| `faces` | Duplicant face accessories |
| `shirts` | Duplicant clothing options |
| `expressions` | Facial expression sets |
| `minionEmotes` | Duplicant emote animations |
| `critterEmotes` | Critter emote animations |
| `thoughts` | Thought bubble definitions |
| `dreams` | Sleep dream definitions |
| `buildingStatusItems` | Status items for buildings |
| `miscStatusItems` | Miscellaneous status items |
| `creatureStatusItems` | Critter status items |
| `robotStatusItems` | Robot status items |
| `statusItemCategories` | Status item category labels |
| `deaths` | Death cause definitions |
| `choreTypes` | Work chore type definitions |
| `techItems` | Research tree item links |
| `accessorySlots` | Equipment accessory slot definitions |
| `accessories` | Individual accessory items |
| `scheduleBlockTypes` | Schedule block type definitions |
| `scheduleGroups` | Schedule group groupings |
| `roomTypeCategories` | Room type category definitions |
| `roomTypes` | Room type definitions with constraints |
| `artifactDropRates` | Space artifact drop tables |
| `spaceDestinationTypes` | Starmap destination type definitions |
| `skillPerks` | Individual skill perk definitions |
| `permitResources` | Permit/cosmetic resource definitions |
| `artableStatuses` | Artable building status definitions |
| `stories` | Story trait definitions |
| `DuplicantStatusItems` | Additional Duplicant-specific status items |
| `ChoreGroups` | Chore group priority definitions |
| `modifierInfos` | Attribute modifier info entries |
| `traits` | Duplicant trait definitions |
| `effects` | Effect definitions |
| `traitGroups` | Trait group definitions |
| `FertilityModifiers` | Critter fertility modifiers |
| `Attributes` | Attribute definitions |
| `buildingAttributes` | Attribute overrides for buildings |
| `critterAttributes` | Attribute overrides for critters |
| `plantAttributes` | Attribute overrides for plants |
| `amounts` | Amount (tracked value) definitions |
| `attributeConverters` | Attribute converter definitions |

---

## elements.json

All game elements (solids, liquids, gases).

| Field | Type | Count |
|---|---|---|
| `elementTable` | dict | 212 keys |

### Key format

Dict key is the element's SimHash as a **signed decimal integer string** (matches `tag` field inside the entry). Example key: `"-2123557039"`.

### elementTable entry

```jsonc
{
  "name": "<link=\"CRUSHEDICE\">Crushed Ice</link>",  // rich-text, use id for lookup
  "id": "CrushedIce",                                  // plain prefab ID
  "tag": -2123557039,                                  // int — same as the dict key
  "oreTags": ["IceOre", "Unstable", "Solid", "Liquifiable"],
  "state": "Solid",                                    // "Solid" | "Liquid" | "Gas" | "Vacuum"
  "buildMenuSort": 5,                                  // int sort order within build menu
  "materialCategory": "Liquifiable",                   // material category tag name
  "molarMass": 18.01528,
  "specificHeatCapacity": 2.05,
  "thermalConductivity": 2.18,
  "hardness": 10.0,
  "lowTemp": 0.0,                                      // Kelvin — phase transition lower bound
  "highTemp": 272.5,                                   // Kelvin — phase transition upper bound
  "lowTempTransitionTarget": "0",                      // element ID or "0" for none
  "highTempTransitionTarget": "Water",                 // element ID or "0" for none
  "sublimateRate": 0.0,
  "color": 14154495,                                   // ARGB packed int
  "conduitColor": 14154495,
  "uiColor": 6413311
}
```

---

## entities.json

Critters, plants, and other game entities (not buildings).

| Field | Type | Count |
|---|---|---|
| `entities` | array | 429 non-null |

### entities entry (fields)

Most component fields are `null` when absent for that entity type.

```jsonc
{
  "name": "ClusterMapLongRangeMissile",
  "nameString": "<link=\"MISSILELONGRANGE\">Intracosmic Blastshot</link>",
  "kPrefabID": { /* BKprefabID — see building.json */ },
  "kBoxCollider2D": { "x": 0.8, "y": 0.8 },   // null if no collider
  "blightVulnerable": null,
  "isStandardCropPlant": false,
  "plantBranchGrowerDef": null,
  "decorProvider": null,
  "decorToggler": null,
  "wiltConditions": null,
  "pressureVulnerable": null,
  "drowningMonitor": null,
  "temperatureVulnerable": null,
  "mutantPlant": null,
  "cropVal": null,
  "growing": null,
  "health": null,
  "critterTemperatureMonitorDef": null,
  "seedInfo": null,
  "storage": null,
  "elementConsumer": null,
  "manualDeliveryKG_Num": 0,
  "manualDeliveryKGs": [],
  "fertilizationDef": null,
  "irrigationDef": null,
  "primaryElement": {
    "Name": "Creature",
    "InternalTemperature": 293.0,
    "Mass": 2000.0,
    "Temperature": 293.0,
    "DiseaseCount": 0,
    "DiseaseIdx": 255,
    "Units": 1.0
  },
  "illuminationVulnerable": null,
  // ... many more monitor def fields (all nullable) ...
  "navigator": null,
  "isRanchable": false,
  "pickupable": null,
  "factionAlignment": null,
  "elementConverters": [],
  "passiveElementConsumers": [],
  "comet": null,
  "oxygenBreather": null
}
```

---

## food.json

Edible item definitions.

| Field | Type | Count |
|---|---|---|
| `foodInfoList` | array | 64 non-null |
| `requiredDlcIdsMap` | dict | 64 keys | Keyed by food Id |
| `forbiddenDlcIdsMap` | dict | 64 keys | Keyed by food Id |
| `qualityEffects` | dict | 8 keys | Quality level → effect name |

### foodInfoList entry

```jsonc
{
  "Id": "FieldRation",
  "Name": "<link=\"FIELDRATION\">Nutrient Bar</link>",
  "Description": "A nourishing nutrient paste...",
  "CaloriesPerUnit": 800000.0,
  "PreserveTemperature": 255.15,   // Kelvin — stays fresh below this
  "RotTemperature": 277.15,        // Kelvin — rots above this
  "StaleTime": 9600.0,             // seconds
  "SpoilTime": 19200.0,            // seconds
  "CanRot": false,
  "Quality": -1,                   // int food quality tier
  "Effects": [],                   // string[] — effect IDs applied on eating
  "ConsumableId": "FieldRation",
  "ConsumableName": "<link=\"FIELDRATION\">Nutrient Bar</link>",
  "MajorOrder": -1,
  "MinorOrder": 800000,
  "Display": true
}
```

---

## geyser.json

Geyser type definitions.

| Field | Type | Count |
|---|---|---|
| `geysers` | array | 27 non-null |
| `CategorySettings` | dict | 7 keys |
| `geotunerGeyserSettings` | dict | 27 keys | Keyed by geyser prefab ID |
| `geyserIdHashDictionary` | dict | 27 keys | Keyed by geyser prefab ID → hash |

### geysers entry

```jsonc
{
  "id": "GeyserGeneric_steam",
  "anim": "geyser_gas_steam_kanim",
  "width": 2,
  "height": 4,
  "nameStringKey": { "String": "STRINGS.CREATURES.SPECIES.GEYSER.STEAM.NAME", "Hash": 504382788 },
  "descStringKey": { "String": "STRINGS.CREATURES.SPECIES.GEYSER.STEAM.DESC", "Hash": -1068349238 },
  "geyserType": {
    "id": "steam",
    "idHash": { "IsValid": true, "HashValue": -899515856 },
    "element": -899515856,             // SimHash of the emitted element
    "shape": 0,
    "temperature": 383.15,
    "minRatePerCycle": 1000.0,
    "maxRatePerCycle": 2000.0,
    "maxPressure": 5.0,
    "diseaseInfo": { "idx": 255, "count": 0 },
    "minIterationLength": 60.0,
    "maxIterationLength": 1140.0,
    "minIterationPercent": 0.1,
    "maxIterationPercent": 0.9,
    "minYearLength": 15000.0,
    "maxYearLength": 135000.0,
    "minYearPercent": 0.4,
    "maxYearPercent": 0.8,
    "geyserTemperature": 372.15,
    "DlcID": null,
    "requiredDlcIds": null,
    "forbiddenDlcIds": null
  },
  "isGenericGeyser": true
}
```

---

## items.json

Pickupable items: eggs, plant seeds, and equipment suits.

| Field | Type | Count | Notes |
|---|---|---|---|
| `eggs` | array | 47 non-null | Critter eggs |
| `seeds` | array | 40 non-null | Plant seeds |
| `equipments` | array | 11 non-null | Wearable suits/equipment |

### eggs entry

```jsonc
{
  "name": "PuftEgg",
  "nameString": "<link=\"PUFT\">Puft Egg</link>",
  "kPrefabID": { /* BKprefabID with tags as proper array */ },
  "tags": [ { "Name": "Egg", "IsValid": true }, ... ],
  "kBoxCollider2D": { "x": 0.8, "y": 0.8 },
  "incubatorMonitorDef": {
    "baseIncubationRate": 0.01111,
    "spawnedCreature": { "Name": "PuftBaby", "IsValid": true }
  },
  "pickupable": { /* OutPickupable */ },
  "primaryElement": { "Name": "Creature", "InternalTemperature": 293.0, "Mass": 0.5, ... }
}
```

### seeds entry

Same shape as eggs but without `incubatorMonitorDef`; `kBoxCollider2D` may be null.

### equipments entry

```jsonc
{
  "name": "Atmo_Suit",
  "nameString": "<link=\"ATMOSUIT\">Atmo Suit</link>",
  "kPrefabID": { /* BKprefabID */ },
  "tags": [],
  "kBoxCollider2D": null,
  "suitTank": {
    "element": "Oxygen",
    "amount": 0.0,
    "elementTag": { "Name": "Breathable", "IsValid": true },
    "capacity": 75.0,
    "underwaterSupport": false,
    "ShouldEmitCO2": true,
    "ShouldStoreCO2": false
  },
  "storage": { /* OutStorage */ },
  "pickupable": { /* OutPickupable */ },
  "primaryElement": { "Name": "Dirt", "InternalTemperature": 293.0, "Mass": 200.0, ... }
}
```

---

## multiEntities.json

Space POIs, meteor showers, comets, and other cluster-map entities.

| Field | Type | Count |
|---|---|---|
| `multiEntities` | array | 132 non-null |
| `meteorShowerEventMap` | dict | 19 keys | Keyed by event ID |

### multiEntities entry

```jsonc
{
  "name": "ArtifactSpacePOI_GravitasSpaceStation1",
  "nameString": "ArtifactSpacePOI_GravitasSpaceStation1",
  "entityType": "ArtifactPOIConfig",
  "kPrefabID": { /* BKprefabID */ },
  "kBoxCollider2D": null,
  "occupyArea": null,
  "decorProvider": null,
  "pickupable": null,
  "primaryElement": null,
  "harvestablePOIType": null,
  "artifactPOIType": {
    "id": "GravitasSpaceStation1",
    "idHash": { "IsValid": true, "HashValue": 671325510 },
    "harvestableArtifactID": null,
    "destroyOnHarvest": false,
    "poiRechargeTimeMin": 30000.0,
    "poiRechargeTimeMax": 60000.0,
    "dlcID": null,
    "initialDatabankCount": 50,
    "requiredDlcIds": "EXPANSION1_ID",
    "forbiddenDlcIds": null,
    "orbitalObject": "gravitas"
  },
  "spaceArtifact": null,
  "light2D": null,
  "geyserType": null,
  "clusterMapMeteorShowerVisualizer": null,
  "clusterTraveler": null,
  "clusterMapMeteorShowerDef": null,
  "meteorShowerEvent": null
}
```

### meteorShowerEventMap entry (keyed by event ID)

```jsonc
{
  "id": "MeteorShowerGoldEvent",
  "bombardmentInfo": [
    { "prefab": "GoldComet", "weight": 2.0 },
    { "prefab": "RockComet", "weight": 0.5 },
    { "prefab": "DustComet", "weight": 5.0 }
  ],
  "secondsBombardmentOff": { "min": 800.0, "max": 1200.0 },
  "secondsBombardmentOn":  { "min": 50.0,  "max": 100.0 },
  "secondsPerMeteor": 0.4,
  "duration": 3000.0,
  "clusterMapMeteorShowerID": null,
  "affectedByDifficulty": true,
  "animFileName": { "IsValid": true, "HashValue": 1601411940 },
  "tags": [ { "Name": "SpaceDanger", "IsValid": true } ],
  "Name": "MeteorShowerGoldEvent",
  "IdHash": { "IsValid": true, "HashValue": 1601411940 },
  "numTimesAllowed": -1,
  "allowMultipleEventInstances": true
}
```

---

## po_string.json

All localizable strings, organized by game namespace. This is the U59 **English** string table.

| Namespace | Keys | Description |
|---|---|---|
| `BUILDING` | 891 | Building status item strings |
| `BUILDINGS` | 2914 | Building names, descriptions, effects |
| `CLUSTER_NAMES` | 68 | Cluster/world names |
| `CODEX` | 1811 | Codex entry text |
| `COLONY_ACHIEVEMENTS` | 331 | Achievement text |
| `CREATURES` | 1389 | Critter names and descriptions |
| `DUPLICANTS` | 2783 | Duplicant-related strings |
| `ELEMENTS` | 480 | Element names and descriptions |
| `EQUIPMENT` | 772 | Equipment strings |
| `GAMEPLAY_EVENTS` | 93 | Gameplay event strings |
| `INPUT` / `INPUT_BINDINGS` | 21 / 188 | Input binding labels |
| `ITEMS` | 444 | Item strings |
| `LORE` | 16 | Lore text |
| `MISC` | 628 | Miscellaneous strings |
| `NAMEGEN` | 556 | Procedural name generation |
| `RESEARCH` | 329 | Research tree strings |
| `ROBOTS` | 56 | Robot strings |
| `ROOMS` | 300 | Room type strings |
| `SETITEMS` | 0 | (empty) |
| `STICKERNAMES` | 20 | Sticker names |
| `SUBWORLDS` | 87 | Sub-world biome strings |
| `UI` | 5807 | UI label strings |
| `VIDEOS` | 7 | Video strings |
| `WORLD_TRAITS` | 50 | World trait strings |
| `WORLDS` | 167 | World/asteroid strings |

Each namespace is a **flat dict** keyed by the full dotted string path:

```jsonc
{
  "BUILDING.STATUSITEMS.LITTERBOXBEINGEMPTIED.NAME": "Being Scooped",
  "BUILDING.STATUSITEMS.LITTERBOXBEINGEMPTIED.TOOLTIP": "...",
  ...
}
```

---

## recipe.json

Fabricator recipes.

| Field | Type | Count |
|---|---|---|
| `recipes` | array | 173 non-null |

### recipes entry

```jsonc
{
  "id": "ChemicalRefinery_I_Water_Salt_O_SaltWater",
  "recipeCategoryID": "ChemicalRefinery_I_Water_Salt_O_SaltWater_Default_SaltWater",
  "ingredients": [
    {
      "material": { "Name": "Water", "IsValid": true },
      "possibleMaterials": [ { "Name": "Water", "IsValid": true } ],
      "possibleMaterialAmounts": null,
      "temperatureOperation": 0,
      "storeElement": false,
      "inheritElement": false,
      "facadeID": null,
      "doNotConsume": false,
      "amount": 93.0
    }
  ],
  "results": [
    {
      "material": { "Name": "SaltWater", "IsValid": true },
      "possibleMaterials": [ { "Name": "SaltWater", "IsValid": true } ],
      "possibleMaterialAmounts": null,
      "temperatureOperation": 0,
      "storeElement": true,
      "inheritElement": false,
      "facadeID": null,
      "doNotConsume": false,
      "amount": 100.0
    }
  ],
  "time": 40.0,                         // seconds
  "consumedHEP": 0,
  "producedHEP": 0,
  "nameDisplay": 1,                     // 0=None 1=Result 2=Custom
  "customName": null,
  "customSpritePrefabID": null,
  "description": "Salt Water is a <link=\"ELEMENTSLIQUID\">Liquid</link>...",
  "runTimeDescription": null,
  "fabricators": [ { "Name": "ChemicalRefinery", "IsValid": true } ],
  "sortOrder": 0,
  "requiredTech": null,
  "ProductHasFacade": false,
  "RequiresAllIngredientsDiscovered": false,
  "FirstResult": { "Name": "SaltWater", "IsValid": true }
}
```

---

## tags.json

Tag and hash lookup tables.

| Field | Type | Count | Notes |
|---|---|---|---|
| `SimHashes` | dict | 212 keys | Element name → int hash (matches `elements.json` keys) |
| `RoomConstraintTags` | array | 33 | Tag objects `{Name, IsValid}` |
| `mGameTags` | dict | 4 keys | See below |
| `prefabIDs` | dict | 2382 keys | All prefab IDs: name → int hash |

### SimHashes

Flat dict: element name string → signed integer hash.
Example: `"CrushedIce": -2123557039`

These values are the same as the keys in `elements.json`'s `elementTable`.

### mGameTags structure

```jsonc
{
  "tags": { /* flat dict of all known Tag names → empty string */ },
  "tagLists": {
    "AllSuitTags": [...],
    "OxygenSuitTags": [...],
    "AllClothesTags": [...]
  },
  "tagSets": {
    "SolidElements": [...],
    "LiquidElements": [...],
    "GasElements": [...],
    "CalorieCategories": [...],
    "UnitCategories": [...],
    "IgnoredMaterialCategories": [...],
    "MaterialCategories": [...],
    "BionicCompatibleBatteries": [...],
    "BionicIncompatibleBatteries": [...],
    "MaterialBuildingElements": [...],
    "OtherEntityTags": [...],
    "AllCategories": [...],
    "DisplayAsCalories": [...],
    "DisplayAsUnits": [...],
    "DisplayAsInformation": [...],
    "HiddenElementTags": [...]
  },
  "tagNotOutput": "StartingMetalOres StartingRefinedMetals BasicWoods ..."
}
```

---

## uiSpriteInfo.json

Sprite metadata for UI icons.

| Field | Type | Count |
|---|---|---|
| `uiSpriteInfos` | dict | 1241 keys | Keyed by item prefab ID |
| `uiFacadeInfos` | dict | 988 keys | Keyed by facade ID |

### uiSpriteInfos entry (keyed by prefab ID e.g. `"FabricatedWood"`)

```jsonc
{
  "id": "<link=\"FABRICATEDWOOD\">Plywood</link>",  // rich-text display name
  "name": "Plywood",                                 // plain display name
  "spriteName": "compressed_wood_0:ui:False",
  "textureName": "compressed_wood_0",
  "color": { "r": 1.0, "g": 1.0, "b": 1.0, "a": 1.0 }
}
```

### uiFacadeInfos entry (keyed by facade ID e.g. `"ExteriorWall_basic_white"`)

```jsonc
{
  "id": "ExteriorWall_basic_white",
  "name": "Fresh White",
  "spriteName": "walls_basic_white_0:ui:False",
  "textureName": "walls_basic_white_0",
  "color": null   // may be null or { r, g, b, a }
}
```

---

## Known Issues / Notes

### Rich-text link tags in name strings

All `nameString` fields and some description fields include Unity rich-text link tags:

```
<link="CRUSHEDICE">Crushed Ice</link>
```

Rendered as `&lt;link=...&gt;` in some contexts (JSON encodes `<` as `<`).
Strip with: `/<link="[^"]*">([^<]*)<\/link>/g` → capture group 1.

### db.json duplicate-key issue

The `diseases` array (and possibly others) in `db.json` contains objects with both `id` (object) and `Id` (possibly string) as sibling keys. Standard case-insensitive parsers will throw. Use `JSON.parse` (browser/Node), `jq`, or Newtonsoft.Json with default settings.

### spriteModifiers (2023 field — absent in 2024)

The 2023 `database.json` had a `spriteModifiers` list. It does not appear in any 2024 export file under any name. Either it was removed from the game or the website's use of it should be audited.

### Image files — use `ui_image/`, not `images/`

The 2024 mod exports 1,241 PNGs to `export/ui_image/` (named by proper display name).
The old `export/images/` folder (536 files, Oct 2025, OniExtract2020) is obsolete.

To resolve a PNG for any prefab:
```
ui_image/{uiSpriteInfos[prefabTagName].name}.png
```

Facade sprites are under `export/ui_image_facade/{facadeSetId}/`.
