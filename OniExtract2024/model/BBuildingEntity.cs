using System.Collections.Generic;

namespace OniExtract2024
{
    public class BBuildingEntity
    {
        public string name;
        public OutEnergyGenerator energyGenerator;
        public OutConduitConsumer conduitConsumer;
        public OutConduitDispenser conduitDispenser;
        public OutPlantablePlot plantablePlot;
        public List<OutElementConverter> elementConverters = new List<OutElementConverter>();
        public List<OutElementConsumer> elementConsumers = new List<OutElementConsumer>();
        public List<OutPassiveElementConsumer> passiveElementConsumers = new List<OutPassiveElementConsumer>();
        public OutStorage storage = null;

        public BBuildingEntity(string name)
        {
            this.name = name;
        }
    }
}