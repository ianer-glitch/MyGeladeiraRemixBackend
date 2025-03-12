namespace Fridge.Domain.Recipes.AskRecipes;

public interface IAskRecipesIn
{
    public Guid UserId { get; set; }  
    public string ResponseLanguage { get; set; }
    
}