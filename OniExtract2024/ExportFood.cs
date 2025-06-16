using OniExtract2024;
using System.Collections.Generic;
using static EdiblesManager;

public class ExportFood : BaseExport
{
    public override string ExportFileName { get; set; } = "food";
    public List<FoodInfo> foodInfoList = new List<FoodInfo>();
    public Dictionary<string, string[]> requiredDlcIdsMap = new Dictionary<string, string[]>();
    public Dictionary<string, string[]> forbiddenDlcIdsMap = new Dictionary<string, string[]>();
    public Dictionary<int, string> qualityEffects = new Dictionary<int, string>();

    public ExportFood()
	{
    }

    public void ExportAllFood()
    {
        foreach (var foodInfo in EdiblesManager.GetAllLoadedFoodTypes())
        {
            this.foodInfoList.Add(foodInfo);
            this.requiredDlcIdsMap.Add(foodInfo.Id, foodInfo.GetRequiredDlcIds());
            this.forbiddenDlcIdsMap.Add(foodInfo.Id, foodInfo.GetForbiddenDlcIds());
            this.qualityEffects[foodInfo.Quality] = Edible.GetEffectForFoodQuality(foodInfo.Quality);
        }
    }
}
