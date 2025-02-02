using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ports;
using Statistic.Domain.Statistics;
using Statistic.Domain.Statistics.Create;
using Statistic.Infrastructure;

namespace Statistic.Application.Statistics.CreateExpired;

public class CreateExpiredStatistic :IHostedService,ICreateExpiredStatistic
{
    private readonly IServiceProvider _serviceProvider; 
    
    public CreateExpiredStatistic(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public async Task<ICreateExpiredStatisticOut> ExecuteAsync(ICreateExpiredStatisticIn request)
    {
        try
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _rStatistics = scope.ServiceProvider.GetRequiredService<IRepository<Domain.Statistics.Statistic, StatisticContext>>();
                var _rExpiredStatistics = scope.ServiceProvider.GetRequiredService<IRepository<Domain.Statistics.ExpiredStatistic, StatisticContext>>();
                
                var statistic = new Domain.Statistics.Statistic(request.UserId);
                await _rStatistics.InsertAsync(statistic);
                var sucessStatistic = await _rStatistics.SaveChangesAsync() > 0;
                if(!sucessStatistic)
                    throw new Exception("Can't create statistic");
                
                var expiredStatistic =
                    new ExpiredStatistic(request.ItemId, request.ItemWeight, statistic.Id, request.UserId);
                
                await _rExpiredStatistics.InsertAsync(expiredStatistic);
                await _rExpiredStatistics.SaveChangesAsync();
            }
            
            return new CreateExpiredStatisticOut();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public  Task StartAsync(CancellationToken cancellationToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var  _listenObjectsFromQueue= scope.ServiceProvider.GetRequiredService<IListenObjectsFromQueue>();  
            _listenObjectsFromQueue
                .Execute<ICreateExpiredStatisticIn,Task<ICreateExpiredStatisticOut>>
                    (ExecuteAsync,cancellationToken,EQueue.ExpiredStatustic);
            
        }
        
        return Task.CompletedTask;
            
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}