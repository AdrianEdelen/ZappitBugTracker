﻿using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace ZappitBugTracker.services
{
    public class BTImageService : IBTImageService
    {
        public byte[] EncodeImage(IFormFile image)
        {
            //turns image into a storable format
            var ms = new MemoryStream();
            //copy file to memorystream
            image.CopyTo(ms);
            //image prop now equals byte array
            var output = ms.ToArray();
            ms.Close();
            ms.Dispose();
            return output;
        }

        public string DecodeImage(byte[] image, string fileName)
        {
            if (image == null || fileName == null)
            {
                return "";
            }
            else
            {
                var binary = Convert.ToBase64String(image);
                var ext = Path.GetExtension(fileName);
                return $"data:image/{ext};base64,{binary}";
            }

        }
    }
}
