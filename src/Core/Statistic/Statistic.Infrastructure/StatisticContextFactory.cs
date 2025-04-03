using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Statistic.Infrastructure;

public class StatisticContextFactory :IDesignTimeDbContextFactory<StatisticContext>
{
    public StatisticContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<StatisticContext>();
        optionsBuilder.UseSqlServer("");
        return new StatisticContext(optionsBuilder.Options);
    }
}