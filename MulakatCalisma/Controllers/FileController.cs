using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MulakatCalisma.CloudSystem;
using System.Configuration;

namespace MulakatCalisma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly ICloudinaryService _cloudinaryService;
        public FileController(ICloudinaryService cloudinaryService)
        {
            _cloudinaryService = cloudinaryService;
        }

        [HttpPost]
        [Authorize]
        [RequestSizeLimit(100_000_000)]
        public async Task<ActionResult<VideoUploadResult>> CreateVideo(IFormFile file)
        {
            //HttpClient client = new HttpClient();
            //client.Timeout = TimeSpan.FromSeconds(180);
            var result = await _cloudinaryService.AddVideoAsync(file);
            return Ok(result);
        }

        [HttpGet]
        public  ActionResult<string> GetFile(string publicId, string format = "mp4")
        {
            var result =  _cloudinaryService.GetVideoUrl(publicId, format);
            return Ok(result);
        }

    }
}
