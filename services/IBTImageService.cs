using Microsoft.AspNetCore.Http;

namespace ZappitBugTracker.services
{
    public interface IBTImageService
    {
        public byte[] EncodeImage(IFormFile image);
        public string DecodeImage(byte[] image, string fileName);
    }
}
