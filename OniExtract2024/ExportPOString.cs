using OniExtract2024;
using System;
using System.Collections.Generic;
using System.Reflection;

public class ExportPOString : BaseExport
{
    public override string ExportFileName { get; set; } = "po_string";
    public Dictionary<string, string> BUILDING = new Dictionary<string, string>();
    public Dictionary<string, string> BUILDINGS = new Dictionary<string, string>();
    public Dictionary<string, string> CLUSTER_NAMES = new Dictionary<string, string>();
    public Dictionary<string, string> CODEX = new Dictionary<string, string>();
    public Dictionary<string, string> COLONY_ACHIEVEMENTS = new Dictionary<string, string>();
    public Dictionary<string, string> CREATURES = new Dictionary<string, string>();
    public Dictionary<string, string> DUPLICANTS = new Dictionary<string, string>();
    public Dictionary<string, string> ELEMENTS = new Dictionary<string, string>();
    public Dictionary<string, string> EQUIPMENT = new Dictionary<string, string>();
    public Dictionary<string, string> GAMEPLAY_EVENTS = new Dictionary<string, string>();
    public Dictionary<string, string> INPUT = new Dictionary<string, string>();
    public Dictionary<string, string> INPUT_BINDINGS = new Dictionary<string, string>();
    public Dictionary<string, string> ITEMS = new Dictionary<string, string>();
    public Dictionary<string, string> LORE = new Dictionary<string, string>();
    public Dictionary<string, string> MISC = new Dictionary<string, string>();
    public Dictionary<string, string> NAMEGEN = new Dictionary<string, string>();
    public Dictionary<string, string> RESEARCH = new Dictionary<string, string>();
    public Dictionary<string, string> ROBOTS = new Dictionary<string, string>();
    public Dictionary<string, string> ROOMS = new Dictionary<string, string>();
    public Dictionary<string, string> SETITEMS = new Dictionary<string, string>();
    public Dictionary<string, string> STICKERNAMES = new Dictionary<string, string>();
    public Dictionary<string, string> SUBWORLDS = new Dictionary<string, string>();
    public Dictionary<string, string> UI = new Dictionary<string, string>();
    public Dictionary<string, string> VIDEOS = new Dictionary<string, string>();
    public Dictionary<string, string> WORLD_TRAITS = new Dictionary<string, string>();
    public Dictionary<string, string> WORLDS = new Dictionary<string, string>();

    public ExportPOString()
	{
    }

    public string getFieldInfoValue(FieldInfo field, Type type)
    {
        if (field.IsStatic)
        {
            return field.GetValue(null).ToString();
        }
        else
        {
            var instance = Activator.CreateInstance(type);
            return field.GetValue(instance).ToString();
        }
    }

    public int Traverse(Type type, string parentTypeName, Dictionary<string, string> poDict)
    {
        // 输出类名
        //Debug.Log("Class: " + parentTypeName);

        // 输出类的所有成员
        foreach (var member in type.GetMembers(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance))
        {
            string typeName = parentTypeName + "." + member.Name;
            // 判断成员类型是否为 LocString
            if (member is FieldInfo fieldInfo && fieldInfo.FieldType == typeof(LocString))
            {
                //Debug.Log("LocString fieldInfo: " + member.Name);
                string valueStr = getFieldInfoValue(fieldInfo, type);
                if (valueStr != null)
                {
                    poDict[typeName] = valueStr;
                }
            }
            //else if (member is PropertyInfo propertyInfo && propertyInfo.PropertyType == typeof(LocString))
            //{
            //    Debug.Log("LocString propertyInfo: " + member.Name);
            //    var instance = Activator.CreateInstance(type);
            //    poDict[typeName] = propertyInfo.GetValue(instance).ToString();
            //}
        }

        if (type.GetNestedTypes() != null && type.GetNestedTypes().Length > 0)
        {
            // 递归遍历子类
            foreach (var nestedType in type.GetNestedTypes())
            {
                string typeName = parentTypeName + "." + nestedType.Name;
                Traverse(nestedType, typeName, poDict);
            }
        }

        return type.GetNestedTypes().Length;
    }

    public void ExportAll()
    {
        Type type1 = typeof(STRINGS.BUILDING);
        Traverse(type1, type1.Name, BUILDING);
        Type type2 = typeof(STRINGS.BUILDINGS);
        Traverse(type2, type2.Name, BUILDINGS);
        Type type3 = typeof(STRINGS.CLUSTER_NAMES);
        Traverse(type3, type3.Name, CLUSTER_NAMES);
        Type type4 = typeof(STRINGS.CODEX);
        Traverse(type4, type4.Name, CODEX);
        Type type5 = typeof(STRINGS.COLONY_ACHIEVEMENTS);
        Traverse(type5, type5.Name, COLONY_ACHIEVEMENTS);
        Type type6 = typeof(STRINGS.CREATURES);
        Traverse(type6, type6.Name, CREATURES);
        Type type7 = typeof(STRINGS.DUPLICANTS);
        Traverse(type7, type7.Name, DUPLICANTS);
        Type type8 = typeof(STRINGS.ELEMENTS);
        Traverse(type8, type8.Name, ELEMENTS);
        Type type9 = typeof(STRINGS.EQUIPMENT);
        Traverse(type9, type9.Name, EQUIPMENT);
        Type type10 = typeof(STRINGS.GAMEPLAY_EVENTS);
        Traverse(type10, type10.Name, GAMEPLAY_EVENTS);
        Type type11 = typeof(STRINGS.INPUT);
        Traverse(type11, type11.Name, INPUT);
        Type type12 = typeof(STRINGS.INPUT_BINDINGS);
        Traverse(type12, type12.Name, INPUT_BINDINGS);
        Type type13 = typeof(STRINGS.ITEMS);
        Traverse(type13, type13.Name, ITEMS);
        Type type14 = typeof(STRINGS.LORE);
        Traverse(type14, type14.Name, LORE);
        Type type15 = typeof(STRINGS.MISC);
        Traverse(type15, type15.Name, MISC);
        Type type16 = typeof(STRINGS.NAMEGEN);
        Traverse(type16, type16.Name, NAMEGEN);
        Type type17 = typeof(STRINGS.RESEARCH);
        Traverse(type17, type17.Name, RESEARCH);
        Type type18 = typeof(STRINGS.ROBOTS);
        Traverse(type18, type18.Name, ROBOTS);
        Type type19 = typeof(STRINGS.ROOMS);
        Traverse(type19, type19.Name, ROOMS);
        Type type20 = typeof(STRINGS.SETITEMS);
        Traverse(type20, type20.Name, SETITEMS);
        Type type21 = typeof(STRINGS.STICKERNAMES);
        Traverse(type21, type21.Name, STICKERNAMES);
        Type type22 = typeof(STRINGS.SUBWORLDS);
        Traverse(type22, type22.Name, SUBWORLDS);
        Type type23 = typeof(STRINGS.UI);
        Traverse(type23, type23.Name, UI);
        Type type24 = typeof(STRINGS.VIDEOS);
        Traverse(type24, type24.Name, VIDEOS);
        Type type25 = typeof(STRINGS.WORLD_TRAITS);
        Traverse(type25, type25.Name, WORLD_TRAITS);
        Type type26 = typeof(STRINGS.WORLDS);
        Traverse(type26, type26.Name, WORLDS);
    }
}
