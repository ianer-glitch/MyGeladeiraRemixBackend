using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Fridge.Infrastructure;

public class FridgeContextFactory : IDesignTimeDbContextFactory<FridgeContext>
{
    public FridgeContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<FridgeContext>();
        optionsBuilder.UseSqlServer("");
        return new FridgeContext(optionsBuilder.Options);
    }
}