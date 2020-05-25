using ImagesBackEnd.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ImagesBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        // GET api/images/GetImageForMovie/5
        [HttpGet]
        [Route("GetImageForMovie/{movieId}")]
        public ActionResult GetImageForMovie(string movieId)
        {
            return new OkObjectResult(imageRepository.GetFileImage(movieId));
        }
      
    }
}