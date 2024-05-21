using OniExtract2024;
using System.Collections.Generic;

public class ExportRecipe : BaseExport
{
    public override string ExportFileName { get; set; } = "recipe";
    public List<ComplexRecipe> recipes = new List<ComplexRecipe>();

    public ExportRecipe()
	{
    }

    public void ExportComplexRecipes()
    {
        foreach (var mRecipe in ComplexRecipeManager.Get().recipes)
        {
            this.recipes.Add(mRecipe);
        }
    }
}
