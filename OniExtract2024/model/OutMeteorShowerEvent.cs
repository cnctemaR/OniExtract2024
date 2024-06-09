using Klei.AI;
using System.Collections.Generic;

namespace OniExtract2024
{
    public class OutMeteorShowerEvent
    {
        public string id;
        public List<MeteorShowerEvent.BombardmentInfo> bombardmentInfo;
        public MathUtil.MinMax secondsBombardmentOff;
        public MathUtil.MinMax secondsBombardmentOn;
        public float secondsPerMeteor = 0.33f;
        public float duration = -1;
        public string clusterMapMeteorShowerID = null;
        public bool affectedByDifficulty = true;

        public HashedString animFileName;
        public List<Tag> tags = new List<Tag>();
        public string Name = null;
        public HashedString IdHash = null;
        public int numTimesAllowed = -1;
        public bool allowMultipleEventInstances = true;
        protected int basePriority = 0;

        public OutMeteorShowerEvent(string id, float duration, float secondsPerMeteor, MathUtil.MinMax secondsBombardmentOff = default(MathUtil.MinMax), MathUtil.MinMax secondsBombardmentOn = default(MathUtil.MinMax), string clusterMapMeteorShowerID = null, bool affectedByDifficulty = true)
        {
            this.id = id;
            this.allowMultipleEventInstances = true;
            this.clusterMapMeteorShowerID = clusterMapMeteorShowerID;
            this.duration = duration;
            this.secondsPerMeteor = secondsPerMeteor;
            this.secondsBombardmentOff = secondsBombardmentOff;
            this.secondsBombardmentOn = secondsBombardmentOn;
            this.affectedByDifficulty = affectedByDifficulty;
            this.bombardmentInfo = new List<MeteorShowerEvent.BombardmentInfo>();
            this.tags.Add(GameTags.SpaceDanger);
        }

        public void SetMeteorShowerEventData(MeteorShowerEvent obj)
        {
            this.bombardmentInfo = obj.GetMeteorsInfo();
            this.animFileName = obj.animFileName;
            this.tags = obj.tags;
            this.Name = obj.Name;
            this.IdHash = obj.IdHash;
            this.allowMultipleEventInstances = obj.allowMultipleEventInstances;
            this.numTimesAllowed = obj.numTimesAllowed;
        }
    }
}
