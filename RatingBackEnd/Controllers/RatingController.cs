using System;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using RatingBackEnd.Repository;

namespace RatingBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingRepository ratingRepository;
        private ILogger _logger;
        TelemetryClient telemetryClient = new TelemetryClient();

        public RatingController(IRatingRepository ratingRepository, ILogger logger)
        {
            this.ratingRepository = ratingRepository;
            _logger = logger;
        }

        // GET api/rating/GetRatingForMovie/5
        [HttpGet]
        [Route("GetRatingForMovie/{movieId}")]
        public ActionResult<double> GetRatingForMovie(string movieId)
        {
            try
            {
                var result = ratingRepository.GetRatingForMovie(movieId);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                telemetryClient.TrackException(ex);

            }
            return new NotFoundResult();
        }
    }
}