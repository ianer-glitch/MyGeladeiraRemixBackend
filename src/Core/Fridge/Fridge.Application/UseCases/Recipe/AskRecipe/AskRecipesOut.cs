using Fridge.Domain.Recipes.AskRecipes;

namespace Fridge.Application.UseCases.Recipe.AskRecipe;

public class AskRecipesOut : IAskRecipesOut
{
    public string Title { get; set; }
    public List<string> Ingredients { get; set; }
    public string Method { get; set; }
}