using OniExtract2024;
using System.Collections.Generic;
using static EdiblesManager;

public class ExportFood : BaseExport
{
    public override string ExportFileName { get; set; } = "food";
    public List<FoodInfo> foodInfoList;

    public ExportFood()
	{
        foodInfoList = new List<FoodInfo>();
    }

    public void ExportAllFood()
    {
        foreach (var foodInfo in EdiblesManager.GetAllFoodTypes())
        {
            this.foodInfoList.Add(foodInfo);
        }
    }
}
