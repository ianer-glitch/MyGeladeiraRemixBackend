using Microsoft.AspNetCore.Identity;
using Models;

namespace User.Domain;

public class User : IdentityUser<Guid>, IEntity
{
    public DateTime Inclusion { get; set; }
    public DateTime Modified { get; set; }
    public bool IsActive { get; set; }
    public Guid UserInclusionId { get; set; }
    public Guid UserModifiedId { get; set; }
    public void SetActive()
    {
        IsActive = true;
        Modified = DateTime.Now;
    }

    public void SetInactive()
    {
        IsActive = false;
        Modified = DateTime.Now;
    }

    public bool Equals()
    {
        throw new NotImplementedException();
    }
}