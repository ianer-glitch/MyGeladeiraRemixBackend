namespace Fridge.Domain.Ports.FileAdapter;

public interface IFileAdapterResult
{
    public string Name { get; set; } 
    public int Size { get; set; }
    public string? Link { get; set; }
    public bool Success { get; set; }
}
