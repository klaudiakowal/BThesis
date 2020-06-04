using System;
using System.Collections.Generic;

namespace RatingBackEnd.Repository
{
    public class RatingRepository : IRatingRepository
    {
        private readonly IDictionary<string, double> ratings = new Dictionary<string, double>
        {
            {"1", 9.1},
            {"2", 2.1},
            {"3", 7.1},
            {"4", 5.3},
            {"5", 9.1},
            {"6", 3.3},
            {"7", 6.1}
        };

        public double GetRatingForMovie(string movieId)
        {
            if (!ratings.ContainsKey(movieId)) throw new Exception("Rating for this movie not exist");
                return ratings[movieId];
        }
    }
}