using static Health;

namespace OniExtract2024
{
    public class OutHealth
    {
        public HealthState State;
        public bool CanBeIncapacitated;
        public HealthBar healthBar;

        public OutHealth(Health obj)
        {
            this.State = obj.State;
            this.CanBeIncapacitated = obj.CanBeIncapacitated;
            this.healthBar = obj.healthBar;
        }
    }
}
