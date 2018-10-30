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
            List<int> movieIds = new List<int>();
            Dictionary<int, int> movieReview = new Dictionary<int, int>();

            /*
            _context.Movies.ToList().ForEach(pair =>
            {
                movieReview.Add(pair.Key, pair.Value.Count(mov => mov.Rating == 5));
            });
            
            foreach (var pair in movieReview.OrderByDescending(val => val.Value))
            {
                movieIds.Add(pair.Value);                
            }

            return movieIds;
                */

            /*  
              var temp = _context.Movies
                  .OrderByDescending(x => x.Value.Count(y => y.Rating == 5))
                  //.Take(1)
                  .ToDictionary(key => key.Key, val => val.Value.Count(i => i.Rating == 5));
          */

            var pairs = _context.Movies
                .OrderByDescending(x => x.Value.Count(y => y.Rating == 5))
                .ToDictionary(key => key.Key, val => val.Value.Count(i => i.Rating == 5));

            List<int> topIds = new List<int>();

            int topCount = pairs[pairs.Keys.First()];

            pairs.ToList().ForEach(kv =>
            {
                if (kv.Value < topCount) return;
                else topIds.Add(kv.Key);
            });

            return topIds;
        }

        public List<int> GetTopMovies(int amount)
        {
            var pairs = _context.Movies
                .OrderByDescending(x => x.Value.Sum(y => y.Rating)/x.Value.Count)
                .Take(amount);

            List<int> topIds = new List<int>();

            pairs.ToList().ForEach(x => topIds.Add(x.Key));
            return topIds;


        }
    }
}