using OniExtract2024;
using System.Collections.Generic;

public class ExportCodex : BaseExport
{
    public override string ExportFileName { get; set; } = "codex";
    public Dictionary<string, OutCodexEntry> categoryEntries = new Dictionary<string, OutCodexEntry>();

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
}
