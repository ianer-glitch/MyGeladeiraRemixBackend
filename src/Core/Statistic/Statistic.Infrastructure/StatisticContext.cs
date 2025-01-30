using Microsoft.EntityFrameworkCore;

namespace Statistic.Infrastructure;

public class StatisticContext : DbContext
{
    public StatisticContext(DbContextOptions<StatisticContext> options): base(options)
    {
        
    }
}