using System.Threading.Tasks;

namespace MoviePortal.Services
{
    public interface IMovieRatingService
    {
        Task<string> GetRating(string movieId);
    }
}