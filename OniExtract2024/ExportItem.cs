using OniExtract2024;
using System.Collections.Generic;

public class ExportItem : BaseExport
{
    public override string ExportFileName { get; set; } = "items";
    public List<EquipmentDef> EquipmentDefs = new List<EquipmentDef>();

    public ExportItem()
    {
    }

    public void AddEquipmentDef(IEquipmentConfig config)
    {
        if (!DlcManager.IsDlcListValidForCurrentContent(config.GetDlcIds()))
        {
            return;
        }
        this.EquipmentDefs.Add(config.CreateEquipmentDef());
    }
}
