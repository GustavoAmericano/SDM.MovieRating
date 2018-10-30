using System.Collections.Generic;
using SDM.MovieRating.BE;

namespace SDM.MovieRating.BLL.Interfaces
{
    public interface IReviewerLogic
    {
        List<MovieReview> GetReviews(int reviewerId);
    }
}