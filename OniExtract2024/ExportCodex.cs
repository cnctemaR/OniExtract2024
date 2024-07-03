using OniExtract2024;
using System.Collections.Generic;
using UnityEngine;

public class ExportCodex : BaseExport
{
    public override string ExportFileName { get; set; } = "codex";
    public Dictionary<string, OutCodexEntry> categoryEntries = new Dictionary<string, OutCodexEntry>();
    public List<BTreeNode> categoryTree = new List<BTreeNode>();
    public List<BEffectDesc> effectDescs = new List<BEffectDesc>();

    public ExportCodex()
    {
    }

    public void AddCategoryEntry(Dictionary<string, CodexEntry> categoryEntries)
    {
        foreach (var entry in categoryEntries)
        {
            //Debug.Log("OniExtract: entry:"+entry.Key+" Value: "+entry.Value.name);
            this.categoryEntries[entry.Key] = new OutCodexEntry(entry.Value);
        }
    }

    public void AddCategoryTree(Dictionary<string, CodexEntry> categoryEntries)
    {
        List<BTreeNode> nodes = new List<BTreeNode>();
        foreach (var entry in categoryEntries)
        {
            CodexEntry codexEntry = entry.Value;
            BTreeNode node = new BTreeNode(codexEntry.id);
            if(codexEntry.parentId != null)
            {
                node.parentName = codexEntry.parentId;
            }
            nodes.Add(node);
            if (codexEntry.subEntries != null)
            {
                foreach(var subEntry in codexEntry.subEntries)
                {
                    BTreeNode node2 = new BTreeNode(subEntry.id);
                    if (subEntry.parentEntryID != null)
                    {
                        node2.parentName = subEntry.parentEntryID;
                    }
                    nodes.Add(node2);
                }
            }
        }
        this.categoryTree = BTreeNode.BuildTree(nodes);
    }

    public void AddEffectDesc(GameObject go, List<Descriptor> descs, string effectId, bool increaseIndent)
    {
        if (go == null) return;
        KPrefabID prefabID = go.GetComponent<KPrefabID>();
        if (prefabID != null)
        {
            BEffectDesc effect = new BEffectDesc(prefabID.PrefabID().Name, descs, effectId, increaseIndent);
            this.effectDescs.Add(effect);
        }
    }
}
