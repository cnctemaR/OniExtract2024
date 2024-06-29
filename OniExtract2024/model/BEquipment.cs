using System;
using System.Collections.Generic;
using System.Linq;

namespace OniExtract2024
{
    public class BEquipment
    {
        public string name;
        public string nameString;
        public string[] dlcIds = null;
        public HashSet<Tag> tags;
        public BVector2 kBoxCollider2D;
        public OutSuitTank suitTank;
        public Durability durability;
        public LeadSuitTank leadSuitTank;
        public OutStorage storage;
        public OutPickupable pickupable;
        public OutPrimaryElement primaryElement;

        public BEquipment(string name, HashSet<Tag> tags)
        {
            this.name = name;
            this.tags = tags;
        }

    }
}
