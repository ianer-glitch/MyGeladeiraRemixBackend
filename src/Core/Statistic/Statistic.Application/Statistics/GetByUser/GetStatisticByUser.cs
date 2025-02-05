using Microsoft.Extensions.Configuration;
using Ports;
using Statistic.Domain.FoodWasteIndexes;
using Statistic.Domain.Statistics.GetByUser;
using Statistic.Infrastructure;

namespace Statistic.Application.Statistics.GetByUser;

public class GetStatisticByUser : IGetStatisticByUser
{
    private readonly IRepository<UserFoodWasteIndex,StatisticContext> _rUserFoodWasteIndex;
    private readonly IConfiguration _configuration;
    
    public GetStatisticByUser(IRepository<UserFoodWasteIndex, StatisticContext> rUserFoodWasteIndex, IConfiguration configuration)
    {
        _rUserFoodWasteIndex = rUserFoodWasteIndex;
        _configuration = configuration;
    }

    public async Task<IGetStatisticByUserOut> ExecuteAsync(IGetStatisticByUserIn request)
    {
        try
        {
            var userIndex = _rUserFoodWasteIndex.Get(g => g.UserInclusionId == request.UserId).FirstOrDefault();
            if (userIndex == null)
                throw new ArgumentNullException($"{nameof(userIndex)} not found");


            
            var nationalConfig = _configuration.GetSection("NATIONAL_FOOD_WASTE_INDEX").Value;
            if(string.IsNullOrEmpty(nationalConfig))
                throw new ArgumentNullException(nationalConfig);
            
            var national = float.Parse(nationalConfig); 
            
            var result = new GetStatisticByUserOut
                {
                    UserFoodWasteIndex = userIndex.Index,
                    NationalFoodWasteIndex = national,
                };
            
            return await Task.FromResult(result);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}