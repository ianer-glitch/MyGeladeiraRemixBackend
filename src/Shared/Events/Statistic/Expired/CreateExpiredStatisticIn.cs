using Statistic.Domain.Statistics.Create;

namespace Ports.Expired;

public class CreateExpiredStatisticIn : ICreateExpiredStatisticIn
{
    public Guid ItemId { get; set; }
    public Guid UserId { get; set; }
    public float ItemWeight { get; set; }
}