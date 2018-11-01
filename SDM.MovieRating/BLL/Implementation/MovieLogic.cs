using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using SDM.MovieRating.BE;
using SDM.MovieRating.BLL.Interfaces;
using SDM.MovieRating.DAL;

namespace SDM.MovieRating.BLL.Implementation
{
    public class MovieLogic : IMovieLogic
    {
        private readonly IDataContext _context;

        public MovieLogic(IDataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns a list of all reviews for a movie
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns>A list of reviews, or an empty list if none were found</returns>
        public List<MovieReview> GetReviews(int movieId)
        {
            if (!_context.Movies.ContainsKey(movieId)) return new List<MovieReview>();
            return _context.Movies[movieId];
        }
        /// <summary>
        /// Returns the average rating of a movie
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns>the average rating, 0 if none were found</returns>
        public int GetAverageRating(int movieId)
        {
            if (!_context.Movies.ContainsKey(movieId)) return 0;

            int avg = 0;
            int count = 0;
            GetReviews(movieId).ForEach(mov =>
            {
                count++;
                avg += mov.Rating;
            });

            return avg / count;
        }

        /// <summary>
        /// Returns the amount of times a movie has received a specific rating.
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="rating"></param>
        /// <returns>The amount of times the movie was rated N, 0 if none were found</returns>
        public int GetTimesRatingGiven(int movieId, int rating)
        {
            if (!_context.Movies.ContainsKey(movieId)) return 0;
            return GetReviews(movieId).Count(mov => mov.Rating == rating);
        }

        /// <summary>
        /// Retuns a list of movieId's based on the top rated movie(s).
        /// </summary>
        /// <returns>A list of movieId's</returns>
        public List<int> GetTopRatedMovies()
        {
            // Order by single kvp's values which are 5 only
            // Make into new dictionary and remove all ratings which are not 5.
            var orderedDic = _context.Movies
                .OrderByDescending(x => x.Value.Count(rev => rev.Rating == 5))
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Count(x => x.Rating == 5));
            // Return all keys, which value is equal to the top in the list
            // (In case there's a shared top)
            return orderedDic
                .Where(kvp => kvp.Value == orderedDic.Values.First())
                .Select(kvp => kvp.Key).ToList();
        }

        /// <summary>
        /// Returns a list of N top-rated movies, based on average rating descending, and date ascending if two have same rating.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns>A list of movieId's</returns>
        public List<int> GetTopMovies(int amount)
        {
            return _context.Movies
                .OrderByDescending(x => x.Value.Sum(y => y.Rating)/x.Value.Count)
                .Take(amount).Select(kvp => kvp.Key).ToList();
        }
        
        /// <summary>
        /// Returns a list of N MovieReviews, that is related to the movie 
        /// </summary>
        /// <param name="movieId"></param>
        /// <param name="amount"></param>
        /// <returns>A list of moviereviews, null if none was found</returns>
        public List<MovieReview> GetListOfReviewers(int movieId, int amount)
        {
            if (!_context.Movies.ContainsKey(movieId)) return null;
            return _context.Movies[movieId]
                .OrderByDescending(rev => rev.Rating)
                .ThenBy(rev => rev.Date)
                .Take(amount)
                .ToList();
        }
    }
}