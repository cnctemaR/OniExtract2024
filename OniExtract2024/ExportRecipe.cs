using OniExtract2024;
using System.Collections.Generic;

public class ExportRecipe : BaseExport
{
    public override string ExportFileName { get; set; } = "recipe";
    public List<ComplexRecipe> recipes = null;

    public ExportRecipe()
	{
    }

    public void ExportComplexRecipes()
    {
        this.recipes = ComplexRecipeManager.Get().recipes;
    }
}
