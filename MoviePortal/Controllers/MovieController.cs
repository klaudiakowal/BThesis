using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace MoviePortal.Controllers
{
    [Route("api/[controller]")]
    public class MovieController : Controller
    {
        private static string[] Names = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly HttpClient httpClient = new HttpClient();

        public MovieController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:5003/api/rating/GetRatingForMovie/");

        }
      
        [HttpGet("[action]")]
        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            var rng = new Random();
          
            return Enumerable.Range(1, 5).Select(index => new Movie
            {
                ReleaseDate = DateTime.Now.AddDays(index).ToString("d"),
                Rating = GetRating(rng.Next(1,7).ToString()).Result,
                Name = Names[rng.Next(Names.Length)],
                Image = "https://localhost:5001/api/images/GetImageForMovie/"+rng.Next(1,7)
            });
        }

        private async Task<string> GetRating(string movieId)
        {
            var response = await httpClient.GetAsync(movieId).ConfigureAwait(false);
            return await response.Content.ReadAsStringAsync();
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
