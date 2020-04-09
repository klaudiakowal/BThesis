using Microsoft.AspNetCore.Mvc;

namespace ImagesBackEnd.Repository
{
    public interface IImageRepository
    {
        FileStreamResult GetFileImage(string Id);
    }
}