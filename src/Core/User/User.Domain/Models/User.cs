using Microsoft.AspNetCore.Identity;
using Models;

namespace User.Domain.Models;

public class User : IdentityUser<Guid> , IEntity
{
    public string Name { get; set; }
    public string LastName { get; set; }    
    public DateTime BirthDate { get; set; } 
    public bool IsFirstAcess { get; set; }

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