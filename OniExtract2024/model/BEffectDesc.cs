using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OniExtract2024
{
    public class BEffectDesc
    {
        public string id;
        public List<Descriptor> descs;
        public string effectId;
        public bool increaseIndent = false;

        public BEffectDesc(string id, List<Descriptor> descs, string effectId, bool increaseIndent)
        {
            this.id = id;
            this.descs = descs;
            this.effectId = effectId;
            this.increaseIndent = increaseIndent;
        }
    }
}
