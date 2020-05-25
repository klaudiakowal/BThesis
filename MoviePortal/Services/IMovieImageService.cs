using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MoviePortal.Services
{
    public interface IMovieImageService
    {
        Task<string> GetImageForMovie(int movieId);
    }
}