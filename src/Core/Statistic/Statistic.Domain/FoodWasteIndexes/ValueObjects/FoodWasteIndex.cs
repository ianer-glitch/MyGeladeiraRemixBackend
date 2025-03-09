namespace Statistic.Domain.FoodWasteIndexes.ValueObjects;

public class FoodWasteIndex
{
    private double Initial { get; set; }
    private double Current { get; set; }   
    
    public FoodWasteIndex SetInitial(double initialIndex)
    {
        Initial = initialIndex;
        return this;
    }

    public FoodWasteIndex PerMonth()
    {
        Initial = Initial/12;
        Current = Current / 12;
        
        return this;
    }

    public FoodWasteIndex  Calculate(IEnumerable<double> itemWeightsInGram)
    {
        var itemWeightsInKg = (itemWeightsInGram.Sum())/1000;
        
        var tempIndex = (100 * itemWeightsInKg) / Initial; 
        
        Current = double.Round(tempIndex, 2);
        return this;
    }

    public double GetCurrent()
    {
        return Current;
    }

    
}