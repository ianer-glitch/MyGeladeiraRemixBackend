using Statistic.Domain.Statistics.Create;
using Statistic.Domain.Statistics.CreateExpired;

namespace Statistic.Application.Statistics.CreateExpired;

public class CreateExpiredStatisticIn : ICreateExpiredStatisticIn
{
    public Guid ItemId { get; set; }
    public Guid UserId { get; set; }
    public double ItemWeight { get; set; }
}