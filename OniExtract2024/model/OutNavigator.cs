using System.Collections.Generic;
using static Navigator;

namespace OniExtract2024
{
    public class OutNavigator
    {
        public NavType CurrentNavType;
        public bool DebugDrawPath;
        public float defaultSpeed = 1f;
        public Dictionary<NavType, int> distanceTravelledByNavType;
        public bool executePathProbeTaskAsync;
        public PathFinder.PotentialPath.Flags flags;
        public int maxProbingRadius;
        public string NavGridName;
        public PathFinder.Path path;
        public PathProber PathProber;
        public PathProbeTask pathProbeTask;
        public Grid.SceneLayer sceneLayer = Grid.SceneLayer.Move;
        public TransitionDriver transitionDriver;
        public bool updateProber;

        public OutNavigator(Navigator obj)
        {
            this.CurrentNavType = obj.CurrentNavType;
            this.DebugDrawPath  = obj.DebugDrawPath;
            this.defaultSpeed = obj.defaultSpeed;
            this.distanceTravelledByNavType = obj.distanceTravelledByNavType;
            this.executePathProbeTaskAsync = obj.executePathProbeTaskAsync;
            this.flags = obj.flags;
            this.maxProbingRadius = obj.maxProbingRadius;
            this.NavGridName = obj.NavGridName;
            this.path = obj.path;
            this.pathProbeTask = obj.pathProbeTask;
            this.sceneLayer = obj.sceneLayer;
            this.transitionDriver = obj.transitionDriver;
            this.updateProber = obj.updateProber;
        }
    }
}
