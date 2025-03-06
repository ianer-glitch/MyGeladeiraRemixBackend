namespace Fridge.Domain.Recipes.AskRecipes;

public interface IAskRecipesOut
{
    public string Title { get; set; }
    public List<string> Ingredients { get; set; }
    public string Method { get; set; }
}