using System.Collections.Generic;

namespace OniExtract2024
{
    public class OutChoreGroup
    {
        public string Name;
        public string Id;
        public HashedString IdHash;
        public bool Disabled;
        public ResourceGuid Guid;
        public List<string> choreTypeIDs = new List<string>();
        public Klei.AI.Attribute attribute;
        public string description;
        public string sprite;
        public bool userPrioritizable;
        public int DefaultPersonalPriority;

        public OutChoreGroup(ChoreGroup obj)
        {
            this.Name = obj.Name;
            this.Id = obj.Id;
            this.IdHash = obj.IdHash;
            this.Disabled = obj.Disabled;
            this.Guid = obj.Guid;
            foreach (ChoreType choreType in obj.choreTypes)
            {
                this.choreTypeIDs.Add(choreType.Id);
            }
            this.attribute = obj.attribute;
            this.description = obj.description;
            this.sprite = obj.sprite;
            this.userPrioritizable = obj.userPrioritizable;
            this.DefaultPersonalPriority = obj.DefaultPersonalPriority;
        }
    }
}
