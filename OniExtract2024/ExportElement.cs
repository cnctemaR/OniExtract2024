using OniExtract2024;
using System.Collections.Generic;

public class ExportElement : BaseExport
{
    public override string ExportFileName { get; set; } = "elements";
    public Dictionary<int, BElement> elementTable = new Dictionary<int, BElement>();

    public ExportElement()
    {
    }

    public void AddAllElement()
    {
        foreach (Element element in ElementLoader.elements)
        {
            elementTable[((int)element.id)] = new BElement(element);
        }
    }
}
