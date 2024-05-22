
namespace OniExtract2024
{
    public class OutScheduleBlockType
    {
        public string Name;
        public string Id;
        public HashedString IdHash;
        public bool Disabled;
        public ResourceGuid Guid;
        public BColor color;
        public string description;

        public OutScheduleBlockType(ScheduleBlockType obj)
        {
            this.Name = obj.Name;
            this.Id = obj.Id;
            this.IdHash = obj.IdHash;
            this.Disabled = obj.Disabled;
            this.Guid = obj.Guid;
            this.color = new BColor(obj.color);
            this.description = obj.description;
        }
    }
}
