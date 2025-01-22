using Identity.API.Helpers;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Identity.UnitTests;

public class TokenHelpersTests
{
    [Fact]
    public void GenerateToken_GeneratesValidToken_ShouldReturnString()
    {
        var conf =  new Mock<IConfiguration>();
        var mockConfSection = new Mock<IConfigurationSection>();
        
        conf.Setup(c => c.GetSection("JwtConfiguration")).Returns(mockConfSection.Object);
        mockConfSection.Setup(c => c.GetSection("Issuer").Value).Returns("Issuer");
        mockConfSection.Setup(c => c.GetSection("Audience").Value).Returns("Audience");
        mockConfSection.Setup(c => c.GetSection("ExpirationTimeInMinutes").Value).Returns("10");
        mockConfSection.Setup(c => c.GetSection("SecurityKey").Value).Returns("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
        
        var token = TokenHelpers.GenerateToken(conf.Object);
        Assert.NotNull(token);  
        Assert.NotEmpty(token);
        Assert.True(token.Length > 0);
        Assert.IsType<string>(token);
    }
    
    [Fact]
    public void GenerateToken_InvalidConfiguration_ShoulThrowsArgumentNullException()
    {
        var conf =  new Mock<IConfiguration>();
        
        var act = () => TokenHelpers.GenerateToken(conf.Object);
        
        Assert.Throws<ArgumentNullException>(act);  
    }
}