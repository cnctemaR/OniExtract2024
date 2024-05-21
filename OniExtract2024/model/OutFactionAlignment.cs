
namespace OniExtract2024
{
    public class OutFactionAlignment
    {
        public FactionManager.FactionID Alignment;
        public bool canBePlayerTargeted = true;
        public bool updatePrioritizable = true;
        public Health health;
        public AttackableBase attackable;


        public OutFactionAlignment(FactionAlignment obj)
        {
            this.Alignment = obj.Alignment;
            this.canBePlayerTargeted = obj.canBePlayerTargeted;
            this.updatePrioritizable = obj.updatePrioritizable;
            this.health = obj.health;
            this.attackable = obj.attackable;
        }
    }
}
