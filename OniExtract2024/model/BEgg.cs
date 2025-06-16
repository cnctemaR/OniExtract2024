using System.Collections.Generic;

namespace OniExtract2024
{
    public class BEgg
    {
        public string name;
        public string nameString;
        public BKprefabID kPrefabID;
        public HashSet<Tag> tags;
        public BVector2 kBoxCollider2D;
        public OutIncubationMonitorDef incubatorMonitorDef;
        public OutPickupable pickupable;
        public OutPrimaryElement primaryElement;

        public BEgg(string name, KPrefabID kPrefabID)
        {
            this.name = name;
            this.kPrefabID = new BKprefabID(kPrefabID);
            this.tags = kPrefabID.Tags;
        }

    }
}
