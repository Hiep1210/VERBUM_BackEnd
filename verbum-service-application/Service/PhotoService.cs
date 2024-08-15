using verbum_service_domain.Models;
using Microsoft.AspNetCore.Http;

namespace verbum_service_application.Service
{
    public abstract class PhotoService
    {
        public abstract Image UploadImage(IFormFile inputFile);
    }
}
