using System.Collections.Generic;

namespace OniExtract2024
{
    public class OutChoreType
    {
        public string Name;
        public string Id;
        public HashedString IdHash;
        public bool Disabled;
        public ResourceGuid Guid;
        public OutStatusItem statusItem;
        public HashSet<Tag> tags = new HashSet<Tag>();
        public HashSet<Tag> interruptExclusion;
        public string reportName;
        public Urge urge;
        public List<OutChoreGroup> groups = new List<OutChoreGroup>();
        public int priority;
        public int interruptPriority;
        public int explicitPriority;

        public OutChoreType(ChoreType obj)
        {
            this.Name = obj.Name;
            this.Id = obj.Id;
            this.IdHash = obj.IdHash;
            this.Disabled = obj.Disabled;
            this.Guid = obj.Guid;
            this.statusItem = new OutStatusItem(obj.statusItem);
            this.tags = obj.tags;
            this.interruptExclusion = obj.interruptExclusion;
            this.reportName = obj.reportName;
            this.urge = obj.urge;
            foreach (ChoreGroup choreGroup in obj.groups)
            {
                this.groups.Add(new OutChoreGroup(choreGroup));
            }
            this.priority = obj.priority;
            this.interruptPriority = obj.interruptPriority;
            this.explicitPriority = obj.explicitPriority;
        }
    }
}
