using Fridge.Domain.ShoppingLists;

namespace Fridge.Domain.UnitTests;

public class ShoppingListTests
{

    [Fact]
    public void Constructor_Empty_ShouldCreateInstance()
    {
        var list = new ShoppingList();

        Assert.NotNull(list);
        Assert.Equal(Guid.Empty, list.UserId); 
    }
    
    [Fact]
    public void Constructor_WithValidUserId_ShouldSetUserId()
    {
        var userId = Guid.NewGuid();

        var list = new ShoppingList(userId);

        Assert.Equal(userId, list.UserId);
    }

    [Fact]
    public void Constructor_WithEmptyUserId_ShouldSetEmpty()
    {
        var list = new ShoppingList(Guid.Empty);

        Assert.Equal(Guid.Empty, list.UserId);
    }

    [Fact]
    public void UserId_SetAndGet_ShouldMatch()
    {
        var list = new ShoppingList();
        var userId = Guid.NewGuid();

        list.UserId = userId;

        Assert.Equal(userId, list.UserId);
    }
}
