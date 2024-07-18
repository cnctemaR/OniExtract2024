
namespace OniExtract2024
{
    public class OutPrimaryElement
    {
        public string Name;
        public float InternalTemperature;
        public float Mass;
        public float Temperature;
        public int DiseaseCount;
        public int DiseaseIdx;
        public float Units;

        public OutPrimaryElement(PrimaryElement obj)
        {
            if (!(obj.ElementID == null || obj.ElementID == 0))
            {
                this.Name = obj.Element.tag.Name;
            }
            this.InternalTemperature = obj.InternalTemperature;
            this.Mass = obj.Mass;
            this.DiseaseCount = obj.DiseaseCount;
            this.DiseaseIdx = obj.DiseaseIdx;
            this.Temperature = obj.Temperature;
            this.Units = obj.Units;
        }
    }
}
