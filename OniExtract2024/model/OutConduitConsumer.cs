using static ConduitConsumer;

namespace OniExtract2024
{
    public class OutConduitConsumer
    {
        public bool alwaysConsume;
        public float capacityKG = float.PositiveInfinity;
        public Tag capacityTag = GameTags.Any;
        public ConduitType conduitType;
        public bool consumedLastTick = true;
        public float consumptionRate = float.PositiveInfinity;
        public bool forceAlwaysSatisfied;
        public bool ignoreMinMassCheck;
        public bool isConsuming = true;
        public bool isOn = true;
        public bool keepZeroMassObject = true;
        public SimHashes lastConsumedElement = SimHashes.Vacuum;
        public Operational.State OperatingRequirement;
        public OutOperational operational;
        public OutStorage storage;
        public ISecondaryInput targetSecondaryInput;
        public bool useSecondaryInput;
        public WrongElementResult wrongElementResult;

        public OutConduitConsumer(ConduitConsumer obj)
        {
            this.alwaysConsume = obj.alwaysConsume;
            this.capacityKG = obj.capacityKG;
            this.capacityTag = obj.capacityTag;
            this.conduitType = obj.conduitType;
            this.consumedLastTick = obj.consumedLastTick;
            this.consumptionRate = obj.consumptionRate;
            this.forceAlwaysSatisfied = obj.forceAlwaysSatisfied;
            this.ignoreMinMassCheck = obj.ignoreMinMassCheck;
            this.isConsuming = obj.isConsuming;
            this.isOn = obj.isOn;
            this.keepZeroMassObject = obj.keepZeroMassObject;
            this.lastConsumedElement = obj.lastConsumedElement;
            this.OperatingRequirement = obj.OperatingRequirement;
            this.operational = new OutOperational(obj.operational);
            if(obj.storage != null)
            {
                this.storage = new OutStorage(obj.storage);
            }
            else
            {
                this.storage = null;
            }            
            this.targetSecondaryInput = obj.targetSecondaryInput;
            this.useSecondaryInput = obj.useSecondaryInput;
            this.wrongElementResult = obj.wrongElementResult;
        }
    }
}