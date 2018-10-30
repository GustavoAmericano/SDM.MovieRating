using System.Collections.Generic;
using System.Linq;
using SDM.MovieRating.BE;

namespace SDM.MovieRating.BLL.Interfaces
{
    public interface IMovieLogic
    {
        List<MovieReview> GetReviews(int movieId);
        int GetAverageRating(int movieId);
        int GetTimesRatingGiven(int movieId, int rating);
        List<int> GetTopRatedMovies();
        List<int> GetTopMovies(int amount);
    }
}