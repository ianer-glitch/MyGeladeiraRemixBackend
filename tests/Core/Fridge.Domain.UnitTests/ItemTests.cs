using Fridge.Domain.Items;
using Microsoft.AspNetCore.Authentication;

namespace Fridge.Domain.UnitTests;

public class ItemTests
{

    public static Item GetValidItem =>
        new("Tomate",
            "tomato",
            DateTime.UtcNow.AddDays(5),
            1,
            4,
            "someIcon"
            , 1,
            Guid.NewGuid());

    [Fact]
    public void SetName_WhenNameIsEmpty_ThrowsArgumentNullException()
    {
        var item = GetValidItem;
        var act = () => item.SetName(string.Empty);
        Assert.Throws<ArgumentNullException>(act);
    }

    [Fact]
    public void SetName_WhenNameIsValid_DoesNotThrow()
    {
        var item = GetValidItem;
        var itemName = "Abacate";
        item.SetName(itemName);

        Assert.Equal(item.Name, itemName);
    }

    [Fact]
    public void SetColor_WhenNameIsEmpty_ThrowsArgumentNullException()
    {
        var item = GetValidItem;
        var act = () => item.SetColor(string.Empty);
        Assert.Throws<ArgumentNullException>(act);
    }

    [Fact]
    public void SetColor_WhenNameIsValid_DoesNotThrow()
    {
        var item = GetValidItem;
        var itemColor = "Orange";
        item.SetColor(itemColor);

        Assert.Equal(item.Color, itemColor);
    }

    [Fact]
    public void SetIcon_WhenIconIsEmpty_ThrowsArgumentNullException()
    {
        var item = GetValidItem;
        var act = () => item.SetIconName(string.Empty);
        Assert.Throws<ArgumentNullException>(act);
    }

    [Fact]
    public void SetIconName_WhenNameIsValid_DoesNotThrow()
    {
        var item = GetValidItem;
        var iconName = "new icon";
        item.SetIconName(iconName);

        Assert.Equal(item.IconName, iconName);
    }

    [Fact]
    public void SetWeight_WhenWeightIsZero_ThrowsArgumentException()
    {
        var item = GetValidItem;
        var act = () => item.SetWeight(0);
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void SetWeight_WhenWeightIsPositive_DoesNotThrow()
    {
        var item = GetValidItem;
        var weight = 100;
        item.SetWeight(weight);
        Assert.Equal(weight, item.Weight);
    }

    [Fact]
    public void Constructor_Empty_ShouldCreateInstance()
    {
        var item = new Item();
        Assert.NotNull(item);
    }

    [Fact]
    public void Constructor_WithValidParameters_ShouldSetProperties()
    {
        var name = "Butter";
        var color = "Yellow";
        var expiration = DateTime.UtcNow.AddDays(5);
        var minQty = 2;
        var qty = 1;
        var icon = "icon-butter";
        var weight = 1.5;
        var userId = Guid.NewGuid();

        var item = new Item(name, color, expiration, minQty, qty, icon, weight, userId);

        Assert.Equal(name, item.Name);
        Assert.Equal(color, item.Color);
        Assert.Equal(expiration, item.Expiration);
        Assert.Equal(minQty, item.MinimunQuantity);
        Assert.Equal(qty, item.Quantity);
        Assert.Equal(icon, item.IconName);
        Assert.Equal(weight, item.Weight);
    }



    [Fact]
    public void SetName_WithValidValue_ShouldSet()
    {
        var item = new Item();
        item.SetName("Tomato");
        Assert.Equal("Tomato", item.Name);
    }

    [Fact]
    public void SetName_NullOrEmpty_ShouldThrow()
    {
        var item = new Item();
        Assert.Throws<ArgumentNullException>(() => item.SetName(null));
        Assert.Throws<ArgumentNullException>(() => item.SetName(""));
    }



    [Fact]
    public void SetColor_WithValidValue_ShouldSet()
    {
        var item = new Item();
        item.SetColor("Red");
        Assert.Equal("Red", item.Color);
    }

    [Fact]
    public void SetColor_NullOrEmpty_ShouldThrow()
    {
        var item = new Item();
        Assert.Throws<ArgumentNullException>(() => item.SetColor(null));
        Assert.Throws<ArgumentNullException>(() => item.SetColor(""));
    }


    [Fact]
    public void SetIconName_WithValidValue_ShouldSet()
    {
        var item = new Item();
        item.SetIconName("icon-apple");
        Assert.Equal("icon-apple", item.IconName);
    }

    [Fact]
    public void SetIconName_NullOrEmpty_ShouldThrow()
    {
        var item = new Item();
        Assert.Throws<ArgumentNullException>(() => item.SetIconName(null));
        Assert.Throws<ArgumentNullException>(() => item.SetIconName(""));
    }

    [Fact]
    public void SetWeight_WithValidValue_ShouldSet()
    {
        var item = new Item();
        item.SetWeight(1.0);
        Assert.Equal(1.0, item.Weight);
    }

    [Fact]
    public void SetWeight_ZeroNegativeOrNaN_ShouldThrow()
    {
        var item = new Item();

        Assert.Throws<ArgumentException>(() => item.SetWeight(0));
        Assert.Throws<ArgumentException>(() => item.SetWeight(-5));
        Assert.Throws<ArgumentException>(() => item.SetWeight(double.NaN));
    }


    [Fact]
    public void SetTimeToExpire_WithValidDate_ShouldSetDays()
    {
        var item = new Item();
        var future = DateTime.Today.AddDays(7);
        item.SetTimeToExpire(future);
        Assert.Equal(7, item.TimeToExpireInDays);
    }

    [Fact]
    public void CurrecntExpirationDate_ShouldReturnCorrectFutureDate()
    {
        var item = new Item();
        item.TimeToExpireInDays = 10;
        var expected = DateTime.UtcNow.AddDays(10);
        var diff = Math.Abs((item.CurrecntExpirationDate - expected).TotalSeconds);
        Assert.True(diff < 1);
    }

    [Fact]
    public void ShouldAddToShoppingList_WhenQuantityLessThanMinimum_ShouldReturnTrue()
    {
        var item = new Item { Quantity = 1, MinimunQuantity = 5 };
        Assert.True(item.ShouldAddToShoppingList);
    }

    [Fact]
    public void ShouldAddToShoppingList_WhenQuantityMeetsOrExceedsMinimum_ShouldReturnFalse()
    {
        var item = new Item { Quantity = 5, MinimunQuantity = 5 };
        Assert.False(item.ShouldAddToShoppingList);

        item.Quantity = 6;
        Assert.False(item.ShouldAddToShoppingList);
    }
}


