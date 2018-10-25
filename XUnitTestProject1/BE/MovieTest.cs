using System.Collections.Generic;
using SDM.MovieRating.BE;
using Xunit;

namespace MovieRatingTest.BE
{
    public class MovieTest
    {
        [Fact]
        public void TestMovieValid()
        {
            int id = 1;
            List<MovieReview> revs = new List<MovieReview>();
            Movie m = new Movie()
            {
                Id = id,
                Reviews = revs
            };

            Assert.Equal(revs, m.Reviews);
            Assert.Equal(id, m.Id);
        }
    }
}