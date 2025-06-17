using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OniExtract2024
{
    public class BKprefabID
    {
        public string name;
        public string nameString;
        public Tag SaveLoadTag;
        public Tag PrefabTag;
        public int defaultLayer;
        public HashSet<Tag> tags;
        public string[] requiredDlcIds;
        public string[] forbiddenDlcIds;
        public List<Descriptor> AdditionalRequirements;
        public List<Descriptor> AdditionalEffects;

        public BKprefabID(KPrefabID kPrefabID)
        {
            this.name = kPrefabID.PrefabID().Name;
            this.nameString = kPrefabID.GetProperName();
            this.SaveLoadTag = kPrefabID.SaveLoadTag;
            this.PrefabTag = kPrefabID.PrefabTag;
            this.defaultLayer = kPrefabID.defaultLayer;
            this.tags = kPrefabID.Tags;
            this.requiredDlcIds = kPrefabID.requiredDlcIds == null ? null : (kPrefabID.requiredDlcIds.Length == 0 ? null : kPrefabID.requiredDlcIds);
            this.forbiddenDlcIds = kPrefabID.forbiddenDlcIds == null ? null : (kPrefabID.forbiddenDlcIds.Length == 0 ? null : kPrefabID.forbiddenDlcIds);
            this.AdditionalRequirements = kPrefabID.AdditionalRequirements;
            this.AdditionalEffects = kPrefabID.AdditionalEffects;
        }
    }
}
