using Klei.AI;
using static Database.Spice;

namespace OniExtract2024
{
    public class OutSpice
    {
        public string Name;
        public string Id;
        public HashedString IdHash;
        public bool Disabled;
        public ResourceGuid Guid;
        public Ingredient[] Ingredients;
        public float TotalKG;
        public AttributeModifier StatBonus;
        public AttributeModifier FoodModifier;
        public AttributeModifier CalorieModifier;
        public BColor PrimaryColor;
        public BColor SecondaryColor;
        public string Image;
        public string[] DlcIds;


        public OutSpice(Database.Spice obj)
        {
            this.Name = obj.Name;
            this.Id = obj.Id;
            this.IdHash = obj.IdHash;
            this.Disabled = obj.Disabled;
            this.Guid = obj.Guid;
            this.Ingredients = obj.Ingredients;
            this.TotalKG = obj.TotalKG;
            this.StatBonus = obj.StatBonus;
            this.FoodModifier = obj.FoodModifier;
            this.CalorieModifier = obj.CalorieModifier;
            this.PrimaryColor = new BColor(obj.PrimaryColor);
            this.SecondaryColor = new BColor(obj.SecondaryColor);
            this.Image = obj.Image;
            this.DlcIds = obj.DlcIds;
        }
    }
}
