using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MoviePortal.Services
{
    public class MovieRatingService : IMovieRatingService
    {
        private HttpClient _httpClient;
        public MovieRatingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://ratingbackend.azurewebsites.net/api/rating/GetRatingForMovie/");

        }
        public async Task<string> GetRating(string movieId)
        {
            var response = await _httpClient.GetAsync($"https://ratingbackend.azurewebsites.net/api/rating/GetRatingForMovie/{movieId}").ConfigureAwait(false);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
