namespace Statistic.Domain.Statistics.Create;

public interface ICreateExpiredStatistic
{
    public Task<ICreateExpiredStatisticOut> ExecuteAsync(ICreateExpiredStatisticIn request);
}