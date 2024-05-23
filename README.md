# OniExtract2024

Dumps game data and images from game **Oxygen Not Included**.

Compiled under VS2022 for ONI version **U51-600112**.

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

4. open `OniExtract2024.sln` in **Visual Studio**. Load up solution, click `build`. 

Enable the mod in game. Output will be in `Documents\Klei\OxygenNotIncluded\export`.

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
│    └─ tags.json
└─ ui_image
```