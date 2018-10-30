using System.Collections.Generic;
using SDM.MovieRating.BE;

namespace SDM.MovieRating.DAL
{
    public class DataContext
    {
        public List<MovieReview> MovieReviews { get; set; }
        public Dictionary<int, List<MovieReview>> Movies;
        public Dictionary<int, List<MovieReview>> Reviewers;

        public DataContext()
        {
            Movies = new Dictionary<int, List<MovieReview>>();
            Reviewers = new Dictionary<int, List<MovieReview>>();
            JSONReader jr = new JSONReader();
            MovieReviews = jr.ReadJSON();

            InsertIntoDictionaries();
        }

        private void InsertIntoDictionaries()
        {
            foreach (MovieReview m in MovieReviews)
            {
                if (!Reviewers.ContainsKey(m.ReviewerId))
                    Reviewers[m.ReviewerId] = new List<MovieReview>();
                Reviewers[m.ReviewerId].Add(m);

                if (!Movies.ContainsKey(m.MovieId))
                    Movies[m.MovieId] = new List<MovieReview>();
                Movies[m.MovieId].Add(m);
            }
        }
    }
}