
namespace OniExtract2024
{
    public class OutTechItem
    {
        public string Name;
        public string Id;
        public HashedString IdHash;
        public bool Disabled;
        public ResourceGuid Guid;
        public string description;
        public string parentTechId;
        public string[] dlcIds;

        public OutTechItem(TechItem obj)
        {
            this.Name = obj.Name;
            this.Id = obj.Id;
            this.IdHash = obj.IdHash;
            this.Disabled = obj.Disabled;
            this.Guid = obj.Guid;
            this.description = obj.description;
            this.parentTechId = obj.parentTechId;
            this.dlcIds = obj.dlcIds;
        }
    }
}
