﻿using OniExtract2024;
using OniExtract2024.utils;
using Database;
using System;
using System.Collections.Generic;
using System.IO;
using PeterHan.PLib.Options;
using UnityEngine;
using System.Linq;
using STRINGS;

public class ExportUISprite : BaseExport
{
    public override string ExportFileName { get; set; } = "uiSpriteInfo";
    public Dictionary<string, BUISprite> uiSpriteInfos = new Dictionary<string, BUISprite>();
    public Dictionary<string, BUISprite> uiFacadeInfos = new Dictionary<string, BUISprite>();

    public ExportUISprite()
    {
    }

    public void AddUISpriteInfo(KPrefabID prefabID, Tuple<Sprite, Color> tupleUISprite)
    {
        this.uiSpriteInfos[prefabID.PrefabTag.Name] = new BUISprite(prefabID.name, tupleUISprite.first, tupleUISprite.second);
    }

    public void AddFacadeInfos(string id, Sprite sprite)
    {
        this.uiFacadeInfos[id] = new BUISprite(id, sprite);
    }

    public void ExportAllUISprite()
    {
        string ExportIconDir = Path.Combine(Util.RootFolder(), "export", "ui_image");
        foreach (var prefab in Assets.Prefabs)
        {
            if (prefab == null || prefab.PrefabTag == null)
            {
                continue;
            }
            if (prefab.PrefabTag.Name.StartsWith("Compost") || prefab.PrefabTag.Name.EndsWith("Preview") || prefab.PrefabTag.Name.EndsWith("UnderConstruction") || prefab.PrefabTag.Name.EndsWith("_Preview") || prefab.PrefabTag.Name.EndsWith("_preview"))
            {
                if (prefab.PrefabTag.Name != "Compost")
                {
                    continue;
                }
            }
            var formattedName = GetFormatedUIImageFileName(prefab);
            Element element = ElementLoader.GetElement(prefab.PrefabTag);
            if (element != null)
            {
                var tupleUISprite = Def.GetUISprite(element);
                AnimTool.WriteUISpriteToFile(tupleUISprite.first, ExportIconDir, formattedName, tupleUISprite.second);
                this.AddUISpriteInfo(prefab, tupleUISprite);
            }
            else
            {
                Tuple<Sprite, Color> tupleUISprite = null;
                try
                {
                    tupleUISprite = Def.GetUISprite(prefab.PrefabTag);
                }
                catch (Exception e)
                {
                    //Debug.LogError("OniExtract: read " + prefab.PrefabTag.Name + " Failed.");
                }
                if (tupleUISprite != null)
                {
                    Sprite UISprite = tupleUISprite.first;
                    if (UISprite != null && UISprite != Assets.GetSprite("unknown"))
                    {
                        AnimTool.WriteUISpriteToFile(UISprite, ExportIconDir, formattedName);
                        this.AddUISpriteInfo(prefab, tupleUISprite);
                    }
                }
            }
        }
        string[] facadeSetIds = new string[] {
            Db.Get().Permits.BuildingFacades.Id,
            Db.Get().Permits.EquippableFacades.Id,
            Db.Get().Permits.ArtableStages.Id,
            Db.Get().Permits.StickerBombs.Id,
            Db.Get().Permits.ClothingItems.Id,
            Db.Get().Permits.BalloonArtistFacades.Id
        };
        foreach (string facadeSetId in facadeSetIds)
        {
            if (!Db.Get().Permits.Permits.Keys.Contains(facadeSetId))
            {
                continue;
            }
            string ExportFacadeDir = Path.Combine(Util.RootFolder(), "export", "ui_image_facade", facadeSetId);
            foreach (PermitResource permitResource in Db.Get().Permits.Permits[facadeSetId])
            {
                if (!(permitResource.Id == "Default"))
                {
                    Sprite UISprite = permitResource.GetPermitPresentationInfo().sprite;
                    
                    if (UISprite != null && UISprite != Assets.GetSprite("unknown"))
                    {
                        AnimTool.WriteUISpriteToFile(UISprite, ExportFacadeDir, 
                            UI.StripLinkFormatting(GetFacadeUIImageFileName(permitResource)));
                        this.AddFacadeInfos(permitResource.Id, UISprite);
                    }
                }
            }
        }
        string ExportFacadeDir2 = Path.Combine(Util.RootFolder(), "export", "ui_image_facade", Db.Get().Permits.MonumentParts.Id);
        foreach (MonumentPartResource monumentPart in Db.GetMonumentParts().resources)
        {
            if (!(monumentPart.Id == "Default"))
            {
                Sprite UISprite = Def.GetUISpriteFromMultiObjectAnim(monumentPart.AnimFile, monumentPart.State, false, monumentPart.SymbolName);
                if (UISprite != null && UISprite != Assets.GetSprite("unknown"))
                {
                    AnimTool.WriteUISpriteToFile(UISprite, ExportFacadeDir2, monumentPart.Id);
                    this.AddFacadeInfos(monumentPart.Id, UISprite);
                }
            }
        }
    }

    private string GetFacadeUIImageFileName(PermitResource permitResource)
    {
        return SingletonOptions<ModOptions>.Instance.SaveUIFileName == ModOptions.SaveNameMod.ID
            ? permitResource.Id
            : permitResource.Name;
    }
    
    private string GetFormatedUIImageFileName(KPrefabID prefab)
    {
        if (SingletonOptions<ModOptions>.Instance.SaveUIFileName == ModOptions.SaveNameMod.ID)
            return prefab.PrefabTag.Name;

        var properName = TagManager.GetProperName(prefab.PrefabID(), true);
        if (!properName.Equals("")) return properName;
        var instance = Assets.GetPrefab(prefab.PrefabTag);
        return instance == null ? prefab.PrefabTag.Name : UI.StripLinkFormatting(instance.GetProperName());
    }
}
