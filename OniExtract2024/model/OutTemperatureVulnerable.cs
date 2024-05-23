
namespace OniExtract2024
{
    public class OutTemperatureVulnerable
    {
        public float temperatureLethalLow;
        public float temperatureLethalHigh;
        public float temperatureWarningLow;
        public float temperatureWarningHigh;
        public OutTemperatureVulnerable(float temperatureLethalLow, float temperatureLethalHigh, float temperatureWarningLow, float temperatureWarningHigh)
        {
            this.temperatureLethalLow = temperatureLethalLow;
            this.temperatureLethalHigh = temperatureLethalHigh;
            this.temperatureWarningLow = temperatureWarningLow;
            this.temperatureWarningHigh = temperatureWarningHigh;
        }

        public OutTemperatureVulnerable(TemperatureVulnerable obj)
        {
            this.temperatureLethalLow = obj.TemperatureLethalLow;
            this.temperatureLethalHigh = obj.TemperatureLethalHigh;
            this.temperatureWarningLow = obj.TemperatureWarningLow;
            this.temperatureWarningHigh = obj.TemperatureWarningHigh;
        }
    }
}
