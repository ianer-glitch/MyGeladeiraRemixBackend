using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace User.Infrastructure;

public class UserContext : IdentityDbContext<Domain.Models.User,IdentityRole<Guid>,Guid>
{
    public UserContext(DbContextOptions<UserContext> options)
    {
        
    }
}