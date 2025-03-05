using Fridge.Domain.Recipes.AskRecipes;

namespace Fridge.Application.UseCases.Recipe.AskRecipe;

public class AskRecipesIn : IAskRecipesIn
{
    public Guid UserId { get; set; }
}