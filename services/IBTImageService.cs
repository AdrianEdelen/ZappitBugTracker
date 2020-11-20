using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZappitBugTracker.services
{
    public interface IBTImageService
    {
        public byte[] EncodeImage(IFormFile image);
        public string DecodeImage(byte[] image, string fileName);
    }
}
