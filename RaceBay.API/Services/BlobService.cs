using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace RaceBay.API.Services;

public class BlobService
{
    private readonly string _connectionString;

    public BlobService(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("AzureBlobStorage")!;
    }

    public async Task<string> UploadFileAsync(IFormFile file, string containerName)
    {
        var blobServiceClient = new BlobServiceClient(_connectionString);
        var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
        
        // Create container if it doesn't exist (set to public for easy viewing in this project)
        await blobContainerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

        var blobClient = blobContainerClient.GetBlobClient(Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));
        
        using var stream = file.OpenReadStream();
        await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = file.ContentType });

        return blobClient.Uri.ToString();
    }
}
