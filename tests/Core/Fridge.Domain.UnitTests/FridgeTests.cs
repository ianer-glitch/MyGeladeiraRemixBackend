namespace Fridge.Domain.UnitTests;

public class FridgeTests
{
    [Fact]
    public void Constructor_ShouldCreateInstance_WithDefaultValues()
    {
        var fridge = new Fridges.Fridge();

        Assert.NotNull(fridge);
        Assert.Equal(Guid.Empty,fridge.UserId);
    }

    [Fact]
    public void Constructor_WithParameters_ShouldAssignValues()
    {
        var userId = Guid.NewGuid();
        var userInclusionId = Guid.NewGuid();

        var fridge = new Fridges.Fridge(userId, userInclusionId);

        Assert.Equal(userId, fridge.UserId);
        Assert.Equal(userInclusionId, fridge.UserInclusionId);
        
    }

    [Fact]
    public void UserId_Property_ShouldBeSettable()
    {
        var fridge = new Fridges.Fridge();
        var userId = Guid.NewGuid();

        fridge.UserId = userId;
        Assert.Equal(userId, fridge.UserId);    
        
    }
}