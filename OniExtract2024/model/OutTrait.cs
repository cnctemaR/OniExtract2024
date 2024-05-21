using Klei.AI;
using System.Collections.Generic;

namespace OniExtract2024
{
    public class OutTrait
    {
        public string Name;
        public string Id;
        public HashedString IdHash;
        public bool Disabled;
        public ResourceGuid Guid;
        public float Rating;
        public bool ShouldSave;
        public bool PositiveTrait;
        public bool ValidStarterTrait;
        public List<OutChoreGroup> disabledChoreGroups = new List<OutChoreGroup>();
        public bool isTaskBeingRefused;
        public List<string> ignoredEffects = new List<string>();
        public string description;
        public List<AttributeModifier> SelfModifiers = new List<AttributeModifier>();

        public OutTrait(Trait obj)
        {
            this.Name = obj.Name;
            this.Id = obj.Id;
            this.IdHash = obj.IdHash;
            this.Disabled = obj.Disabled;
            this.Guid = obj.Guid;
            this.Rating = obj.Rating;
            this.ShouldSave = obj.ShouldSave;
            this.PositiveTrait = obj.PositiveTrait;
            this.ValidStarterTrait = obj.ValidStarterTrait;
            if(obj.disabledChoreGroups != null)
            {
                foreach (var choreGroup in obj.disabledChoreGroups)
                {
                     this.disabledChoreGroups.Add(new OutChoreGroup(choreGroup));
                }
            }
            this.isTaskBeingRefused = obj.isTaskBeingRefused;
            foreach(string ignoredEffect in obj.ignoredEffects)
            {
                this.ignoredEffects.Add(ignoredEffect);
            }
            this.description = obj.description;
            this.SelfModifiers = obj.SelfModifiers;
        }
    }
}
