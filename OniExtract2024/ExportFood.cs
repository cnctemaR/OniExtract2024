using OniExtract2024;
using System.Collections.Generic;
using static EdiblesManager;

public class ExportFood : BaseExport
{
    public override string ExportFileName { get; set; } = "food";
    public List<FoodInfo> foodInfoList = new List<FoodInfo>();
    public Dictionary<int, string> qualityEffects = new Dictionary<int, string>();

    public ExportFood()
	{
    }

    public void ExportAllFood()
    {
        foreach (var foodInfo in EdiblesManager.GetAllLoadedFoodTypes())
        {
            this.foodInfoList.Add(foodInfo);
            this.qualityEffects[foodInfo.Quality] = Edible.GetEffectForFoodQuality(foodInfo.Quality);
        }
    }
}
