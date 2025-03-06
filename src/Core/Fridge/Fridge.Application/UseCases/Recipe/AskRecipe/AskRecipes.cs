using System.Text.Json.Serialization;
using Fridge.Domain.Recipes.AskRecipes;

namespace Fridge.Application.UseCases.Recipe.AskRecipe;

public class AskRecipes : IAskRecipes
{
    private readonly IRepository<FridgeItemModel,FridgeContext> _rFridgeItem;
    private readonly IRepository<FridgeModel,FridgeContext> _rFridge;
    private readonly IChatGpt _chatGpt;
    
    public AskRecipes(IRepository<FridgeItemModel, FridgeContext> rFridgeItem, IChatGpt chatGpt, IRepository<FridgeModel, FridgeContext> rFridge)
    {
        _rFridgeItem = rFridgeItem;
        _chatGpt = chatGpt;
        _rFridge = rFridge;
    }
    public async Task<List<IAskRecipesOut>> ExecuteAsync(IAskRecipesIn request)
    {
        try
        {
            var userFridge = await _rFridge.Get(g=>g.UserId == request.UserId).FirstOrDefaultAsync();
            
            var items = _rFridgeItem.Get(g => g.IsActive && g.FridgeId == userFridge.Id);

            var itemNames = string.Join(',', items.Select(s => s.Name));
            if (string.IsNullOrEmpty(itemNames))
                return new List<IAskRecipesOut>();
            var assistMessage =
                $"Você é um assistente que gera receitas no formato JSON. A resposta deve ser apenas uma lista de modelos de receita, contendo o título (title), ingredientes (ingredients, uma lista de strings) e o método de preparo (method). A resposta deve estar em português brasileiro, com os seguintes ingredientes: {itemNames}. Não inclua nada além do modelo da lista em formato JSON.";
            var r = await _chatGpt.AskAssistant<List<AskRecipesOut>>(assistMessage);  
            return new List<IAskRecipesOut>().Concat(r).ToList();
        }
        catch (Exception ex)
        {
            throw ;
        }
    }
}