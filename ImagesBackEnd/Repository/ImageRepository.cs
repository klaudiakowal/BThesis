using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace ImagesBackEnd.Repository
{
    public class ImageRepository : IImageRepository
    {
        public string GetFileImage(string Id)
        {
            var path = string.Concat(AppDomain.CurrentDomain.BaseDirectory, "/Images", "/", Id + ".jpg");
            var base64String = Convert.ToBase64String(File.ReadAllBytes(path));
            return "data:image/jpg;base64," + base64String;
        }
    }
}