using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OniExtract2024
{
    public class OutRocketEngineCluster
    {
        public float exhaustEmitRate = 50f;
        public float exhaustTemperature = 1500f;
        public SpawnFXHashes explosionEffectHash;
        public SimHashes exhaustElement = SimHashes.CarbonDioxide;
        public Tag fuelTag;
        public float efficiency = 1f;
        public bool requireOxidizer = true;
        public int maxModules = 32;
        public int maxHeight;
        public bool mainEngine = true;
        public byte exhaustDiseaseIdx = byte.MaxValue;
        public int exhaustDiseaseCount;
        public bool emitRadiation;
        public OutLight2D flameLight;

        public OutRocketEngineCluster(RocketEngineCluster obj) {
            this.exhaustEmitRate = obj.exhaustEmitRate;
            this.exhaustTemperature = obj.exhaustTemperature;
            this.explosionEffectHash = obj.explosionEffectHash;
            this.exhaustElement = obj.exhaustElement;
            this.fuelTag = obj.fuelTag;
            this.efficiency = obj.efficiency;
            this.requireOxidizer = obj.requireOxidizer;
            this.maxModules = obj.maxModules;
            this.maxHeight = obj.maxHeight;
            this.mainEngine = obj.mainEngine;
            this.exhaustDiseaseIdx = obj.exhaustDiseaseIdx;
            this.exhaustDiseaseCount = obj.exhaustDiseaseCount;
            this.emitRadiation = obj.emitRadiation;
            this.flameLight = new OutLight2D(obj.flameLight);
        }
    }
}
