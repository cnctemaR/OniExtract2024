
namespace OniExtract2024
{
    public class OutConsumedElement
    {
        public HandleVector<int>.Handle Accumulator;
        public bool IsActive;
        public float MassConsumptionRate;
        public Tag Tag;

        public OutConsumedElement(ElementConverter.ConsumedElement obj)
        {
            this.Accumulator = obj.Accumulator;
            this.IsActive = obj.IsActive;
            this.MassConsumptionRate = obj.MassConsumptionRate;
            this.Tag = obj.Tag;            
        }
    }
}