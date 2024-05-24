using OniExtract2024;
using OniExtract2024.utils;
using System;
using System.Collections.Generic;
using System.IO;
using PeterHan.PLib.Core;
using PeterHan.PLib.Options;
using UnityEngine;

public class ExportUISprite : BaseExport
{
    public override string ExportFileName { get; set; } = "uiSpriteInfo";
    public Dictionary<string, BUISprite> uiSpriteInfos = new Dictionary<string, BUISprite>();

    public ExportUISprite()
    {
    }

    public void AddUISpriteInfo(KPrefabID prefabID, Tuple<Sprite, Color> tupleUISprite)
    {
        this.uiSpriteInfos[prefabID.PrefabTag.Name] = new BUISprite(prefabID.name, tupleUISprite.first, tupleUISprite.second);
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
    }

    private string GetFormatedUIImageFileName(KPrefabID prefab) {
        
        var pattern = SingletonOptions<ModOptions>.Instance.UINamePattern;
        // 如果 pattern 设置为 "ui{0} {1}" 这种用序号作为占位符的，可以用
        // var var1 = "tag";
        // var var2 = "datetime";
        //
        // return string.Format(pattern, var1, var2); 
        // 或者
        // return pattern.F(var1, var2);
        return pattern.Replace("{tag}", prefab.PrefabTag.Name);
    }
}
