using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OniExtract2024
{
    public class OutRocketEngine
    {
        public float exhaustEmitRate = 50f;
        public float exhaustTemperature = 1500f;
        public SpawnFXHashes explosionEffectHash;
        public SimHashes exhaustElement = SimHashes.CarbonDioxide;
        public Tag fuelTag;
        public float efficiency = 1f;
        public bool requireOxidizer = true;

        public OutRocketEngine(RocketEngine obj) {
            this.exhaustEmitRate = obj.exhaustEmitRate;
            this.exhaustTemperature = obj.exhaustTemperature;
            this.explosionEffectHash = obj.explosionEffectHash;
            this.exhaustElement = obj.exhaustElement;
            this.fuelTag = obj.fuelTag;
            this.efficiency = obj.efficiency;
            this.requireOxidizer = obj.requireOxidizer;
        }
    }
}
