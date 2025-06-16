using static Health;

namespace OniExtract2024
{
    public class OutHealth
    {
        public HealthState State;
        public bool canBeIncapacitated;
        public HealthBar healthBar;
        public bool isCritter = false;

        public OutHealth(Health obj)
        {
            this.State = obj.State;
            this.canBeIncapacitated = obj.canBeIncapacitated;
            this.healthBar = obj.healthBar;
            this.isCritter = obj.isCritter;
        }
    }
}
