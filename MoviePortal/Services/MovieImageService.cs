using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Serilog;

namespace MoviePortal.Services
{
    public class MovieImageService: IMovieImageService
    {
        private HttpClient _httpClient;
        TelemetryClient telemetryClient = new TelemetryClient();
        private readonly ILogger _logger;
        public MovieImageService(HttpClient httpClient, ILogger logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<string> GetImageForMovie(int movieId)
        {
            _logger.Information($"Getting image for movie {movieId}");
            var result = await _httpClient.GetAsync($"https://imagesbackend.azurewebsites.net/api/images/GetImageForMovie/{movieId}");
            var content = await result.Content.ReadAsStringAsync();
            _logger.Information($"Image service returned {result.StatusCode}");
            return content;
        }
    }
}
