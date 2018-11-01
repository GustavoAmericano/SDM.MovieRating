using System.Diagnostics;
using Newtonsoft.Json;
using SDM.MovieRating.BLL.Implementation;
using SDM.MovieRating.DAL;
using Xunit;
using Xunit.Abstractions;

namespace MovieRatingTest
{
    public class MoviePerformanceTest : IClassFixture<DataContext>
    {
        private readonly ITestOutputHelper Output;
        private MovieLogic _movieLogic;
        private readonly DataContext _ctx;


        public MoviePerformanceTest(ITestOutputHelper output, DataContext dataContext)
        {
            _ctx = dataContext;
            Output = output;
            _movieLogic = new MovieLogic(_ctx);
        }

        [Fact]
        public void GetReviewersPerformanceTest()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            _movieLogic.GetReviews(1493615);
            stopwatch.Stop();

            Output.WriteLine($"It took {stopwatch.Elapsed.TotalMilliseconds} ms in average\n");
            Assert.True(stopwatch.Elapsed.TotalMilliseconds < 4000);
        }

        [Fact]
        public void GetAverageRatingPerformanceTest()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            _movieLogic.GetAverageRating(1638787);
            
            stopwatch.Stop();
            
            Output.WriteLine($"It took {stopwatch.Elapsed.TotalMilliseconds} ms in average\n");
            Assert.True(stopwatch.Elapsed.TotalMilliseconds < 4000);
        }
        
        [Fact]
        public void GetTimesRatingGivenPerformanceTest()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            _movieLogic.GetTimesRatingGiven(2387526, 3);
            
            stopwatch.Stop();
            
            Output.WriteLine($"It took {stopwatch.Elapsed.TotalMilliseconds} ms in average\n");
            Assert.True(stopwatch.Elapsed.TotalMilliseconds < 4000);
        }
        
        [Fact]
        public void GetTopRatedMoviesPerformanceTest()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            _movieLogic.GetTopRatedMovies();
            
            stopwatch.Stop();
            
            Output.WriteLine($"It took {stopwatch.Elapsed.TotalMilliseconds} ms in average\n");
            Assert.True(stopwatch.Elapsed.TotalMilliseconds < 4000);
        }
        
        [Fact]
        public void GetTopMoviesPerformanceTest()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            _movieLogic.GetTopMovies(5);
            
            stopwatch.Stop();
            
            Output.WriteLine($"It took {stopwatch.Elapsed.TotalMilliseconds} ms in average\n");
            Assert.True(stopwatch.Elapsed.TotalMilliseconds < 4000);
        }
        
        [Fact]
        public void GetListOfReviewersPerformanceTest()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            _movieLogic.GetListOfReviewers(1320519, 5);
            
            stopwatch.Stop();
            
            Output.WriteLine($"It took {stopwatch.Elapsed.TotalMilliseconds} ms in average\n");
            Assert.True(stopwatch.Elapsed.TotalMilliseconds < 4000);
        }
        
        
    }
}