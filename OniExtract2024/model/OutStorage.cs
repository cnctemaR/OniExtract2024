using UnityEngine;
using System.Collections.Generic;

namespace OniExtract2024
{
    public class OutStorage
    {
        public bool allowItemRemoval;
        public bool ignoreSourcePriority;
        public bool onlyTransferFromLowerPriority;
        public float capacityKg = 0f;
        public bool showDescriptor;
        public bool doDiseaseTransfer = true;
        public List<Tag> storageFilters;
        public bool useGunForDelivery = true;
        public bool sendOnStoreOnSpawn;
        public bool showInUI = true;
        public bool allowClearable;
        public bool showCapacityStatusItem;
        public bool showCapacityAsMainStatus;
        public bool showUnreachableStatus;
        public bool showSideScreenTitleBar;
        public bool useWideOffsets;

        public Storage.FetchCategory fetchCategory;
        public int storageNetworkID = -1;
        public float storageFullMargin;

        public List<GameObject> items = new List<GameObject>();

        public bool dropOnLoad;
        protected float maxKGPerItem = float.MaxValue;
        private bool endOfLife;
        public bool allowSettingOnlyFetchMarkedItems = true;
        private bool onlyFetchMarkedItems;
        public float storageWorkTime = 1.5f;

        public OutStorage(Storage obj)
        {
            this.onlyTransferFromLowerPriority = obj.onlyTransferFromLowerPriority;
            this.capacityKg = obj.capacityKg;
            this.showDescriptor = obj.showDescriptor;
            this.doDiseaseTransfer = obj.doDiseaseTransfer;
            this.storageFilters = obj.storageFilters;
            this.useGunForDelivery = obj.useGunForDelivery;
            this.sendOnStoreOnSpawn = obj.sendOnStoreOnSpawn;
            this.showInUI = obj.showInUI;
            this.allowClearable = obj.allowClearable;
            this.showCapacityStatusItem = obj.showCapacityStatusItem;
            this.showCapacityAsMainStatus = obj.showCapacityAsMainStatus;
            this.showUnreachableStatus = obj.showUnreachableStatus;
            this.showSideScreenTitleBar = obj.showSideScreenTitleBar;
            this.useWideOffsets = obj.useWideOffsets;
            this.fetchCategory = obj.fetchCategory;
            this.storageNetworkID = obj.storageNetworkID;
            this.storageFullMargin = obj.storageFullMargin;
            this.items = obj.items;
            this.dropOnLoad = obj.dropOnLoad;
            this.endOfLife = obj.IsEndOfLife();
            this.allowSettingOnlyFetchMarkedItems = obj.allowSettingOnlyFetchMarkedItems;
            this.onlyFetchMarkedItems = obj.GetOnlyFetchMarkedItems();
            this.storageWorkTime = obj.storageWorkTime;
        }
    }
}
