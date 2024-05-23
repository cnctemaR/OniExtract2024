using System.Collections.Generic;

namespace OniExtract2024
{
    public class OutPressureVulnerable
    {
        public float pressureLethal_Low;
        public float pressureWarning_Low;
        public float pressureWarning_High;
        public float pressureLethal_High;
        public bool pressure_sensitive;
        public HashSet<string> safe_atmospheres;
        public bool testAreaElementSafe;

        public OutPressureVulnerable(PressureVulnerable obj)
        {
            this.pressureLethal_Low = obj.pressureLethal_Low;
            this.pressureWarning_Low = obj.pressureWarning_Low;
            this.pressureWarning_High = obj.pressureWarning_High;
            this.pressureLethal_High = obj.pressureLethal_High;
            this.pressure_sensitive = obj.pressure_sensitive;
            this.safe_atmospheres = new HashSet<string>();
            foreach (Element item in obj.safe_atmospheres)
            {
                this.safe_atmospheres.Add(item.tag.Name.ToString());
            }
            this.testAreaElementSafe = obj.testAreaElementSafe;
        }
    }
}
