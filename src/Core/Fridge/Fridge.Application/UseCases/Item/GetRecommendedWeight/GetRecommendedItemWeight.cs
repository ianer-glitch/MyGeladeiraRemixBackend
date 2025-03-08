using Fridge.Domain.Fridges.GetRecommendedItem;
using Fridge.Domain.Items.GetRecommendedWeight;

namespace Fridge.Application.UseCases.Item.GetRecommendedWeight;

public class GetRecommendedItemWeight:IGetRecommendedItemWeight
{
    private readonly IChatGpt _chatGpt;

    public GetRecommendedItemWeight(IChatGpt chatGpt)
    {
        _chatGpt = chatGpt;
    }
    public async Task<IGetRecommendedItemWeightOut> ExecuteAsync(IGetRecommendedItemWeightIn request)
    {
        try
        {
            var assistMessage =
                $"Você é um assistente que retorna o peso médio de um alimento em formato númerico, em gramas.Responda de forma precisa apenas o valor do peso desse alimento :{request.Name}";
            var r = await _chatGpt.AskAssistant(assistMessage);
            return new GetRecommendedItemWeightOut
            {
                Weight = double.Parse(r) 
            };
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}