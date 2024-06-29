using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.UI.Image;

namespace OniExtract2024
{
    public class OutLight2D
    {
        public float Angle;
        public Vector2 Direction;
        public bool drawOverlay;
        public BColor overlayColour;
        public MaterialPropertyBlock materialPropertyBlock;
        public LightShape shape;
        public LightGridManager.LightGridEmitter emitter;
        public BColor Color;
        public int Lux;
        public float Range;
        public float FalloffRate;
        public float IntensityAnimation;
        public Vector2 Offset;

        public OutLight2D(Light2D obj) {
            if (obj == null)
            {
                return;
            }
            this.Angle = obj.Angle;
            this.Direction = obj.Direction;
            this.drawOverlay = obj.drawOverlay;
            this.overlayColour = new BColor(obj.overlayColour);
            this.materialPropertyBlock = obj.materialPropertyBlock;
            this.shape = obj.shape;
            this.emitter = obj.emitter;
            this.Color = new BColor(obj.Color);
            this.Lux = obj.Lux;
            this.Range = obj.Range;
            this.FalloffRate = obj.FalloffRate;
            this.IntensityAnimation = obj.IntensityAnimation;
            this.Offset = obj.Offset;
        }
    }
}
