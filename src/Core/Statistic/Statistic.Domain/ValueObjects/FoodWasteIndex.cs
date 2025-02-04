namespace Statistic.Domain.ValueObjects;

public class FoodWasteIndex
{
    private float NationalIndex { get; set; }
    private float UserIndex { get; set; }   
    
    public FoodWasteIndex SetNationalIndexPerMonth(float nationalIndexPerYear)
    {
        NationalIndex = nationalIndexPerYear/12;
        return this;
    }

    public FoodWasteIndex SetInitiaUserIndexPerMonth(float initiaUserIndexPerYear)
    {
        UserIndex = initiaUserIndexPerYear/12;
        return this;
    }

    public FoodWasteIndex  CalculateMonthUserIndex(IEnumerable<float> itemWeights)
    {
        UserIndex = (100*itemWeights.Sum())/NationalIndex;
        return this;
    }

    public float GetIndex()
    {
        return UserIndex;
    }

    
}