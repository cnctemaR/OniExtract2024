using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ClusterGridEntity;

namespace OniExtract2024
{
    public class OutClusterMapMeteorShowerVisualizer
    {
        public List<AnimConfig> AnimConfigs;
        public string AnimName;
        public bool IsVisible;
        public ClusterRevealLevel IsVisibleInFOW;
        public EntityLayer Layer;
        public string Name;
        public string QuestionMarkAnimName;
        public bool revealed;
        public string p_name;
        public string clusterAnimName;

        public OutClusterMapMeteorShowerVisualizer(ClusterMapMeteorShowerVisualizer obj)
        {
            this.AnimConfigs = obj.AnimConfigs;
            this.AnimName = obj.AnimName;
            this.IsVisible = obj.IsVisible;
            this.IsVisibleInFOW = obj.IsVisibleInFOW;
            this.Layer = obj.Layer;
            this.Name = obj.Name;
            this.QuestionMarkAnimName = obj.QuestionMarkAnimName;
            this.revealed = obj.revealed;
            this.p_name = obj.p_name;
            this.clusterAnimName = obj.clusterAnimName;
        }
    }
}
