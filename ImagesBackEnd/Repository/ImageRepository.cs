using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace ImagesBackEnd.Repository
{
    public class ImageRepository : IImageRepository
    {
        public FileStreamResult GetFileImage(string Id)
        {
            var path = string.Concat(AppDomain.CurrentDomain.BaseDirectory, "/Images", "/", Id + ".jpg");
            return new FileStreamResult(new FileStream(path, FileMode.Open), "image/jpeg");
        }
    }
}