using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using User.Domain.Models;

namespace User.Infrastructure;

public class UserContext : IdentityDbContext<Domain.Models.User,UserRoles,Guid>
{
    public DbSet<UserClaims> UsersClaims { get; set; }
    public UserContext(DbContextOptions<UserContext> options) : base(options)   
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<UserClaims>().ToTable("AspNetUserClaims"); 
    }
}