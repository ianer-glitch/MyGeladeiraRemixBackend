using Statistic.Domain.Statistics.Create;
using Statistic.Domain.Statistics.CreateExpired;

namespace Ports.Expired;

public class CreateExpiredStatisticIn : ICreateExpiredStatisticIn
{
    public Guid ItemId { get; set; }
    public Guid UserId { get; set; }
    public double ItemWeight { get; set; }
}