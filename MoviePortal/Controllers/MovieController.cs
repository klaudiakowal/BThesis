using Microsoft.AspNetCore.Mvc;
using MoviePortal.Services;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;

namespace MoviePortal.Controllers
{
    [Route("api/[controller]")]
    public class MovieController : Controller
    {

        private static string[] Names = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IMovieRatingService ratingService;
        private readonly IMovieImageService imageService;
        private ILogger _logger;
        private TelemetryClient telemetryClient;

        public MovieController(IMovieRatingService ratingService, IMovieImageService imageService, ILogger logger)
        {
            this.ratingService = ratingService;
            this.imageService = imageService;
            _logger = logger;
        }
      
        [HttpGet("[action]")]
        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            var operation = telemetryClient.StartOperation<DependencyTelemetry>("Get all movie operation");
            var rng = new Random();
            var result = Enumerable.Range(1, 5).Select(index => new Movie
            {
                ReleaseDate = DateTime.Now.AddDays(index).ToString("d"),
                Rating = ratingService.GetRating(rng.Next(1, 7).ToString()).Result,
                Name = Names[rng.Next(Names.Length)],
                Image = imageService.GetImageForMovie(rng.Next(1, 7)).Result
            });
            telemetryClient.StopOperation<DependencyTelemetry>(operation);
            return result;
        }

       
        public class Movie
        {
            public string ReleaseDate { get; set; }
            public string Rating { get; set; }
            public string Name { get; set; }

            public string Image { get; set; }
        }
    }
}
