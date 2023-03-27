using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace MulakatCalisma.CloudSystem
{
    public interface ICloudinaryService
    {
        Task<VideoUploadResult> AddVideoAsync(IFormFile file);
       string GetVideoUrl( string publicId, string format = "mp4");
     
    }
}
