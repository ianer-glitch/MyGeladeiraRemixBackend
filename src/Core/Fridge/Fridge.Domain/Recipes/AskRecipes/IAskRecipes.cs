namespace Fridge.Domain.Recipes.AskRecipes;

public interface IAskRecipes
{
    public Task<List<IAskRecipesOut>> ExecuteAsync(IAskRecipesIn request);
}