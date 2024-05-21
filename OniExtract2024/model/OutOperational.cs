using System.Collections.Generic;
using static Operational;

namespace OniExtract2024
{
    public class OutOperational
    {
        public float activeStartTime;
        public Dictionary<Flag, bool> flags;
        public float inactiveStartTime;

        public OutOperational(Operational obj)
        {
            this.flags = new Dictionary<Flag, bool>();
            this.activeStartTime = obj.activeStartTime;
            this.flags = obj.Flags;
            this.inactiveStartTime = obj.inactiveStartTime;
        }
    }
}