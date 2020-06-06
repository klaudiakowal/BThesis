using System;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
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
        private TelemetryClient telemetryClient;

        public RatingController(IRatingRepository ratingRepository, ILogger logger, TelemetryClient telemetry)
        {
            this.ratingRepository = ratingRepository;
            _logger = logger;
            telemetryClient = telemetry;
        }

        // GET api/rating/GetRatingForMovie/5
        [HttpGet]
        [Route("GetRatingForMovie/{movieId}")]
        public ActionResult<double> GetRatingForMovie(string movieId)
        {
            var operation = telemetryClient.StartOperation<DependencyTelemetry>("GetRatingForMovie-operation");
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

            telemetryClient.StopOperation(operation);
            return new NotFoundResult();
        }
    }
}