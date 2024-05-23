
namespace OniExtract2024
{
    public class OutIlluminationVulnerable
    {
        public bool prefersDarkness;
        public int LightIntensityThreshold;

        public OutIlluminationVulnerable(IlluminationVulnerable obj)
        {
            this.prefersDarkness = obj.prefersDarkness;
            this.LightIntensityThreshold = obj.LightIntensityThreshold;
        }
    }
}
