using SDM.MovieRating.DAL;
using Xunit;

namespace MovieRatingTest.DAL
{
    public class FakeDBTest
    {
        [Fact]
        public void TestDataExistOnInitializationSmileyFace()
        {
            FakeDB db = new FakeDB();
            Assert.NotEmpty(db.MovieReviews);
            Assert.NotEmpty(db.Movies);
            Assert.NotEmpty(db.Reviewers);
        }
    }
}