using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ZappitBugTracker.services
{
    public interface IBTAttachmentService
    {
        public Task<byte[]> ConvertFileToByteArrayAsync(IFormFile file);

        public string ConvertByteArrayToFile(byte[] fileData, string extension);

        public string GetFileIcon(string file);

        public string FormatFileSize(long bytes);

    }
}
