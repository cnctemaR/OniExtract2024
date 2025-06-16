using System.Collections.Generic;

namespace OniExtract2024
{
    public class OutTech
    {
        public string Name;
        public string Id;
        public HashedString IdHash;
        public bool Disabled;
        public ResourceGuid Guid;
        public List<string> requiredTechIDs = new List<string>();
        public List<string> unlockedTechIDs = new List<string>();
        public List<OutTechItem> unlockedItems = new List<OutTechItem>();
        public List<string> unlockedItemIDs = new List<string>();
        public int tier;
        public Dictionary<string, float> costsByResearchTypeID;
        public string desc;
        public string category;
        public List<string> searchTerms;
        public float width;
        public float height;

        public OutTech(Tech obj)
        {
            this.Name = obj.Name;
            this.Id = obj.Id;
            this.IdHash = obj.IdHash;
            this.Disabled = obj.Disabled;
            this.Guid = obj.Guid;
            this.requiredTechIDs = getOutTechIDList(obj.requiredTech);
            this.unlockedTechIDs = getOutTechIDList(obj.unlockedTech);
            this.unlockedItems = getOutTechItemList(obj.unlockedItems);
            this.unlockedItemIDs = obj.unlockedItemIDs;
            this.tier = obj.tier;
            this.costsByResearchTypeID = obj.costsByResearchTypeID;
            this.desc = obj.desc;
            this.category = obj.category;
            this.searchTerms = obj.searchTerms;
            this.width = obj.width;
            this.height = obj.height;

        }

        private static List<string> getOutTechIDList(List<Tech> techList)
        {
            var result = new List<string>();
            foreach (Tech item in techList)
            {
                result.Add(item.Id);
            }

            return result;
        }

        private static List<OutTechItem> getOutTechItemList(List<TechItem> techItems)
        {
            var result = new List<OutTechItem>();
            foreach (TechItem item in techItems)
            {
                result.Add(new OutTechItem(item));
            }
            return result;
        }
    }
}
