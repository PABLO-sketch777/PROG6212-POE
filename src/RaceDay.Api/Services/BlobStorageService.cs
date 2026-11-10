using Azure.Storage.Blobs; using Azure.Storage.Blobs.Models;
namespace RaceDay.Api.Services;
public interface IBlobStorageService { Task<string> UploadAsync(IFormFile file,string folder,CancellationToken token); }
public class BlobStorageService(IConfiguration config,IWebHostEnvironment env):IBlobStorageService {
 public async Task<string> UploadAsync(IFormFile file,string folder,CancellationToken token){
  var name=$"{Guid.NewGuid():N}{Path.GetExtension(file.FileName).ToLowerInvariant()}"; var connection=config["AzureBlob:ConnectionString"]; var container=config["AzureBlob:Container"]??"raceday-media";
  if(!string.IsNullOrWhiteSpace(connection)) { var service=new BlobServiceClient(connection); var client=service.GetBlobContainerClient(container); await client.CreateIfNotExistsAsync(PublicAccessType.Blob,cancellationToken:token); var blob=client.GetBlobClient($"{folder}/{name}"); await using var stream=file.OpenReadStream(); await blob.UploadAsync(stream,new BlobUploadOptions{HttpHeaders=new BlobHttpHeaders{ContentType=file.ContentType}},token); return blob.Uri.ToString(); }
  var dir=Path.Combine(env.WebRootPath??Path.Combine(env.ContentRootPath,"wwwroot"),"uploads",folder); Directory.CreateDirectory(dir); var path=Path.Combine(dir,name); await using(var output=File.Create(path)){await file.CopyToAsync(output,token);} return $"/uploads/{folder}/{name}";
 }
}
