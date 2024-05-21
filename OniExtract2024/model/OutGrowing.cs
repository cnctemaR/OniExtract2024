using Klei.AI;

namespace OniExtract2024
{
    public class OutGrowing
    {
        public bool shouldGrowOld = true;
        public float maxAge = 2400f;
        public float minRads;
        public float maxRads;

        public OutGrowing(Growing obj)
        {
            this.shouldGrowOld = obj.shouldGrowOld;
            this.maxAge = obj.maxAge;
            Modifiers component1 = obj.gameObject.GetComponent<Modifiers>();
            this.minRads = component1.GetPreModifiedAttributeValue(Db.Get().PlantAttributes.MinRadiationThreshold);
            this.maxRads = component1.GetPreModifiedAttributeValue(Db.Get().PlantAttributes.MaxRadiationThreshold);
        }
    }
}
