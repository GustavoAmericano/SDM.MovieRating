using SDM.MovieRating.DAL;
using Xunit;

namespace MovieRatingTest.DAL
{
    public class DataContextTest
    {
        [Fact]
        public void TestDataExistOnInitializationSmileyFace()
        {
            DataContext db = new DataContext();
            Assert.NotEmpty(db.MovieReviews);
            Assert.NotEmpty(db.Movies);
            Assert.NotEmpty(db.Reviewers);
        }
    }
}