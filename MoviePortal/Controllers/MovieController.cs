using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using MoviePortal.Services;

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

        public MovieController(IMovieRatingService ratingService, IMovieImageService imageService)
        {
            this.ratingService = ratingService;
            this.imageService = imageService;
        }
      
        [HttpGet("[action]")]
        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            var rng = new Random();
          
            return Enumerable.Range(1, 5).Select(index => new Movie
            {
                ReleaseDate = DateTime.Now.AddDays(index).ToString("d"),
                Rating = ratingService.GetRating(rng.Next(1,7).ToString()).Result,
                Name = Names[rng.Next(Names.Length)],
                Image = imageService.GetImageForMovie(rng.Next(1, 7)).Result
            });
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
