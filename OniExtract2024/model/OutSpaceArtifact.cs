using System;
using System.Collections.Generic;
using System.Linq;
using static STRINGS.UI.SPACEARTIFACTS;
using static TUNING.DECOR;

namespace OniExtract2024
{
    public class OutSpaceArtifact
    {
        public string ui_anim;
        public ArtifactTier artifactTier;
        public ArtifactType artifactType;
        public string uniqueAnimNameFragment;

        public OutSpaceArtifact(SpaceArtifact obj)
        {
            this.ui_anim = obj.GetUIAnim();
            this.artifactTier = obj.artifactTier;
            this.artifactType = obj.artifactType;
            this.uniqueAnimNameFragment = obj.uniqueAnimNameFragment;
        }
    }
}
