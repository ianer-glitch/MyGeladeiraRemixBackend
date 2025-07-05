using Fridge.Domain.Fridges;

namespace Fridge.Domain.UnitTests;

public class FridgeItemTests
{
    [Fact]
    public void Constructor_WithValidParameters_ShouldSetProperties()
    {
        var name = "Milk";
        var color = "White";
        var expiration = DateTime.UtcNow.AddDays(10);
        var minQuantity = 5;
        var quantity = 3;
        var iconName = "milk_icon";
        var itemWeight = 2.5;
        var itemId = Guid.NewGuid();
        var fridgeId = Guid.NewGuid();
        var userInclusionId = Guid.NewGuid();

        var item = new FridgeItem(name, color, expiration, minQuantity, quantity, iconName, itemWeight, itemId, fridgeId, userInclusionId);

        Assert.Equal(name, item.Name);
        Assert.Equal(color, item.Color);
        Assert.Equal(expiration, item.Expiration);
        Assert.Equal(minQuantity, item.MinimunQuantity);
        Assert.Equal(quantity, item.Quantity);
        Assert.Equal(iconName, item.IconName);
        Assert.Equal(itemWeight, item.Weight);
        Assert.Equal(itemId, item.ItemId);
        Assert.Equal(fridgeId, item.FridgeId);
    }

    [Fact]
    public void SetName_WithValidName_ShouldSet()
    {
        var item = new FridgeItem();
        item.SetName("Eggs");
        Assert.Equal("Eggs", item.Name);
    }

    [Fact]
    public void SetName_WithNullOrEmpty_ShouldThrow()
    {
        var item = new FridgeItem();
        Assert.Throws<ArgumentNullException>(() => item.SetName(null));
        Assert.Throws<ArgumentNullException>(() => item.SetName(""));
    }
    
    [Fact]
    public void SetColor_WithValidColor_ShouldSet()
    {
        var item = new FridgeItem();
        item.SetColor("Green");
        Assert.Equal("Green", item.Color);
    }

    [Fact]
    public void SetColor_WithNullOrEmpty_ShouldThrow()
    {
        var item = new FridgeItem();
        Assert.Throws<ArgumentNullException>(() => item.SetColor(null));
        Assert.Throws<ArgumentNullException>(() => item.SetColor(""));
    }
    [Fact]
    public void SetExpiration_WithFutureDate_ShouldSet()
    {
        var item = new FridgeItem();
        var future = DateTime.UtcNow.AddDays(3);
        item.SetExpiration(future);
        Assert.Equal(future, item.Expiration);
    }

    [Fact]
    public void SetExpiration_WithPastDate_ShouldThrow()
    {
        var item = new FridgeItem();
        var past = DateTime.UtcNow.AddDays(-3);
        Assert.Throws<ArgumentException>(() => item.SetExpiration(past));
    }
    
    [Fact]
    public void SetWeight_WithValidValue_ShouldSet()
    {
        var item = new FridgeItem();
        item.SetWeight(2.3);
        Assert.Equal(2.3, item.Weight);
    }

    [Fact]
    public void SetWeight_WithInvalidValue_ShouldThrow()
    {
        var item = new FridgeItem();
        Assert.Throws<ArgumentException>(() => item.SetWeight(0));
        Assert.Throws<ArgumentException>(() => item.SetWeight(-1));
        Assert.Throws<ArgumentException>(() => item.SetWeight(double.NaN));
    }

    [Fact]
    public void SetIconName_WithValidValue_ShouldSet()
    {
        var item = new FridgeItem();
        item.SetIconName("icon-fridge");
        Assert.Equal("icon-fridge", item.IconName);
    }

    [Fact]
    public void SetIconName_WithNullOrEmpty_ShouldThrow()
    {
        var item = new FridgeItem();
        Assert.Throws<ArgumentNullException>(() => item.SetIconName(null));
        Assert.Throws<ArgumentNullException>(() => item.SetIconName(""));
    }
    
    [Fact]
    public void SetTimeToExpire_WithValidFutureDate_ShouldSetDays()
    {
        var item = new FridgeItem();
        var target = DateTime.Today.AddDays(10);
        item.SetTimeToExpire(target);
        Assert.Equal(10, item.TimeToExpireInDays);
    }
    
    [Fact]
    public void UpdateItemExpiration_ShouldUpdateToCorrectFutureDate()
    {
        var item = new FridgeItem();
        item.TimeToExpireInDays = 7;

        item.UpdateItemExpiration();

        var expected = DateTime.UtcNow.AddDays(7);
        var diffSeconds = Math.Abs((item.Expiration - expected).TotalSeconds);
        Assert.True(diffSeconds < 1);
    }

    [Fact]
    public void CurrecntExpirationDate_ShouldReturnDateUtcNowPlusDays()
    {
        var item = new FridgeItem();
        item.TimeToExpireInDays = 5;

        var expected = DateTime.UtcNow.AddDays(5);
        var diff = Math.Abs((item.CurrecntExpirationDate - expected).TotalSeconds);

        Assert.True(diff < 1);
    }
    
    [Fact]
    public void IsExpired_WithFutureDate_ShouldReturnFalse()
    {
        var item = new FridgeItem();
        item.Expiration = DateTime.UtcNow.AddDays(1);

        Assert.False(item.IsExpired);
    }

    [Fact]
    public void IsExpired_WithPastDate_ShouldReturnTrue()
    {
        var item = new FridgeItem();
        item.Expiration = DateTime.UtcNow.AddDays(-1);

        Assert.True(item.IsExpired);
    }
    
    [Fact]
    public void ShouldAddToShoppingList_WhenQuantityLess_ShouldReturnTrue()
    {
        var item = new FridgeItem { Quantity = 1, MinimunQuantity = 5 };
        Assert.True(item.ShouldAddToShoppingList);
    }

    [Fact]
    public void ShouldAddToShoppingList_WhenQuantityEnough_ShouldReturnFalse()
    {
        var item = new FridgeItem { Quantity = 5, MinimunQuantity = 5 };
        Assert.False(item.ShouldAddToShoppingList);
    }
    
    [Fact]
    public void GetPercentageExpired_WhenExpired_ShouldReturn100Percent()
    {
        var item = new FridgeItem();
        item.Expiration = DateTime.UtcNow.AddDays(-1);
        var result = item.GetPercentageExpired();
        Assert.Equal("100%", result);
    }

    [Fact]
    public void GetPercentageExpired_WithInclusion_ShouldReturnValidPercentage()
    {
        var item = new FridgeItem();
        item.Inclusion = DateTime.UtcNow.AddDays(-2);
        item.SetExpiration(DateTime.UtcNow.AddDays(2));
        var result = item.GetPercentageExpired();

        Assert.Contains("%", result);
    }


}