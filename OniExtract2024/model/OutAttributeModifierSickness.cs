using Klei.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OniExtract2024
{
    public class OutAttributeModifierSickness
    {
        public AttributeModifier[] Modifers;
        public List<Descriptor> symptoms;

        public OutAttributeModifierSickness(AttributeModifierSickness obj)
        {
            this.Modifers = obj.Modifers;
            this.symptoms = obj.GetSymptoms();
        }
    }
}
