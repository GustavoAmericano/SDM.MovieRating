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
    }
}