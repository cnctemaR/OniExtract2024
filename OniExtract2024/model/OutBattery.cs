using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OniExtract2024
{
    public class OutBattery
    {
        [SerializeField]
        public float capacity;
        public float joulesLostPerSecond = 0f;
        [SerializeField]
        public int powerSortOrder;
        [SerializeField]
        public Tag[] connectedTags;


        public OutBattery(Battery obj)
        {
            this.capacity = obj.capacity;
            this.joulesLostPerSecond = obj.joulesLostPerSecond;
            this.powerSortOrder = obj.powerSortOrder;
            this.connectedTags = obj.connectedTags;
        }
    }
}
