using Extensions;

namespace ExtensionUnitTests;

public class TypesTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]    
    public void StringIsNullOrEmpty_ShouldReturnTrue_WhenStringIsNull(string value)
    {
        var isNull = value.IsNullOrEmpty();
        Assert.True(isNull);    
    }

    [Fact]
    public void StringIsNullOrEmpty_ShouldReturnFalse_WhenStringIsNotEmpty()
    {
        var fakeString = "some data";
        Assert.False(fakeString.IsNullOrEmpty());
    }

    [Theory]
    [InlineData(null)]

    public void IsEmpty_ShouldReturnTrue_WhenArrayIsNull(IEnumerable<object>? value)
    {
        Assert.True(value.IsNullOrEmpty()); 
    }
}