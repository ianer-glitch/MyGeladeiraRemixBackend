namespace User.Domain.UnitTests;
using  User = Domain.Models.User;

public class UserTests
{
    private static User GetValidUser => new("some name", "some lastname",DateTime.Parse("1999-01-01"),"some@mail.com");

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void SetName_InvalidName_ThrowsArgumentNullException(string value)
    {
        var user = GetValidUser;
        Action act = () => user.SetName(value);
        Assert.Throws<ArgumentNullException>(act);
    }

    [Fact]
    public void SetName_ValidName_SetsNameProperty()
    {
        var user = GetValidUser;
        var name = "some name 2";
        user.SetName(name);  
        Assert.Equal(name, user.Name);  
    }    

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void SetLastName_InvalidLastName_ThrowsArgumentNullException(string value)
    {
        var user = GetValidUser;    
        Action act = () => user.SetLastName(value); 
        Assert.Throws<ArgumentNullException>(act);
    }
    
    [Fact]
    public void SetLastName_ValidLastName_SetsLastNameProperty()
    {
        var user = GetValidUser;
        var lastName = "some lastname 2";
        user.SetLastName(lastName);
        Assert.Equal(lastName, user.LastName);
    }

    [Theory]
    [InlineData("")]
    [InlineData("aaa.com")]
    [InlineData(null)]
    public void SetEmail_InvalidEmail_ThrowsArgumentException(string value)
    {
        var user = GetValidUser;
        Action act = () => user.SetEmail(value);
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void SetEmail_ValidEmail_SetsEmailProperty()
    {
        var user = GetValidUser;    
        var email = "some@email.address";   
        user.SetEmail(email);
        Assert.Equal(email, user.Email);
    }

    [Fact]
    public void SetBirthDate_InvalidBirthDate_ThrowsArgumentException()
    {
        var user = GetValidUser;
        var date = DateTime.Parse("2099-01-01");
        
        var act = () => user.SetBirtDate(date);
        Assert.Throws<ArgumentException>(act);

    }

    [Fact]
    public void SetBirthDate_ValidBirthDate_SetsBirthDateProperty()
    {
        var user = GetValidUser;
        var birthDate = DateTime.Parse("1999-01-01");
        user.SetBirtDate(birthDate);
        Assert.Equal(birthDate, user.BirthDate);
        
    }
    
    
    
}
