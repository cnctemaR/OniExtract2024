using UnityEngine;

namespace OniExtract2024
{
    public class OutOutputElement
    {
        public bool IsActive;
        public SimHashes elementHash;
        public float minOutputTemperature;
        public bool useEntityTemperature;
        public float massGenerationRate;
        public bool storeOutput;
        public Vector2 outputElementOffset;
        public HandleVector<int>.Handle accumulator;
        public float diseaseWeight;
        public byte addedDiseaseIdx;
        public int addedDiseaseCount;
        public string Name;

        public OutOutputElement(ElementConverter.OutputElement obj)
        {
            this.IsActive = obj.IsActive;
            this.elementHash = obj.elementHash;
            this.minOutputTemperature = obj.minOutputTemperature;
            this.useEntityTemperature = obj.useEntityTemperature;
            this.massGenerationRate = obj.massGenerationRate;
            this.storeOutput = obj.storeOutput;
            this.outputElementOffset = obj.outputElementOffset;
            this.accumulator = obj.accumulator;
            this.diseaseWeight = obj.diseaseWeight;
            this.addedDiseaseIdx = obj.addedDiseaseIdx;
            this.addedDiseaseCount = obj.addedDiseaseCount;
            this.Name = obj.Name;
        }
    }
}