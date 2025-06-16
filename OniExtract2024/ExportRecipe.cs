using OniExtract2024;
using System.Collections.Generic;

public class ExportRecipe : BaseExport
{
    public override string ExportFileName { get; set; } = "recipe";
    public List<ComplexRecipe> recipes = null;
    public HashSet<ComplexRecipe> preProcessRecipes = new HashSet<ComplexRecipe>();
    public Dictionary<string, string> obsoleteIDMapping = new Dictionary<string, string>();

    public ExportRecipe()
	{
    }

    public void ExportComplexRecipes()
    {
        ComplexRecipeManager manager = ComplexRecipeManager.Get();
        this.recipes = manager.recipes;
        this.preProcessRecipes = manager.preProcessRecipes;
    }
}
