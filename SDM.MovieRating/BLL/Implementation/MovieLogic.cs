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

        public List<MovieReview> GetReviews(int movieId)
        {
            return _context.Movies[movieId];
        }

        public int GetAverageRating(int movieId)
        {
            int avg = 0;
            int count = 0;

            GetReviews(movieId).ForEach(mov =>
            {
                count++;
                avg += mov.Rating;
            });

            return avg / count;
        }

        public int GetTimesRatingGiven(int movieId, int rating)
        {
            return GetReviews(movieId).Count(mov => mov.Rating == rating);
        }

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

        public List<int> GetTopMovies(int amount)
        {
            return _context.Movies
                .OrderByDescending(x => x.Value.Sum(y => y.Rating)/x.Value.Count)
                .Take(amount).Select(kvp => kvp.Key).ToList();
        }
        
        public List<MovieReview> GetListOfReviewers(int movieId, int amount)
        {
            return _context.Movies[movieId]
                .OrderByDescending(rev => rev.Rating)
                .ThenBy(rev => rev.Date)
                .Take(amount)
                .ToList();
        }
    }
}