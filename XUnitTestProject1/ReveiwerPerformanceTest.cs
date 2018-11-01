using System.Diagnostics;
using SDM.MovieRating.BLL.Implementation;
using SDM.MovieRating.DAL;
using Xunit;
using Xunit.Abstractions;

namespace MovieRatingTest
{
    public class ReveiwerPerformanceTest : IClassFixture<DataContext>
    {
        private readonly ITestOutputHelper Output;
        private ReviewerLogic _reviewerLogic;
        private readonly DataContext _ctx;


        public ReveiwerPerformanceTest(ITestOutputHelper output, DataContext dataContext)
        {
            _ctx = dataContext;
            Output = output;
            _reviewerLogic = new ReviewerLogic(_ctx);
        }

        [Fact]
        public void GetReveiwesPerformanceTest()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            _reviewerLogic.GetReviews(459);
            
            stopwatch.Stop();
            
            Output.WriteLine($"It took {stopwatch.Elapsed.TotalMilliseconds} ms in average\n");
            Assert.True(stopwatch.Elapsed.TotalMilliseconds < 4000);
        }
        
        [Fact]
        public void GetAverageRatingPerformanceTest()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            _reviewerLogic.GetAverageRating(598);
            
            stopwatch.Stop();
            
            Output.WriteLine($"It took {stopwatch.Elapsed.TotalMilliseconds} ms in average\n");
            Assert.True(stopwatch.Elapsed.TotalMilliseconds < 4000);
        }
        
        [Fact]
        public void GetTimesRatingGivenPerformanceTest()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            _reviewerLogic.GetTimesRatingGiven(640, 4);
            
            stopwatch.Stop();
            
            Output.WriteLine($"It took {stopwatch.Elapsed.TotalMilliseconds} ms in average\n");
            Assert.True(stopwatch.Elapsed.TotalMilliseconds < 4000);
        }
        
        [Fact]
        public void GetReviewerWithMostReviewsPerformanceTest()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            _reviewerLogic.GetReviewerWithMostReviewes();
            
            stopwatch.Stop();
            
            Output.WriteLine($"It took {stopwatch.Elapsed.TotalMilliseconds} ms in average\n");
            Assert.True(stopwatch.Elapsed.TotalMilliseconds < 4000);
        }
        
        [Fact]
        public void PerformanceTest()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            _reviewerLogic.GetReviewedMovies(560, 5);
            
            stopwatch.Stop();
            
            Output.WriteLine($"It took {stopwatch.Elapsed.TotalMilliseconds} ms in average\n");
            Assert.True(stopwatch.Elapsed.TotalMilliseconds < 4000);
        }
        
        
    }
}