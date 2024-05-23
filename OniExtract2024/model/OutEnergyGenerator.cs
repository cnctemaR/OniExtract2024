using static EnergyGenerator;

namespace OniExtract2024
{
    public class OutEnergyGenerator
    {
        public bool hasMeter = true;
        public bool ignoreBatteryRefillPercent;
        public Formula formula;
        public Meter.Offset meterOffset;

        public OutEnergyGenerator(EnergyGenerator obj)
        {
            this.hasMeter = obj.hasMeter;
            this.ignoreBatteryRefillPercent = obj.ignoreBatteryRefillPercent;
            this.formula = obj.formula;
            this.meterOffset = obj.meterOffset;
        }
    }
}