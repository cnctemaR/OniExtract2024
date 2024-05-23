using UnityEngine;

namespace OniExtract2024
{
    public class OutPlantablePlot
    {
        public Grid.SceneLayer plantLayer = Grid.SceneLayer.BuildingBack;
        public Vector3 occupyingObjectVisualOffset = Vector3.zero;
        public bool has_liquid_pipe_input;
        public bool ValidPlant;
        public bool AcceptsIrrigation;
        public bool AcceptsFertilizer;

        public OutPlantablePlot(PlantablePlot obj)
        {
            this.plantLayer = obj.plantLayer;
            this.occupyingObjectVisualOffset = obj.occupyingObjectVisualOffset;
            this.has_liquid_pipe_input = obj.has_liquid_pipe_input;
            this.ValidPlant = obj.ValidPlant;
            this.AcceptsIrrigation = obj.AcceptsIrrigation;
            this.AcceptsFertilizer = obj.AcceptsFertilizer;
        }
    }
}