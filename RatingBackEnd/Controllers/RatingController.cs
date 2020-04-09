using Microsoft.AspNetCore.Mvc;
using RatingBackEnd.Repository;

namespace RatingBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingRepository ratingRepository;

        public RatingController(IRatingRepository ratingRepository)
        {
            this.ratingRepository = ratingRepository;
        }

        // GET api/rating/GetRatingForMovie/5
        [HttpGet]
        [Route("GetRatingForMovie/{movieId}")]
        public ActionResult<double> GetRatingForMovie(string movieId)
        {
            return ratingRepository.GetRatingForMovie(movieId);
        }
    }
}