using System.Collections.Generic;

namespace OniExtract2024
{
    public class OutScheduleGroup
    {
        public string Name;
        public string Id;
        public HashedString IdHash;
        public bool Disabled;
        public ResourceGuid Guid;
        public int defaultSegments;
        public string description;
        public string notificationTooltip;
        public List<OutScheduleBlockType> allowedTypes = new List<OutScheduleBlockType>();
        public bool alarm;

        public OutScheduleGroup(ScheduleGroup obj)
        {
            this.Name = obj.Name;
            this.Id = obj.Id;
            this.IdHash = obj.IdHash;
            this.Disabled = obj.Disabled;
            this.Guid = obj.Guid;
            this.defaultSegments = obj.defaultSegments;
            this.description = obj.description;
            this.notificationTooltip = obj.notificationTooltip;
            foreach (ScheduleBlockType type in obj.allowedTypes)
            {
                this.allowedTypes.Add(new OutScheduleBlockType(type));
            }
            this.alarm = obj.alarm;
        }
    }
}
