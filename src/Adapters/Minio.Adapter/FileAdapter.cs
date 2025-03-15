using Extensions;
using Fridge.Domain.Ports.FileAdapter;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio.DataModel.Args;

namespace Minio.Adapter;

public class FileAdapter<TFile> : IFileAdapter<TFile>    
    where TFile:  IFileAdapterResult 
{
    private readonly MinioClient _minioClient;
    private readonly MinioClient _minioClienteExternal;
    private readonly string _bucketName;
    private readonly IServiceProvider _serviceProvider;
    
    public FileAdapter(IConfiguration configuration , IServiceProvider serviceProvider)
    {
        
        
        
        var endpointExternal = configuration.GetSection("MINIO_ENDPOINT_EXTERNAL").Value;
        var portExternal = configuration.GetSection("MINIO_ENDPOINT_PORT_EXTERNAL").Value;
        
        var endpoint = configuration.GetSection("MINIO_ENDPOINT").Value;
        var port = configuration.GetSection("MINIO_ENDPOINT_PORT").Value;
        var accessKey = configuration.GetSection("MINIO_ACCESS_KEY").Value;
        var secretKey = configuration.GetSection("MINIO_SECRET_KEY").Value;;
        var secure = bool.Parse(configuration.GetSection("MINIO_USE_SSL").Value);

        
        _minioClient = (MinioClient?)new MinioClient()
            .WithEndpoint(endpoint,int.Parse(port))
            .WithCredentials(accessKey, secretKey)
            .WithSSL(secure)
            .Build() ?? throw new InvalidOperationException();   
        
        _minioClienteExternal = (MinioClient?)new MinioClient()
            .WithEndpoint(endpointExternal,int.Parse(portExternal))
            .WithCredentials(accessKey, secretKey)
            .WithSSL(secure)
            .Build() ?? throw new InvalidOperationException();   
        
        _bucketName = configuration.GetSection("MINIO_BUCKET_NAME").Value ?? throw new InvalidOperationException("Bucket name config is missing");

        EnsureBucketExistsAsync().GetAwaiter().GetResult();
        
        _serviceProvider = serviceProvider; 
    }

    private async Task EnsureBucketExistsAsync()
    {
        var bucketExists = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(_bucketName));

        if (!bucketExists)
        {
            await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(_bucketName));
        }
    }
    
    public async Task<TFile> GetFileAsync(string objectName)
    {
        if (string.IsNullOrWhiteSpace(objectName))
            throw new ArgumentException("Object name cannot be null or empty.", nameof(objectName));

        try
        {
            var args = new PresignedGetObjectArgs()
                .WithObject(objectName)
                .WithBucket(_bucketName)
                .WithExpiry(900); // 15 minutos
            

            var presignedUrl = await _minioClienteExternal.PresignedGetObjectAsync(args).ConfigureAwait(false);

            var result = _serviceProvider.GetRequiredService<TFile>();
            result.Link = presignedUrl;
            result.Success = true;
            return result;  
            
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error getting file {objectName}: {ex.Message}", ex);
        }
    }
    
    public async Task<TFile> UploadAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("File cannot be null or empty.", nameof(file));

        try
        {
            var objectName = $"{Path.GetFileNameWithoutExtension(file.FileName)}_{Guid.NewGuid()}{Path.GetExtension(file.FileName).ToLower()}";

            using var stream = file.OpenReadStream();
            
            

            var putObjectArgs = new PutObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(objectName)
                .WithStreamData(stream)
                .WithObjectSize(stream.Length)
                .WithContentType(file.ContentType);

            var response = await _minioClient.PutObjectAsync(putObjectArgs);
            
            
            var result = _serviceProvider.GetRequiredService<TFile>();
            result.Name = objectName;
            result.Size = (int)stream.Length;
            result.Success = true;

            return result;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error uploading file: {ex.Message}", ex);
        }
    }
}