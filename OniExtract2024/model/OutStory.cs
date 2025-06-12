using Database;
using ProcGen;

namespace OniExtract2024
{
    public class OutStory
    {
        public string Name;
        public string Id;
        public HashedString IdHash;
        public bool Disabled;
        public ResourceGuid Guid;
        public int kleiUseOnlyCoordinateOrder;
        public bool autoStart;
        public string keepsakePrefabId;
        public readonly string worldgenStoryTraitKey;
        public string sandboxStampTemplateId;
        private WorldTrait StoryTrait;
        public int HashId;

        public OutStory(Story obj)
        {
            this.Name = obj.Name;
            this.Id = obj.Id;
            this.IdHash = obj.IdHash;
            this.Disabled = obj.Disabled;
            this.Guid = obj.Guid;
            this.kleiUseOnlyCoordinateOrder = obj.kleiUseOnlyCoordinateOrder;
            this.autoStart = obj.autoStart;
            this.keepsakePrefabId = obj.keepsakePrefabId;
            this.worldgenStoryTraitKey = obj.worldgenStoryTraitKey;
            this.sandboxStampTemplateId = obj.sandboxStampTemplateId;
            this.StoryTrait = obj.StoryTrait;
            this.HashId = obj.HashId;
        }
    }
}
