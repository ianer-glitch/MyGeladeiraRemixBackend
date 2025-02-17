using Microsoft.AspNetCore.Http;

namespace Fridge.Domain.Ports.FileAdapter;

public interface IFileAdapter<TFile> where TFile : IFileAdapterResult
{
    public Task<TFile> GetFileAsync(string objectName);

    public Task<TFile> UploadAsync(IFormFile file);
}