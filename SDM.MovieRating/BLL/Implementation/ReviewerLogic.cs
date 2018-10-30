using System.Collections.Generic;
using System.Linq;
using SDM.MovieRating.BE;
using SDM.MovieRating.BLL.Interfaces;
using SDM.MovieRating.DAL;

namespace SDM.MovieRating.BLL.Implementation
{
    public class ReviewerLogic : IReviewerLogic
    {
        private readonly IDataContext _context;

        public ReviewerLogic(IDataContext ctx)
        {
            _context = ctx;
        }

        public List<MovieReview> GetReviews(int reviewerId)
        {
            return _context.Reviewers[reviewerId];
        }

        public int GetAverageRating(int reviewerId)
        {
            int count = 0;
            int avg = 0;
            GetReviews(reviewerId).ForEach(rev =>
            {
                count++;
                avg += rev.Rating;
            });
            return avg / count;
        }

        public int GetTimesRatingGiven(int reviewerId, int rating)
        {
            return GetReviews(reviewerId).Count(x => x.Rating == rating);
        }
    }
}