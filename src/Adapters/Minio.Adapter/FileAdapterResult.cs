using Fridge.Domain.Ports.FileAdapter;

namespace Minio.Adapter;

public class FileAdapterResult : IFileAdapterResult
{
    public string Name { get; set; }
    public int Size { get; set; }
    public string? Link { get; set; }
    public bool Success { get; set; }
}