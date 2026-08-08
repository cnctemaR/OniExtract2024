using System.Collections.Generic;
using UnityEngine;

namespace OniExtract2024
{
    public class BElement
    {
        public string name;
        public string id;
        public int tag;
        public List<string> oreTags = new List<string>();
        public string state;
        public int buildMenuSort;
        public string materialCategory;
        public float molarMass;
        public float specificHeatCapacity;
        public float thermalConductivity;
        public float hardness;
        public float lowTemp;
        public float highTemp;
        public string lowTempTransitionTarget;
        public string highTempTransitionTarget;
        public float sublimateRate;
        public int color;
        public int conduitColor;
        public int uiColor;

        public BElement(Element e)
        {
            name = e.name;
            id = e.id.ToString();
            tag = e.tag.GetHash();
            state = e.state.ToString();
            buildMenuSort = e.buildMenuSort;
            materialCategory = e.materialCategory.Name;
            molarMass = e.molarMass;
            specificHeatCapacity = e.specificHeatCapacity;
            thermalConductivity = e.thermalConductivity;
            hardness = e.hardness;
            lowTemp = e.lowTemp;
            highTemp = e.highTemp;
            lowTempTransitionTarget = e.lowTempTransitionTarget.ToString();
            highTempTransitionTarget = e.highTempTransitionTarget.ToString();
            sublimateRate = e.sublimateRate;

            foreach (var t in e.oreTags)
                oreTags.Add(t.Name);

            Substance substance = e.substance;
            if (substance != null)
            {
                Color32 c = substance.colour;
                color = (c.r << 16) | (c.g << 8) | c.b;
                Color32 cc = substance.conduitColour;
                conduitColor = (cc.r << 16) | (cc.g << 8) | cc.b;
                Color32 uc = substance.uiColour;
                uiColor = (uc.r << 16) | (uc.g << 8) | uc.b;
            }
        }
    }
}
