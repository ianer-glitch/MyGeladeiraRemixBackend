using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Ports;
using Statistic.Application.Statistics.GetByUser;
using Statistic.Domain.FoodWasteIndexes;
using Statistic.Domain.Statistics.GetByAllUser;
using Statistic.Infrastructure;

namespace Statistic.Application.Statistics.GetByAllUser;

public class GetStatisticByAllUser : IGetStatisticByAllUser
{
    private readonly IRepository<UserFoodWasteIndex,StatisticContext> _rUserFoodWasteIndex;
    private readonly IConfiguration _configuration;

    public GetStatisticByAllUser(IRepository<UserFoodWasteIndex, StatisticContext> rUserFoodWasteIndex, IConfiguration configuration)
    {
        _rUserFoodWasteIndex = rUserFoodWasteIndex;
        _configuration = configuration;
    }
    public async Task<IGetStatisticByAllUserOut> ExecuteAsync(IGetStatisticByAllUserIn request)
    {
        try
        {
            var userIdexes = _rUserFoodWasteIndex.Get(g=>true);
            var userIndexAverage = (await userIdexes.Select(s => s.Index).SumAsync())/await userIdexes.CountAsync();
            
       


            
            var nationalConfig = _configuration.GetSection("NATIONAL_FOOD_WASTE_INDEX").Value;
            if(string.IsNullOrEmpty(nationalConfig))
                throw new ArgumentNullException(nationalConfig);
            
            var national = float.Parse(nationalConfig); 
            
            var result = new GetStatisticByAllUserOut
            {
                UserFoodWasteIndex = userIndexAverage,
                NationalFoodWasteIndex = national,
            };
            
            return result;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}