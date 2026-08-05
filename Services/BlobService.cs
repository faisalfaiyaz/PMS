using Azure.Storage.Blobs;

namespace ProductManagementSystem.Services;

public class BlobService
{
    private readonly BlobContainerClient _blobContainerClient;
    private readonly BlobServiceClient _blobServiceClient;
    private readonly IConfiguration _configuration;
    public BlobService(IConfiguration configuration)
    {
        _configuration = configuration;
        _blobServiceClient = new BlobServiceClient(_configuration["AzureBlob:ConnectionString"]);
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(_configuration["AzureBlob:ContainerName"]);
    }


    public async Task<string> UploadFileAsync(IFormFile file)
    {
        await _blobContainerClient.CreateIfNotExistsAsync();
        var blobName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        BlobClient blobClient = _blobContainerClient.GetBlobClient(blobName);

        using var stream = file.OpenReadStream();
        await blobClient.UploadAsync(stream, overwrite: true);

        return blobClient.Uri.ToString();
    }
}

