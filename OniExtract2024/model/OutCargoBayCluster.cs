using KSerialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OniExtract2024
{
    public class OutCargoBayCluster
    {
        [SerializeField]
        public OutStorage storage;
        [SerializeField]
        public CargoBay.CargoType storageType;
        [Serialize]
        private float userMaxCapacity;
        public float MinCapacity;
        public float MaxCapacity;
        public float AmountStored;
        public bool WholeValues;
        public LocString CapacityUnits;
        public float RemainingCapacity;

        public OutCargoBayCluster(CargoBayCluster obj)
        {
            this.storage = new OutStorage(obj.storage);
            this.storageType = obj.storageType;
            this.userMaxCapacity = obj.UserMaxCapacity;
            this.MinCapacity = obj.MinCapacity;
            this.MaxCapacity = obj.MaxCapacity;
            this.AmountStored = obj.AmountStored;
            this.WholeValues = obj.WholeValues;
            this.CapacityUnits = obj.CapacityUnits;
            this.RemainingCapacity = obj.RemainingCapacity;
        }
    }
}
