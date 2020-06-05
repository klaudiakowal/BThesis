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
        private TelemetryClient telemetryClient;

        public ImagesController(IImageRepository imageRepository, ILogger logger, TelemetryClient telemetry)
        {
            this.imageRepository = imageRepository;
            _logger = logger;
            telemetryClient = telemetry;
        }

        // GET api/images/GetImageForMovie/5
        [HttpGet]
        [Route("GetImageForMovie/{movieId}")]
        public ActionResult GetImageForMovie(string movieId)
        {
            var success = false;
            var result = "";
            var startTime = DateTime.UtcNow;
            var timer = System.Diagnostics.Stopwatch.StartNew();
            try
            {
                result = imageRepository.GetFileImage(movieId);
                success = true;
            }
            catch (Exception ex)
            {
                success = false;
                telemetryClient.TrackException(ex);
                _logger.Error(ex.Message);
            }
            finally
            {
                timer.Stop();
                telemetryClient.TrackDependency("DependencyType", "myDependency", "myCall", startTime, timer.Elapsed, success);
            }

            return success ? (ActionResult)new OkObjectResult(result) : new NotFoundResult();
        }

    }
}