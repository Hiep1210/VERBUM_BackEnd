using verbum_service.Models;

namespace verbum_service.Service
{
    public abstract class PhotoService
    {
        public abstract Image UploadImage(IFormFile inputFile);
    }
}
