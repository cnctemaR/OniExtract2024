
namespace OniExtract2024
{
    public class OutManualDeliveryKG
    {
        public OutStorage storage;        
        public Tag requestedItemTag;
        public Tag[] forbiddenTags;        
        public float capacity = 100f;        
        public float refillMass = 10f;        
        public float MinimumMass = 10f;
        public bool RoundFetchAmountToInt;
        public bool FillToCapacity = false;
        public Operational.State operationalRequirement;        
        public bool allowPause;        
        public bool paused;        
        public HashedString choreTypeIDHash;
        public bool ShowStatusItem = true;
        public FetchList2 fetchList;

        public OutManualDeliveryKG(ManualDeliveryKG obj)
        {
            if(obj.DebugStorage != null)
            {
                this.storage = new OutStorage(obj.DebugStorage);
            }
            else
            {
                this.storage = null;
            }
            this.requestedItemTag = obj.requestedItemTag;
            this.forbiddenTags = obj.ForbiddenTags;
            this.capacity = obj.Capacity;
            this.refillMass = obj.refillMass;
            this.MinimumMass = obj.MinimumMass;
            this.RoundFetchAmountToInt = obj.RoundFetchAmountToInt;
            this.FillToCapacity = obj.FillToCapacity;
            this.operationalRequirement = obj.operationalRequirement;
            this.allowPause = obj.allowPause;
            this.paused = obj.IsPaused;
            this.choreTypeIDHash = obj.choreTypeIDHash;
            this.ShowStatusItem = obj.ShowStatusItem;
            this.fetchList = obj.DebugFetchList;
        }
    }
}
