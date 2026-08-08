using OniExtract2024;
using System.Collections.Generic;
using System.Linq;

public class ExportRecipe : BaseExport
{
    public override string ExportFileName { get; set; } = "recipe";
    public List<ComplexRecipe> recipes;

    public ExportRecipe() { }

    public void ExportComplexRecipes()
    {
        this.recipes = ComplexRecipeManager.Get().preProcessRecipes.ToList();
    }
}
