using OniExtract2024;
using System.Collections.Generic;

public class ExportElement : BaseExport
{
    public override string ExportFileName { get; set; } = "elements";
    public Dictionary<int, OutElement> elementTable = new Dictionary<int, OutElement>();

    public ExportElement()
    {
    }

    public void AddAllElement()
    {
        foreach (var keyValue in ElementLoader.elementTable)
        {
            this.elementTable[keyValue.Key] = OutElement.GetOutElement(keyValue.Value);
        }
    }
}
