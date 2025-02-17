﻿using Extensions;
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
    private readonly string _bucketName;
    private readonly IServiceProvider _serviceProvider;
    
    public FileAdapter(IConfiguration configuration , IServiceProvider serviceProvider)
    {
        
        _bucketName = configuration.GetSection("MINIO_BUCKET_NAME").Value ?? throw new InvalidOperationException("Bucket name config is missing");
        
        var endpoint = configuration.GetSection("MINIO_ENDPOINT").Value;
        var accessKey = configuration.GetSection("MINIO_ACCESS_KEY").Value;
        var secretKey = configuration.GetSection("MINIO_SECRET_KEY").Value;;
        var secure = bool.Parse(configuration.GetSection("MINIO_USE_SSL").Value);

        
        _minioClient = (MinioClient?)new MinioClient()
            .WithEndpoint(endpoint)
            .WithCredentials(accessKey, secretKey)
            .WithSSL(secure)
            .Build() ?? throw new InvalidOperationException();    
        
        _serviceProvider = serviceProvider; 
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

            var presignedUrl = await _minioClient.PresignedGetObjectAsync(args).ConfigureAwait(false);

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

            await _minioClient.PutObjectAsync(putObjectArgs);

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