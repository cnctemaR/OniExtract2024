English | [简体中文](README_cn.md)

# OniExtract2024

Dumps game data and images from game **Oxygen Not Included**.

Compiled under **Visual Studio 2022** for ONI version **U51-600112**.

## SteamDB Info

Game Name: Oxygen Not Included

App ID: 457140

Depot ID: 457141

Manifest ID: 5347960185499743335

Rollback game in Steam console:

```
download_depot <AppID> <DepotsID> <ManifestID>
```

## Build

1. Open `OniExtract2024\OniExtract2024.csproj` in **Visual Studio Code**.
2. Check `<GameLibsFolder>` , adjust to your own game installation.
3. Check `<ModFolder>` , adjust to your mod installation.
4. Open `OniExtract2024.sln` in **Visual Studio**. Load up solution, click `Build`-`Build Solution`. 
5. Mod will be installed to your mod installation. default path: `Documents\Klei\OxygenNotIncluded\mods\dev`.

## Install

### From Build

1. Build project.
2. Check your mod installation. default path: `Documents\Klei\OxygenNotIncluded\mods\dev`.

### From Releases

1. Download package from **Releases**. Unzip package.
2. Copy **unzipped folder** to `Documents\Klei\OxygenNotIncluded\mods\dev` 

Enable the mod in game. Restart game. Output will be in `Documents\Klei\OxygenNotIncluded\export`.

## Output Result

Go to `Documents\Klei\OxygenNotIncluded\export` . Here is the directory tree:

```
export
├─ database
│    ├─ building.json
│    ├─ db.json
│    ├─ elements.json
│    ├─ entities.json
│    ├─ equipment.json
│    ├─ food.json
│    ├─ geyser.json
│    ├─ po_string.json
│    ├─ recipe.json
│    ├─ tags.json
│    └─ uiSpriteInfo.json
├─ ui_image
└─ ui_image_facade
       ├─ ArtableStages
       ├─ BalloonArtistFacades
       ├─ BuildingFacades
       ├─ ClothingItems
       ├─ EquippableFacades
       ├─ MonumentParts
       └─ StickerBombs
```