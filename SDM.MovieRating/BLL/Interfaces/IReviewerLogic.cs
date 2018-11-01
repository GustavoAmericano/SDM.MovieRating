using System.Collections.Generic;
using SDM.MovieRating.BE;

namespace SDM.MovieRating.BLL.Interfaces
{
    public interface IReviewerLogic
    {
        List<MovieReview> GetReviews(int reviewerId);
        int GetAverageRating(int reviewerId);
        int GetTimesRatingGiven(int reviewerId, int rating);
        List<int> GetReviewerWithMostReviewes();
        List<MovieReview> GetReviewedMovies(int reviewerId, int amount);
    }
}