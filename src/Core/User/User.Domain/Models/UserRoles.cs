using Microsoft.AspNetCore.Identity;
using Models;

namespace User.Domain.Models;

public class UserRoles : IdentityRole<Guid>, IEntity
{
    public DateTime Inclusion { get; set; }
    public DateTime Modified { get; set; }
    public bool IsActive { get; set; }
    public Guid UserInclusionId { get; set; }
    public Guid UserModifiedId { get; set; }
    public void SetActive()
    {
        throw new NotImplementedException();
    }

    public void SetInactive()
    {
        throw new NotImplementedException();
    }

    public bool Equals()
    {
        throw new NotImplementedException();
    }
}