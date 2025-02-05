using Microsoft.EntityFrameworkCore;
using Statistic.Domain;
using Statistic.Domain.FoodWasteIndexes;
using Statistic.Domain.Statistics;

namespace Statistic.Infrastructure;

public class StatisticContext : DbContext
{
    public DbSet<Domain.Statistics.Statistic> Statistics { get; set; }
    public DbSet<ExpiredStatistic> ExpiredStatistics { get; set; }
    public DbSet<UserFoodWasteIndex> UserFoodWasteIndexes { get; set; }
    
    public StatisticContext(DbContextOptions<StatisticContext> options): base(options)
    {
        
    }
    
   
}