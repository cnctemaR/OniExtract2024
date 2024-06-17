
namespace OniExtract2024
{
    public class OutPickupable
    {
        public bool IsEntombed;
        public float MinTakeAmount;
        public bool prevent_absorb_until_store;
        public OutPrimaryElement PrimaryElement;
        public float ReservedAmount;
        public int sortOrder;
        public OutStorage storage;
        //public float TotalAmount;
        //public float UnreservedAmount;
        public bool wasAbsorbed;
        public bool absorbable;
        public bool isClearable;
        public bool deleteOffGrid = true;
        //public KPrefabID KPrefabID;
        public Workable targetWorkable;
        public bool trackOnPickup = true;
        public bool useGunforPickup = true;

        public OutPickupable(Pickupable obj)
        {
            this.IsEntombed = obj.IsEntombed;
            this.MinTakeAmount = obj.MinTakeAmount;
            this.prevent_absorb_until_store = obj.prevent_absorb_until_stored;
            if(obj.PrimaryElement != null )
            {
                this.PrimaryElement = new OutPrimaryElement(obj.PrimaryElement);
            }
            else
            {
                this.PrimaryElement=null;
            }
            this.ReservedAmount = obj.ReservedAmount;
            this.sortOrder = obj.sortOrder;
            if(obj.storage != null)
            {
                this.storage = new OutStorage(obj.storage);
            }
            else
            {
                this.storage = null;
            }
            //this.TotalAmount = tv.TotalAmount;
            //this.UnreservedAmount = tv.UnreservedAmount;
            this.wasAbsorbed = obj.wasAbsorbed;
            this.absorbable = obj.absorbable;
            this.isClearable = obj.Clearable != null;
            this.deleteOffGrid = obj.deleteOffGrid;
            //this.KPrefabID = tv.KPrefabID;
            this.targetWorkable = obj.targetWorkable;
            this.trackOnPickup = obj.trackOnPickup;
            this.useGunforPickup = obj.useGunforPickup;
        }
    }
}
