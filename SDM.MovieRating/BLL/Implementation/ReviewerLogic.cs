using System.Collections.Generic;
using System.ComponentModel.Design;
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

        /// <summary>
        /// Returns a list of movieReviews, related to the reviewerID. 
        /// </summary>
        /// <param name="reviewerId"></param>
        /// <returns>A list of movieReviews, Empty list if none were found</returns>
        public List<MovieReview> GetReviews(int reviewerId)
        {
            if (!_context.Reviewers.ContainsKey(reviewerId)) return new List<MovieReview>();
            return _context.Reviewers[reviewerId];
        }
        /// <summary>
        /// Returns the average rating from the reviewer.
        /// </summary>
        /// <param name="reviewerId"></param>
        /// <returns>The reviewers average rating, 0 if none were found</returns>
        public int GetAverageRating(int reviewerId)
        {
            if (!_context.Reviewers.ContainsKey(reviewerId)) return 0;
            int count = 0;
            int avg = 0;
            GetReviews(reviewerId).ForEach(rev =>
            {
                count++;
                avg += rev.Rating;
            });
            return avg / count;
        }

        /// <summary>
        /// Gets the times a reviewer has given a specific rating
        /// </summary>
        /// <param name="reviewerId"></param>
        /// <param name="rating"></param>
        /// <returns>Times rating N was given</returns>
        public int GetTimesRatingGiven(int reviewerId, int rating)
        {
            return GetReviews(reviewerId).Count(x => x.Rating == rating);
        }

        /// <summary>
        /// Gets a list of reviewer(s) whom has reviewed the most movies.
        /// </summary>
        /// <returns>List of top-reviewer(s)</returns>
        public List<int> GetReviewerWithMostReviewes()
        {
            var orderedDic = _context.Reviewers
                .OrderByDescending(x => x.Value.Count);
            return orderedDic
                .Where(x => x.Value.Count == orderedDic.First().Value.Count)
                .Select(kv => kv.Key).ToList();
        }

        /// <summary>
        /// Gets N amount of MovieReview, related to the reviewerId
        /// </summary>
        /// <param name="reviewerId"></param>
        /// <param name="amount"></param>
        /// <returns>A list of N MovieReviews</returns>
        public List<MovieReview> GetReviewedMovies(int reviewerId, int amount)
        {
            if (!_context.Reviewers.ContainsKey(reviewerId)) return null;
            var list =  _context.Reviewers[reviewerId]
                .OrderByDescending(rev => rev.Rating)
                .ThenBy(rev => rev.Date)
                .Take(amount)
                .ToList();
            return list;
        }
    }
}