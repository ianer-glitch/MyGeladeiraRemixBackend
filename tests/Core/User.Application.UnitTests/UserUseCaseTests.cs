using System.Globalization;
using Google.Protobuf.WellKnownTypes;
using Identity.Domain.Protos;
using Microsoft.AspNetCore.Identity;
using Moq;
using User.Application.UnitTests.Helpers;
using User.Application.UseCases;

namespace User.Application.UnitTests;

public class UserUseCaseTests
{
    private static Domain.Models.User GetValidUser => new("some name", "some lastname",DateTime.Parse("1999-01-01"),"some@mail.com");
    [Fact]
    public async Task Login_WhenUser_NotFound_ShouldReturnFalse()
    {
        var userList = new List<Domain.Models.User>()
        {
            new Domain.Models.User()
            {
                Id = Guid.NewGuid(),
                Email = "email@email.com",
            }
        };
        var mockUserManager = MockUserManager.Basic<Domain.Models.User>(userList);
        mockUserManager.Setup(x=>x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(null as Domain.Models.User);
        mockUserManager.Setup(x => x.CheckPasswordAsync(It.IsAny<Domain.Models.User>(), It.IsAny<string>()))
            .ReturnsAsync(false);
        
        
        var userUseCase = new UserUseCase(mockUserManager.Object);
        var request = new PIsUserPasswordValidIn()
        {
            Email = "some@mail.com",
            Password = "12345",
        };

        var act = await userUseCase.IsUserPasswordValidAsync(request);
        Assert.False(act);  
        
        
    }

    [Fact]
    public async Task Login_WhenUserIsValid_ShouldReturnTrue()
    {
        var userList = new List<Domain.Models.User>()
        {
            new Domain.Models.User()
            {
                Id = Guid.NewGuid(),
                Email = "email@email.com",
            }
        };
        var mockUserManager = MockUserManager.Basic<Domain.Models.User>(userList);
        mockUserManager.Setup(x=>x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(userList.FirstOrDefault(f=>f.Email=="email@email.com"));
        mockUserManager.Setup(x => x.CheckPasswordAsync(It.IsAny<Domain.Models.User>(), It.IsAny<string>()))
            .ReturnsAsync(true);
        
        var userUseCase = new UserUseCase(mockUserManager.Object);
        var request = new PIsUserPasswordValidIn
        {
            Email = "some@mail.com",
            Password = "12345",
        };

        var act = await userUseCase.IsUserPasswordValidAsync(request);
        Assert.True(act);  

    }

    [Fact]
    public async Task CreateUser_EmailAlreadyExists_ShouldReturnFalse()
    {
        var userList = new List<Domain.Models.User>()
        {
            new Domain.Models.User()
            {
                Id = Guid.NewGuid(),
                Email = "email@email.com",
            }
        };
        var mockUserManager = MockUserManager.Basic<Domain.Models.User>(userList);
        mockUserManager.Setup(x=>x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(userList.FirstOrDefault(f=>f.Email=="email@email.com"));
        
        var userUseCase = new UserUseCase(mockUserManager.Object);
        var request = new PCreateUserIn()
        {
            Email = "email@email.com",
            Password = "12345",
            BirthDate = DateTime.UtcNow.ToTimestamp(),
            LastName = "lastname",
            FirstName = "firstname",
        };
        
        var act = await userUseCase.CreateUserAsync(request);
        Assert.False(act);

    }

    [Fact]
    public async Task CreateUser_WhenCorrect_RetursTrue()
    {
        var userList = new List<Domain.Models.User>() {};
        var mockUserManager = MockUserManager.Basic<Domain.Models.User>(userList);
        mockUserManager.Setup(x=>x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(null as Domain.Models.User);
        mockUserManager.Setup(x => x.CreateAsync(It.IsAny<Domain.Models.User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
        var userUseCase = new UserUseCase(mockUserManager.Object);
        var request = new PCreateUserIn()
        {
            Email = "email@email.com",
            Password = "12345",
            BirthDate = DateTime.Parse("1999-01-01").ToUniversalTime().ToTimestamp(),
            LastName = "lastname",
            FirstName = "firstname",
        };
        
        var act = await userUseCase.CreateUserAsync(request);
        
        Assert.True(act);
    }
}