using Statistic.Domain.Statistics.Create;

namespace Statistic.Application.Statistics.CreateExpired;

public class CreateExpiredStatisticIn : ICreateExpiredStatisticIn
{
    public Guid ItemId { get; set; }
    public Guid UserId { get; set; }
    public float ItemWeight { get; set; }
}