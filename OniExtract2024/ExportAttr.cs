using OniExtract2024;
using System.Collections.Generic;
using Klei.AI;
using System;
using static Klei.AI.Sickness;

public class ExportAttr : BaseExport
{
    public override string ExportFileName { get; set; } = "attribute";
    public Dictionary<string, OutSicknessComponent> sicknessComponent = new Dictionary<string, OutSicknessComponent>();
    public Dictionary<string, int> SicknessType = new Dictionary<string, int>();
    public Dictionary<string, int> Severity = new Dictionary<string, int>();
    public Dictionary<string, int> InfectionVector = new Dictionary<string, int>();
    public Dictionary<string, int> PermittedRotations = new Dictionary<string, int>();

    public ExportAttr()
    {
    }

    public void AddAllSicknessModifier()
    {
        foreach (Sickness resource in Db.Get().Sicknesses.resources)
        {
            OutSicknessComponent sicknessComponent = new OutSicknessComponent(resource.Id);
            AttributeModifierSickness modifiers = resource.GetSicknessComponent<AttributeModifierSickness>();
            sicknessComponent.modifiers = new OutAttributeModifierSickness(modifiers);
            CommonSickEffectSickness commonSickEffect = resource.GetSicknessComponent<CommonSickEffectSickness>();
            sicknessComponent.commonSickEffect = commonSickEffect;
            AnimatedSickness animated = resource.GetSicknessComponent<AnimatedSickness>();
            sicknessComponent.animated = animated;
            PeriodicEmoteSickness periodicEmote = resource.GetSicknessComponent<PeriodicEmoteSickness>();
            sicknessComponent.periodicEmote = periodicEmote;
            this.sicknessComponent.Add(resource.Id, sicknessComponent);
        }
    }

    public void AddAllEnumClass()
    {
        foreach (var name in Enum.GetNames(typeof(SicknessType)))
        {
            this.SicknessType[name] = (int)Enum.Parse(typeof(SicknessType), name);
        }
        foreach (var name in Enum.GetNames(typeof(Severity)))
        {
            this.Severity[name] = (int)Enum.Parse(typeof(Severity), name);
        }
        foreach (var name in Enum.GetNames(typeof(InfectionVector)))
        {
            this.InfectionVector[name] = (int)Enum.Parse(typeof(InfectionVector), name);
        }
        foreach (var name in Enum.GetNames(typeof(PermittedRotations)))
        {
            this.PermittedRotations[name] = (int)Enum.Parse(typeof(PermittedRotations), name);
        }
    }
}
