using System.Collections.Generic;

namespace OniExtract2024
{
    public class BEgg
    {
        public string name;
        public string nameString;
        public string[] dlcIds = null;
        public HashSet<Tag> tags;
        public BVector2 kBoxCollider2D;
        public OutIncubationMonitorDef incubatorMonitorDef;
        public OutPickupable pickupable;
        public OutPrimaryElement primaryElement;

        public BEgg(string name, HashSet<Tag> tags)
        {
            this.name = name;
            this.tags = tags;
        }

    }
}
