using KSerialization;
using UnityEngine;

namespace OniExtract2024
{
    public class OutOxygenBreather
    {
        public float O2toCO2conversion = 0.5f;
        public float lowOxygenThreshold;
        public float noOxygenThreshold;
        public BVector2 mouthOffset;
        [Serialize]
        public float accumulatedCO2;
        [SerializeField]
        public float minCO2ToEmit = 0.3f;
        private bool IsSuffocating = false;
        public CellOffset[] breathableCells;

        public OutOxygenBreather(OxygenBreather obj)
        {
            this.O2toCO2conversion = obj.O2toCO2conversion;
            this.lowOxygenThreshold = obj.lowOxygenThreshold;
            this.noOxygenThreshold = (float)obj.noOxygenThreshold;
            this.mouthOffset = new BVector2(obj.mouthOffset);
            this.accumulatedCO2 = obj.accumulatedCO2;
            this.minCO2ToEmit = obj.minCO2ToEmit;
            this.IsSuffocating = obj.IsSuffocating;
            this.breathableCells = obj.breathableCells;
        }
    }
}
