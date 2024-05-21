using Klei.AI;
using System.Collections.Generic;

namespace OniExtract2024
{
    public class OutTraitGroup
    {
        public string Name;
        public string Id;
        public HashedString IdHash;
        public bool Disabled;
        public ResourceGuid Guid;
        public bool IsSpawnTrait;
        public List<OutTrait> modifiers = new List<OutTrait>();
        public int Count => modifiers.Count;


        public OutTraitGroup(TraitGroup obj)
        {
            this.Name = obj.Name;
            this.Id = obj.Id;
            this.IdHash = obj.IdHash;
            this.Disabled = obj.Disabled;
            this.Guid = obj.Guid;
            this.IsSpawnTrait = obj.IsSpawnTrait;
            foreach(Trait trait in obj.modifiers)
            {
                this.modifiers.Add(new OutTrait(trait));
            }
        }
    }
}
