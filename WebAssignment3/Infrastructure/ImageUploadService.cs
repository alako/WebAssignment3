using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebAssignment3.Models;

namespace WebAssignment3.Infrastructure
{
    public static class ImageUploadService
    {
        public static async Task<ESImage> FormFileToESIMage(IFormFile formFile)
        {
            ESImage image = new ESImage
            {
                ImageMimeType = formFile.ContentType,
            };

            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
                image.ImageData = memoryStream.ToArray();
            }

            return image;
        }

        public static string ToESImageBase64String(ESImage image)
        {
            return "data:" + image.ImageMimeType + ";base64," + Convert.ToBase64String(image.ImageData, 0, image.ImageData.Length); 
        }
    }
}
