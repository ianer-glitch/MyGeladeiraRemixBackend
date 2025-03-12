using Fridge.Domain.Fridges.GetRecommendedItem;

namespace Fridge.Application.UseCases.Fridge.GetRecommendedItem;

public class GetRecommendedItems : IGetRecommendedItems
{
    private readonly IRepository<FridgeItemModel,FridgeContext> _rFridgeItem;
    private readonly IRepository<FridgeModel,FridgeContext> _rFridge;
    private readonly IChatGpt _chatGpt;
    
    public GetRecommendedItems(IRepository<FridgeItemModel, FridgeContext> rFridgeItem, IRepository<FridgeModel, FridgeContext> rFridge, IChatGpt chatGpt)
    {
        _rFridgeItem = rFridgeItem;
        _rFridge = rFridge;
        _chatGpt = chatGpt;
    }
    public async Task<List<IGetRecommendedItemsOut>> ExecuteAsync(IGetRecommendedItemsIn request)
    {
        try
        {
            var userFridge = await _rFridge.Get(g=>g.UserId == request.UserId).FirstOrDefaultAsync();
            
            var items = _rFridgeItem.Get(g => g.IsActive && g.FridgeId == userFridge.Id);

            var itemNames = string.Join(',', items.Select(s => s.Name));
            if (string.IsNullOrEmpty(itemNames))
                return new List<IGetRecommendedItemsOut>();
            var assistMessage =
                $"Você é um assistente que recomenda alimentos não processados(como tomate ou cenoura, não pode ser receitas) em formato JSON.Me recomende alimentos sabendo que eu tenho na geladeira {itemNames} A resposta deve ser apenas uma lista de alimentos, contendo o nome (Name),e uma cor em hexadecimal(Color) que corresponde a cor do alimento. A resposta deve na linguagem {request.ResponseLanguage}, em caso de cor branca retorne a cor  #d9d9d9 e quantidade mnáxima deve ser 10 items ";
            var r = await _chatGpt.AskAssistant<List<GetRecommendedItemsOut>>(assistMessage);  
            return new List<IGetRecommendedItemsOut>().Concat(r).ToList();
        }
        catch (Exception ex)
        {
            throw ;
        }
    }
}