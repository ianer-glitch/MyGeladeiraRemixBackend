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
                $"You are an assistant that generates recipes in valid JSON format to be used with JSON.parse(). The response should be only a list of recipe templates, containing the title (title), ingredients (ingredients, a list of strings), and the method of preparation (method). The response should be in Brazilian Portuguese, with the following ingredients: {itemNames}. Do not include anything other than the list model in JSON format";
            var responseInJson = await _chatGpt.AskAssistant(assistMessage);  
            
            if (string.IsNullOrEmpty(responseInJson))
                return new List<IAskRecipesOut>();
            
            var r =  Newtonsoft.Json.JsonConvert.DeserializeObject<List<AskRecipesOut>>(responseInJson);
            return new List<IAskRecipesOut>().Concat(r).ToList();
        }
        catch (Exception ex)
        {
            throw ;
        }
    }
}