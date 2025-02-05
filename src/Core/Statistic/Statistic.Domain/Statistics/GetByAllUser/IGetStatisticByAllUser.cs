namespace Statistic.Domain.Statistics.GetByAllUser;

public interface IGetStatisticByAllUser
{
    public Task<IGetStatisticByAllUserOut> ExecuteAsync(IGetStatisticByAllUserIn request);
    
}