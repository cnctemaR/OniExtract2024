
namespace OniExtract2024
{
    public class OutConduitDispenser
    {
        public bool alwaysDispense;
        public bool blocked;
        public ConduitType conduitType;
        public SimHashes[] elementFilter;
        public bool empty = true;
        public bool invertElementFilter;
        public bool isOn = true;
        public OutStorage storage;
        public bool useSecondaryOutput;

        public OutConduitDispenser(ConduitDispenser obj)
        {
            this.alwaysDispense = obj.alwaysDispense;
            this.blocked = obj.blocked;
            this.conduitType = obj.conduitType;
            this.elementFilter = obj.elementFilter;
            this.empty = obj.empty;
            this.invertElementFilter = obj.invertElementFilter;
            this.isOn = obj.isOn;
            if(obj.storage != null)
            {
                this.storage = new OutStorage(obj.storage);
            }
            else
            {
                this.storage = null;
            }
            this.useSecondaryOutput = obj.useSecondaryOutput;
        }
    }
}