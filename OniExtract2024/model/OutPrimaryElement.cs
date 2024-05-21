
namespace OniExtract2024
{
    public class OutPrimaryElement
    {
        public int DiseaseCount;
        public string Name;
        public float InternalTemperature;
        public float Mass;
        public float Temperature;
        public float Units;

        public OutPrimaryElement(PrimaryElement obj)
        {
            this.DiseaseCount = obj.DiseaseCount;
            this.Name = obj.Element.tag.Name;
            this.InternalTemperature = obj.InternalTemperature;
            this.Mass = obj.Mass;
            this.Temperature = obj.Temperature;
            this.Units = obj.Units;
        }
    }
}
