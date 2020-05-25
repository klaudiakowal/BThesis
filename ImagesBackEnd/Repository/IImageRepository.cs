using Microsoft.AspNetCore.Mvc;

namespace ImagesBackEnd.Repository
{
    public interface IImageRepository
    {
        string GetFileImage(string Id);
    }
}