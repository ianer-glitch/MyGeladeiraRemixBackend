namespace Statistic.Domain.Statistics.GetByUser;

public interface IGetStatisticByUser
{
    public Task<IGetStatisticByUserOut> ExecuteAsync(IGetStatisticByUserIn request);
}