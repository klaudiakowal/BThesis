using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Serilog;

namespace MoviePortal.Services
{
    public class MovieImageService : IMovieImageService
    {
        private HttpClient _httpClient;
        private TelemetryClient telemetryClient;
        private readonly ILogger _logger;
        public MovieImageService(HttpClient httpClient, ILogger logger, TelemetryClient telemetry)
        {
            _httpClient = httpClient;
            _logger = logger;
            telemetryClient = telemetry;
        }

        public async Task<string> GetImageForMovie(int movieId)
        {
            var operation = telemetryClient.StartOperation<DependencyTelemetry>("GetImageForMovie-operation");
            telemetryClient.TrackEvent("GetImageForMovie");
            telemetryClient.TrackTrace("GetImageForMovie");
            _logger.Information($"Getting image for movie {movieId}");
            var result =
                await _httpClient.GetAsync(
                    $"https://imagesbackend.azurewebsites.net/api/images/GetImageForMovie/{movieId}");
            var content = await result.Content.ReadAsStringAsync();
            _logger.Information($"Image service returned {result.StatusCode}");
            if (result.StatusCode != HttpStatusCode.Accepted)
            {
                _logger.Error("Getting image for movie failed");
            }

            telemetryClient.StopOperation(operation);
            return content;
        }
    }
}
