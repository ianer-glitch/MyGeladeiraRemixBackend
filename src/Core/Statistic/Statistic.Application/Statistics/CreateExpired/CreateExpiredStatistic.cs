using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ports;
using Statistic.Domain;
using Statistic.Domain.FoodWasteIndexes;
using Statistic.Domain.FoodWasteIndexes.ValueObjects;
using Statistic.Domain.Statistics;
using Statistic.Domain.Statistics.Create;
using Statistic.Domain.Statistics.CreateExpired;
using Statistic.Infrastructure;

namespace Statistic.Application.Statistics.CreateExpired;

public class CreateExpiredStatistic :IHostedService,ICreateExpiredStatistic
{
    private readonly IServiceProvider _serviceProvider; 
    private readonly IConfiguration _configuration;
    public CreateExpiredStatistic(IServiceProvider serviceProvider , IConfiguration configuration)
    {
        _serviceProvider = serviceProvider;
        _configuration = configuration;
    }
    public async Task<ICreateExpiredStatisticOut> ExecuteAsync(ICreateExpiredStatisticIn request)
    {
        try
        {
            var scope = _serviceProvider.CreateScope();
            
                var _rStatistics = scope.ServiceProvider.GetRequiredService<IRepository<Domain.Statistics.Statistic, StatisticContext>>();
                var _rExpiredStatistics = scope.ServiceProvider.GetRequiredService<IRepository<Domain.Statistics.ExpiredStatistic, StatisticContext>>();
                var _userStatistics = scope.ServiceProvider.GetRequiredService<IRepository<UserFoodWasteIndex, StatisticContext>>();
                
                var statistic = new Domain.Statistics.Statistic(request.UserId);
                await _rStatistics.InsertAsync(statistic);
                var sucessStatistic = await _rStatistics.SaveChangesAsync() > 0;
                if(!sucessStatistic)
                    throw new Exception("Can't create statistic");
                
                var expiredStatistic =
                    new ExpiredStatistic(request.ItemId, request.ItemWeight, statistic.Id, request.UserId);
                
                await _rExpiredStatistics.InsertAsync(expiredStatistic);
                var sucessExpired = await _rExpiredStatistics.SaveChangesAsync() > 0;
                if(!sucessExpired)
                    throw new Exception("Can't create expired statistic");

                await  CreateFoodWasteIndexAsync(_rExpiredStatistics, _userStatistics, request.UserId);         
                scope.Dispose();    
            
            
            return new CreateExpiredStatisticOut();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private async Task CreateFoodWasteIndexAsync(
        IRepository<Domain.Statistics.ExpiredStatistic, StatisticContext> _rExpired,
        IRepository<UserFoodWasteIndex, StatisticContext> _rUserStatistics,Guid userId)
    {

        try
        {
            
            var itemWeights = _rExpired.Get(g => g.Inclusion > DateTime.Now.AddMonths(-1))
                                                      .AsNoTracking()
                                                      .Select(s => s.ItemWeight);
            
            var nationalConfig = _configuration.GetSection("NATIONAL_FOOD_WASTE_INDEX").Value;
            if(string.IsNullOrEmpty(nationalConfig))
                throw new ArgumentNullException(nationalConfig);
            
            var national = float.Parse(nationalConfig);
            
            var index = new FoodWasteIndex()
                .SetNationalIndexPerMonth(national)
                .SetInitiaUserIndexPerMonth(national)
                .CalculateMonthUserIndex(itemWeights)
                .GetIndex();
            
            await _rUserStatistics.InsertAsync(new UserFoodWasteIndex(index,userId));
            await _rUserStatistics.SaveChangesAsync();
            
            

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var scope = _serviceProvider.CreateScope();
        var  _listenObjectsFromQueue= scope.ServiceProvider.GetRequiredService<IListenObjectsFromQueue>();
        
        await Task.Run(async () =>
        {
            await _listenObjectsFromQueue
                .ExecuteAsync<ICreateExpiredStatisticIn,ICreateExpiredStatisticOut>
                    (ExecuteAsync,cancellationToken,EQueue.ExpiredStatistic);
        });
        
        scope.Dispose();
            
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}