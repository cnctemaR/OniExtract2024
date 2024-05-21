using OniExtract2024;
using System.Collections.Generic;

public class ExportElement : BaseExport
{
    public override string ExportFileName { get; set; } = "elements";
    public Dictionary<int, Element> elementTable;

    public ExportElement()
    {
    }

    public void AddAllElement()
    {
        this.elementTable = ElementLoader.elementTable;
    }
}
