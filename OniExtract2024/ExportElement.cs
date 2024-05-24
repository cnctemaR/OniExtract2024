using OniExtract2024;
using System.Collections.Generic;
using UnityEngine;

public class ExportElement : BaseExport
{
    public override string ExportFileName { get; set; } = "elements";
    public Dictionary<int, Element> elementTable = new Dictionary<int, Element>();

    public ExportElement()
    {
    }

    public void AddAllElement()
    {
        foreach (Element element in ElementLoader.elements)
        {
            //Debug.Log(element.tag.Name);
            this.elementTable[((int)element.id)] = element;

            Substance substance = this.elementTable[((int)element.id)].substance;
            if (substance != null)
            {
                if (substance.material != null)
                {
                    if (!substance.material.HasProperty("_Color"))
                    {
                        //Debug.Log("No Property: " + element.tag.Name);
                        this.elementTable[((int)element.id)].substance.material.color = Color.clear;
                    }                    
                }
            }
        }
    }
}
