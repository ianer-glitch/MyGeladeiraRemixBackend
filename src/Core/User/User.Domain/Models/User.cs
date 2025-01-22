using Microsoft.AspNetCore.Identity;
using Models;

namespace User.Domain.Models;

public class User : IdentityUser<Guid> , IEntity
{

    public User(string name, string lastName, DateTime birthDate,string email)
    {
        SetName(name);
        SetLastName(lastName);
        SetBirtDate(birthDate);
        SetEmail(email);
    }
    public User()
    {
        
    }

    public void  SetName(string name)
    {
        if(string.IsNullOrEmpty(name))
            throw new ArgumentNullException(name);
        Name = name;
    }

    public void SetLastName(string lastName)
    {
        if(string.IsNullOrEmpty(lastName))
            throw new ArgumentNullException(lastName);
        LastName = lastName;
    }

    public void SetBirtDate(DateTime birthDate)
    {
        if(birthDate > DateTime.Now)
            throw new ArgumentException("Invalid birth date");
        BirthDate = birthDate;
    }

    public void SetEmail(string email)
    {
        if(string.IsNullOrEmpty(email) || !email.Contains('@'))
            throw new ArgumentException("Invalid email");   
        Email = email;
    }


    

    public void SetFalseFirstAcces()
    {
        IsFirstAcess = false;
    }
    
    
    public string Name { get; set; }
    public string LastName { get; set; }    
    public DateTime BirthDate { get; set; } 
    public bool IsFirstAcess { get; set; } = true;

    public DateTime Inclusion { get; set; }
    public DateTime? Modified { get; set; }
    public bool IsActive { get; set; } = true;
    public Guid UserInclusionId { get; set; }
    public Guid UserModifiedId { get; set; }
    public void SetActive()
    {
        IsActive = true;
    }

    public void SetInactive()
    {
        IsActive = false; 
    }

   

    public bool Equals<User>( User other ) where User : IEntity
    {
        throw new NotImplementedException();       
    }
}