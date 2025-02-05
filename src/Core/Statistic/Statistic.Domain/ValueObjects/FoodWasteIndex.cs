namespace Statistic.Domain.ValueObjects;

public class FoodWasteIndex
{
    private double NationalIndex { get; set; }
    private double UserIndex { get; set; }   
    
    public FoodWasteIndex SetNationalIndexPerMonth(double nationalIndexPerYear)
    {
        NationalIndex = nationalIndexPerYear/12;
        return this;
    }

    public FoodWasteIndex SetInitiaUserIndexPerMonth(double initiaUserIndexPerYear)
    {
        UserIndex = initiaUserIndexPerYear/12;
        return this;
    }

    public FoodWasteIndex  CalculateMonthUserIndex(IEnumerable<double> itemWeights)
    {
        var tempIndex = (100 * itemWeights.Sum()) / NationalIndex; 
        
        UserIndex = double.Round(tempIndex, 2);
        return this;
    }

    public double GetIndex()
    {
        return UserIndex;
    }

    
}