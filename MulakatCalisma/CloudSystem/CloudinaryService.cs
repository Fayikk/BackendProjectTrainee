using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using MulakatCalisma.Services.Abstract;
using MulakatCalisma.Entity;
using MulakatCalisma.Context;

namespace MulakatCalisma.CloudSystem
{
    public class CloudinaryService : ICloudinaryService
    {
        private Cloudinary cloudinary;
        private readonly IAuthService _authService;
        private readonly ApplicationDbContext _context;
        
        public CloudinaryService(IAuthService authService,ApplicationDbContext context)
        {
            Account account = new Account(
                "deae8kgzs",
                "876617553444522",
                "WQ6kW1MOsyLLB4oJfflcn8I8Mp4"
            );
            cloudinary = new Cloudinary(account);
            _authService = authService;
            _context = context;
        }

     
        public async Task<VideoUploadResult> AddVideoAsync(IFormFile file)
        {
            var user = _authService.GetUserId();
            Teacher teacher = new Teacher();
            TimeSpan time = new TimeSpan();
            var uploadResult = new VideoUploadResult();
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new VideoUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Folder = "da-net7",

                };
                cloudinary.Api.Client.Timeout = TimeSpan.FromMinutes(3);
                uploadResult = await cloudinary.UploadAsync(uploadParams);
                string videoUrl = cloudinary.Api.UrlVideoUp.BuildUrl( uploadResult.PublicId);
                teacher.PublicId = uploadResult.PublicId;
                teacher.Url = videoUrl;
                teacher.Userıd = user;
                _context.Teachers.Add(teacher);
                await _context.SaveChangesAsync();
                var result = uploadResult;
            }

            return uploadResult;
        }

        public string GetVideoUrl(string publicId, string format = "mp4")
        {
                Account account = new Account(
               "deae8kgzs",
               "876617553444522",
               "WQ6kW1MOsyLLB4oJfflcn8I8Mp4"
            );

            // Cloudinary hesabınızla bağlantı kurun
            Cloudinary cloudinary = new Cloudinary(account);

            // Cloudinary'de depolanan videoya erişmek için PublicID'yi kullanın
            //string publicID = "sample_video";

            // Videoyu görüntülemek için Cloudinary'den URL'yi alın
            string videoUrl = cloudinary.Api.UrlVideoUp.BuildUrl("da-net7/" + publicId);
            return videoUrl;
        // Tarayıcınızda videoyu açın
        //System.Diagnostics.Process.Start(videoUrl);
        }


    }
}
