using KSerialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CargoBay;

namespace OniExtract2024
{
    public class OutCargoBay
    {
        public OutStorage storage;
        [Serialize]
        public float reservedResources;
        public CargoType storageType;

        public OutCargoBay(CargoBay obj){
            this.storage = new OutStorage(obj.storage);
            this.reservedResources = obj.reservedResources;
            this.storageType = obj.storageType;
        }
    }
}
