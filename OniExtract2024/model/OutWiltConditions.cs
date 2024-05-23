using Klei.AI;

namespace OniExtract2024
{
    public class OutWiltConditions
    {
        public float WiltDelay = 1f;
        public float RecoveryDelay = 1f;
        public float minRad = 0f;
        public float maxRad = float.NaN;

        public OutWiltConditions(WiltCondition wiltable, Modifiers componentMods)
        {
            this.WiltDelay = wiltable.WiltDelay;
            this.RecoveryDelay = wiltable.RecoveryDelay;
            this.minRad = componentMods.GetPreModifiedAttributeValue(Db.Get().PlantAttributes.MinRadiationThreshold);
            this.maxRad = componentMods.GetPreModifiedAttributeValue(Db.Get().PlantAttributes.MaxRadiationThreshold);
        }
    }
}