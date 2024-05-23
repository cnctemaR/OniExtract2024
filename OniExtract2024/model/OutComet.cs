using UnityEngine;

namespace OniExtract2024
{
    public class OutComet
    {
        public int addDiseaseCount;
        public int addTiles;
        public int addTilesMaxHeight;
        public int addTilesMinHeight;
        public float bunkerDamageMultiplier;
        public bool canHitDuplicants;
        public string[] craterPrefabs;
        public bool destroyOnExplode = true;
        public byte diseaseIdx = byte.MaxValue;
        public bool affectedByDifficulty = true;
        public Vector2 elementReplaceTileTemperatureRange = new Vector2(800f, 1000f);
        public int entityDamage = 1;
        public SimHashes EXHAUST_ELEMENT = SimHashes.CarbonDioxide;
        public float EXHAUST_RATE = 50f;
        public SpawnFXHashes explosionEffectHash;
        //public Vector2I explosionOreCount = new Vector2I(0, 0);
        public Vector2 explosionSpeedRange = new Vector2(8f, 14f);
        public Vector2 explosionTemperatureRange = new Vector2(500f, 700f);
        public string flyingSound;
        public int flyingSoundID;
        //public Ref<KPrefabID> ignoreObstacleForDamage = new Ref<KPrefabID>();
        public string impactSound;
        public string[] lootOnDestroyedByMissile;
        public Vector2 massRange;
        public System.Action OnImpact;
        public Vector2 spawnAngle = new Vector2(-100f, -80f);
        public Vector2 spawnVelocity = new Vector2(12f, 15f);
        public bool spawnWithOffset;
        public int splashRadius = 1;
        public bool Targeted;
        public Vector2 temperatureRange;
        public float totalTileDamage = 0.2f;
        public Tag typeID;
        public Vector2 velocity;
        public float windowDamageMultiplier = 5f;

        public OutComet(Comet obj)
        {
            this.addDiseaseCount = obj.addDiseaseCount;
            this.addTiles = obj.addTiles;
            this.addTilesMaxHeight = obj.addTilesMaxHeight;
            this.addTilesMinHeight = obj.addTilesMinHeight;
            this.bunkerDamageMultiplier = obj.bunkerDamageMultiplier;
            this.canHitDuplicants = obj.canHitDuplicants;
            this.craterPrefabs = obj.craterPrefabs;
            this.destroyOnExplode = obj.destroyOnExplode;
            this.diseaseIdx = obj.diseaseIdx;
            this.affectedByDifficulty = obj.affectedByDifficulty;
            this.elementReplaceTileTemperatureRange = obj.elementReplaceTileTemperatureRange;
            this.entityDamage = obj.entityDamage;
            this.EXHAUST_ELEMENT = obj.EXHAUST_ELEMENT;
            this.EXHAUST_RATE = obj.EXHAUST_RATE;
            this.explosionEffectHash = obj.explosionEffectHash;
            //this.explosionOreCount = obj.explosionOreCount;
            this.explosionSpeedRange = obj.explosionSpeedRange;
            this.explosionTemperatureRange = obj.explosionTemperatureRange;
            this.flyingSound = obj.flyingSound;
            this.flyingSoundID = obj.flyingSoundID;
            //this.ignoreObstacleForDamage = obj.ignoreObstacleForDamage;
            this.impactSound = obj.impactSound;
            this.lootOnDestroyedByMissile = obj.lootOnDestroyedByMissile;
            this.massRange = obj.massRange;
            this.OnImpact = obj.OnImpact;
            this.spawnAngle = obj.spawnAngle;
            this.spawnVelocity = obj.spawnVelocity;
            this.spawnWithOffset = obj.spawnWithOffset;
            this.splashRadius = obj.splashRadius;
            this.Targeted = obj.Targeted;
            this.temperatureRange = obj.temperatureRange;
            this.totalTileDamage = obj.totalTileDamage;
            this.typeID = obj.typeID;
            this.velocity = obj.Velocity;
            this.windowDamageMultiplier = obj.windowDamageMultiplier;
        }
    }
}
