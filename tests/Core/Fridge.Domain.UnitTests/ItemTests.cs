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
            "someIcon",
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
    public void SetExpiration_WhenExpirationLessThanToday_ThrowsArgumentException(){
    
        var item = GetValidItem;
        var act = () => item.SetExpiration(DateTime.UtcNow.AddDays(-1));
        Assert.Throws<ArgumentException>(act);
    }
    
    [Fact]
    public void SetExpiration_WhenExpirationMoreThanToday_DoesNotThrow(){
    
        var item = GetValidItem;
        var date = DateTime.UtcNow.AddDays(1);
        item.SetExpiration(date);
        Assert.Equal(date,item.Expiration);
    }

    [Fact]
    public void GetPercentageExpired_ShouldReturn_stringWithPercentageExpired()
    {
        var item  = GetValidItem;
        var percentage = item.GetPercentageExpired();
        Assert.NotNull(percentage);
        Assert.NotEmpty(percentage);
        Assert.Contains("%",percentage);
        
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
}