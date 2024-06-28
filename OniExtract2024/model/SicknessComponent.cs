using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Klei.AI;

namespace OniExtract2024
{
    public class OutSicknessComponent
    {
        public string sickness;
        public OutAttributeModifierSickness modifiers;
        public CommonSickEffectSickness commonSickEffect;
        public PeriodicEmoteSickness periodicEmote;
        public AnimatedSickness animated;

        public OutSicknessComponent(string sickness)
        {
            this.sickness = sickness;
        }
    }
}
