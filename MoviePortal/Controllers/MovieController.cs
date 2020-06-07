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
            "Inception", "Bad Boys", "Avatar", "Dunkirk", "Titanic", "Interstellar", "The Dark Knight", "The Godfather", "The Silence of the Lambs", "The Green Mile"
        };

        private readonly IMovieRatingService ratingService;
        private readonly IMovieImageService imageService;
        private ILogger _logger;
        private TelemetryClient telemetryClient;

        public MovieController(IMovieRatingService ratingService, IMovieImageService imageService, ILogger logger, TelemetryClient telemetry)
        {
            this.ratingService = ratingService;
            this.imageService = imageService;
            _logger = logger;
            telemetryClient = telemetry;
        }
      
        [HttpGet("[action]")]
        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            var operation = telemetryClient.StartOperation<DependencyTelemetry>("GetAllMovies-operation");

            var listOfTasks = new List<Task<Movie>>();
            for(var i = 0; i <5;i++)
            {
                listOfTasks.Add(GetMovieAsync(i));
            }
            var results = await Task.WhenAll(listOfTasks);
            telemetryClient.StopOperation<DependencyTelemetry>(operation);
            return results;
        }

        public async Task<Movie> GetMovieAsync(int index)
        {
            var rng = new Random();
            var result = new Movie
            {
                ReleaseDate = DateTime.Now.AddDays(index).ToString("d"),
                Rating = await ratingService.GetRating(rng.Next(1, 7).ToString()),
                Name = Names[rng.Next(Names.Length)],
                Image = await imageService.GetImageForMovie(rng.Next(1, 7))
            };
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
