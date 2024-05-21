using UnityEngine;

namespace OniExtract2024
{
    public class OutPassiveElementConsumer
    {
        [HashedEnum]
        [SerializeField]
        public SimHashes elementToConsume = SimHashes.Vacuum;
        public float consumptionRate;
        public byte consumptionRadius = 1;
        public float minimumMass;
        public bool showInStatusPanel = true;
        public float capacityKG = float.PositiveInfinity;
        public ElementConsumer.Configuration configuration;
        public float consumedMass;
        public float consumedTemperature;
        public bool storeOnConsume;
        public OutStorage storage;
        public bool ignoreActiveChanged;
        public bool showDescriptor = true;
        public bool isRequired = true;

        public OutPassiveElementConsumer(ElementConsumer obj)
        {
            this.elementToConsume = obj.elementToConsume;
            this.consumptionRate = obj.consumptionRate;
            this.consumptionRadius = obj.consumptionRadius;
            this.minimumMass = obj.minimumMass;
            this.showInStatusPanel = obj.showInStatusPanel;
            this.capacityKG = obj.capacityKG;
            this.configuration = obj.configuration;
            this.consumedMass = obj.consumedMass;
            this.consumedTemperature = obj.consumedTemperature;
            this.storeOnConsume = obj.storeOnConsume;
            if (obj.storage != null)
            {
                this.storage = new OutStorage(obj.storage);
            }
            else
            {
                this.storage = null;
            }
            this.ignoreActiveChanged = obj.ignoreActiveChanged;
            this.showDescriptor = obj.showDescriptor;
            this.isRequired = obj.isRequired;
        }
    }
}
