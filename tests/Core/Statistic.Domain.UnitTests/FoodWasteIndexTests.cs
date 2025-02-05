using Statistic.Domain.ValueObjects;

namespace Statistic.Domain.UnitTests;

public class FoodWasteIndexTests
{
    [Fact]
    public void CalculateUserMonthIndex_whenIsValid_shouldReturn()
    {
        float nationalYearIndex = 60;
        float userOldIndex = 60;

        var itemWeights = new List<double>()
        {
            0.1,0.2,0.3
        };

        var result = 12;

        var index = new FoodWasteIndex()
            .SetNationalIndexPerMonth(nationalYearIndex)
            .SetInitiaUserIndexPerMonth(userOldIndex)
            .CalculateMonthUserIndex(itemWeights)
            .GetIndex();
        
        Assert.Equal(result, index);








    }
}