
namespace OniExtract2024
{
    public class OutDrowningMonitor
    {
        public bool canDrownToDeath;
        public bool livesUnderWater;

        public OutDrowningMonitor(DrowningMonitor obj)
        {
            this.canDrownToDeath = obj.canDrownToDeath;
            this.livesUnderWater = obj.livesUnderWater;
        }
    }
}
