using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Serilog;

namespace MoviePortal.Services
{
    public class MovieRatingService : IMovieRatingService
    {
        private HttpClient _httpClient;
        private ILogger _logger;

        public MovieRatingService(HttpClient httpClient, ILogger logger)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://ratingbackend.azurewebsites.net/api/rating/GetRatingForMovie/");
            _logger = logger;
        }
        public async Task<string> GetRating(string movieId)
        {
            _logger.Information($"Getting rating for movie {movieId}");
            var result = await _httpClient.GetAsync($"https://ratingbackend.azurewebsites.net/api/rating/GetRatingForMovie/{movieId}").ConfigureAwait(false);
            var content = await result.Content.ReadAsStringAsync();
            _logger.Information($"Rating service returned {result.StatusCode}");
            return content;
        }
    }
}
