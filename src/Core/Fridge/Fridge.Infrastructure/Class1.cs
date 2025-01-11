using Microsoft.EntityFrameworkCore;

namespace Fridge.Infrastructure;
public class FridgeContext : DbContext
{
    public FridgeContext(DbContextOptions<FridgeContext> options) : base(options)   
    {
        
    }
}