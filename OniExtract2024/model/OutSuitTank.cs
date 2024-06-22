using KSerialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OniExtract2024
{
    public class OutSuitTank
    {
        public string element;
        public float amount;
        public Tag elementTag;
        public float capacity;
        public bool underwaterSupport;
        public bool ShouldEmitCO2;
        public bool ShouldStoreCO2;

        public OutSuitTank(SuitTank obj)
        {
            this.element = obj.element;
            this.amount = obj.amount;
            this.elementTag = obj.elementTag;
            this.capacity = obj.capacity;
            this.underwaterSupport = obj.underwaterSupport;
            this.ShouldEmitCO2 = obj.ShouldEmitCO2();
            this.ShouldStoreCO2 = obj.ShouldStoreCO2();
        }
    }
}
