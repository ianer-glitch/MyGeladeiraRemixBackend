using Fridge.Domain.Fridges;
using Fridge.Domain.Items;
using Microsoft.EntityFrameworkCore;

namespace Fridge.Infrastructure;

public class FridgeContext : DbContext
{
    public FridgeContext(DbContextOptions<FridgeContext> options) : base(options)  
    {
        
    }

    
    
    
    public DbSet<Item> Items { get; set; }
    public DbSet<Domain.Fridges.Fridge> Fridges { get; set; }
    public DbSet<FridgeItem> FridgeItems { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Item>()
            .ToTable("Items");  

        modelBuilder.Entity<FridgeItem>()
            .ToTable("FridgeItems"); 

    }
}
