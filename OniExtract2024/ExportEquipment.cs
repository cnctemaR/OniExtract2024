using OniExtract2024;
using System.Collections.Generic;

public class ExportEquipment : BaseExport
{
    public override string ExportFileName { get; set; } = "equipment";
    public List<EquipmentDef> EquipmentDefs = new List<EquipmentDef>();

    public ExportEquipment()
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
