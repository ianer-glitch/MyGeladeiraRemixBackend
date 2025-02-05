namespace Statistic.Domain.Statistics.CreateExpired;

public interface ICreateExpiredStatisticIn
{
    public Guid ItemId { get; set; }    
    public Guid UserId { get; set; }
    public double ItemWeight { get; set; }
    
}