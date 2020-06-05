using System;
using ImagesBackEnd.Repository;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace ImagesBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;
        private readonly ILogger _logger;

        public ImagesController(IImageRepository imageRepository, ILogger logger)
        {
            this.imageRepository = imageRepository;
            _logger = logger;
        }

        // GET api/images/GetImageForMovie/5
        [HttpGet]
        [Route("GetImageForMovie/{movieId}")]
        public ActionResult GetImageForMovie(string movieId)
        {
            try
            {
                var result = imageRepository.GetFileImage(movieId);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }

            return new NotFoundResult();
        }

    }
}