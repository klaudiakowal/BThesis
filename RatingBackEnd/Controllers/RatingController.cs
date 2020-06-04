using System;
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
            catch (Exception exception)
            {
                _logger.Error(exception.Message);
            }
            return new NotFoundResult();
        }
    }
}