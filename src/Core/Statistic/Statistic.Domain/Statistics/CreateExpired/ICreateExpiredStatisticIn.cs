namespace Statistic.Domain.Statistics.Create;

public interface ICreateExpiredStatisticIn
{
    public Guid ItemId { get; set; }    
    public Guid UserId { get; set; }
    public double ItemWeight { get; set; }
    
}