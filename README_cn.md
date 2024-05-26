[English](README.md) | 简体中文

# OniExtract2024

导出游戏**《缺氧》**中的数据和图片。

项目使用**Visual Studio 2022**编译，ONI游戏版本为 **U51-600112**.

## SteamDB 信息

Game Name: Oxygen Not Included

App ID: 457140

Depot ID: 457141

Manifest ID: 5347960185499743335

在Steam console中回退游戏:

```
download_depot <AppID> <DepotsID> <ManifestID>
```

## 构建项目

1. 在 **Visual Studio Code** 中打开 `OniExtract2024\OniExtract2024.csproj` 。
2. 查看`<GameLibsFolder>` 标签，调整标签标记的内容为你的游戏安装目录。
3. 查看`<ModFolder>` 标签，调整标签标记的内容为你的mod安装目录。
4. 在 **Visual Studio** 打开 `OniExtract2024.sln` 。加载解决方案后, 点击`生成`-`生成解决方案`。
5. mod将被安装到你的mod安装目录下。默认路径: `Documents\Klei\OxygenNotIncluded\mods\dev`。

## 安装

### 通过构建项目

1. 执行构建项目。
2. 检查mod安装目录，默认路径: `Documents\Klei\OxygenNotIncluded\mods\dev`。

### 通过版本发布

1. 从本仓库的**Releases**中下载发布文件。解压缩。
2. 复制**解压后的文件夹**到`Documents\Klei\OxygenNotIncluded\mods\dev` 。

在游戏中打开mod。重启游戏。导出数据将会放在`Documents\Klei\OxygenNotIncluded\export`。

## 导出结果

在该路径下`Documents\Klei\OxygenNotIncluded\export`打开目录。目录结构如下：

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