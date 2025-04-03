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
            ,1,
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
        Assert.Equal(weight,item.Weight);
    }
}