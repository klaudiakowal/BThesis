using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MoviePortal.Services
{
    public class MovieImageService: IMovieImageService
    {
        private HttpClient _httpClient;
        public MovieImageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetImageForMovie(int movieId)
        {
            var result = await _httpClient.GetAsync($"https://imagesbackend.azurewebsites.net/api/images/GetImageForMovie/{movieId}");
            var content = await result.Content.ReadAsStringAsync();
            return content;
        }
    }
}
