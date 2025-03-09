using Statistic.Domain.FoodWasteIndexes.ValueObjects;

namespace Statistic.Domain.UnitTests;

public class FoodWasteIndexTests
{
    [Fact]
    public void CalculateUserMonthIndex_whenIsValid_shouldReturnIndex()
    {
        const float nationalYearIndex = 60;

        var itemWeightsInGram = new List<double>()
        {
            500,500
        };

        const int result = 20;
        
        var userIndex = new FoodWasteIndex()
            .SetInitial(nationalYearIndex)
            .PerMonth()
            .Calculate(itemWeightsInGram)
            .GetCurrent();

        
        
        Assert.Equal(result, userIndex);
    }
}