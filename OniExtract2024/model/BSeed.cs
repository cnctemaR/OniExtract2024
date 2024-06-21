using System.Collections.Generic;

namespace OniExtract2024
{
    public class BSeed
    {
        public string name;
        public string nameString;
        public HashSet<Tag> tags;
        public BVector2 kBoxCollider2D;
        public OutIncubationMonitorDef incubatorMonitorDef;
        public OutPickupable pickupable;
        public OutPrimaryElement primaryElement;
        public PlantableSeed plantableSeed;
        public MutantPlant mutantPlant;

        public BSeed(string name, HashSet<Tag> tags)
        {
            this.name = name;
            this.tags = tags;
        }

    }
}
