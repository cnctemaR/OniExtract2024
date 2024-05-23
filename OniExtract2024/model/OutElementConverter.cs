﻿using System;
using System.Collections.Generic;

namespace OniExtract2024
{
    public class OutElementConverter
    {
        public HashSet<OutConsumedElement> consumedElements;
        public HashSet<OutOutputElement> outputElements;
        public Action<float> onConvertMass;
        public Operational.State OperationalRequirement = Operational.State.Active;
        public bool showDescriptors = true;
        public bool ShowInUI = true;
        public float OutputMultiplier;

        public OutElementConverter(ElementConverter obj)
        {
            this.consumedElements = new HashSet<OutConsumedElement>();
            if (obj.consumedElements != null && obj.consumedElements.Length > 0)
            {
                foreach (var kv in obj.consumedElements)
                {
                    this.consumedElements.Add(new OutConsumedElement(kv));
                }
            }
            this.outputElements = new HashSet<OutOutputElement>();
            if (obj.outputElements != null && obj.outputElements.Length > 0)
            {
                foreach (var kv in obj.outputElements)
                {
                    this.outputElements.Add(new OutOutputElement(kv));
                }
            }


            this.onConvertMass = obj.onConvertMass;
            this.OperationalRequirement = obj.OperationalRequirement;
            this.showDescriptors = obj.showDescriptors;
            this.ShowInUI = obj.ShowInUI;
            this.OutputMultiplier = obj.OutputMultiplier;
        }
    }
}