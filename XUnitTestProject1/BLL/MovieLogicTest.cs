using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Moq;
using SDM.MovieRating.BE;
using SDM.MovieRating.BLL.Implementation;
using SDM.MovieRating.BLL.Interfaces;
using SDM.MovieRating.DAL;
using Xunit;
using Xunit.Abstractions;

namespace MovieRatingTest.BLL
{
    public class MovieLogicTest
    {
        private readonly ITestOutputHelper Output;

        public MovieLogicTest(ITestOutputHelper output)
        {
            Output = output;
        }

        private Dictionary<int, List<MovieReview>> _movies = GenerateData();

        //4. On input N, how many have reviewed movie N?
        [Fact]
        public void MoviesReviewsCountTest()
        {
            IMovieLogic movieLogic = GetMovieLogic();

            int count = movieLogic.GetReviews(1).Count;
            Assert.Equal(3, count);
            count = movieLogic.GetReviews(2).Count;
            Assert.Equal(3, count);
            count = movieLogic.GetReviews(3).Count;
            Assert.Equal(2, count);
            count = movieLogic.GetReviews(4).Count;
            Assert.Equal(1, count);
        }
        [Theory]
        [InlineData(-1)]
        [InlineData(9999)]
        public void MoviesReviewsCountBadIdTest(int id)
        {
            IMovieLogic movieLogic = GetMovieLogic();

            int count = movieLogic.GetReviews(id).Count;
            Assert.Equal(0, count);
        }

        //5. On input N, what is the average rate the movie N had received?
        [Fact]
        public void MovieAverageRatingTest()
        {
            IMovieLogic movieLogic = GetMovieLogic();

            int avg = movieLogic.GetAverageRating(1);

            Assert.Equal(5, avg);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(99999)]
        public void MovieAverageRatingBadIdTest(int id)
        {
            IMovieLogic movieLogic = GetMovieLogic();
            int avg = movieLogic.GetAverageRating(id);

            Assert.Equal(0, avg);
        }



        //6. On input N and G, how many times had movie N received grade G?
        [Fact]
        public void GetTimesSameGradingFromMovieTest()
        {
            IMovieLogic movieLogic = GetMovieLogic();

            int count = movieLogic.GetTimesRatingGiven(1, 5);

            Assert.Equal(3, count);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(99999)]
        public void GetTimesSameGradingFromMovieBadIdTest(int id)
        {
            IMovieLogic movieLogic = GetMovieLogic();

            int count = movieLogic.GetTimesRatingGiven(id, 5);

            Assert.Equal(0, count);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(6)]
        public void GetTimesSameGradingFromMovieGradeOutOfRangeTest(int id)
        {
            IMovieLogic movieLogic = GetMovieLogic();

            int count = movieLogic.GetTimesRatingGiven(id, 5);

            Assert.Equal(0, count);
        }

        //7. What is the id(s) of the movie(s) with the highest number of top rates (5)?
        [Fact]
        public void TopMovieIdsWithMostTopRatingTest()
        {
            IMovieLogic movieLogic = GetMovieLogic();

            List<int> movieIds = movieLogic.GetTopRatedMovies();


            Assert.Equal(2, movieIds.Count);
            Assert.Equal(1, movieIds[0]);
        }

        //9. On input N, what is top N of movies? The score of a movie is its average rate.

        [Fact]
        public void TopNOfMoviesWithHighestAverageRatingTest()
        {
            IMovieLogic movieLogic = GetMovieLogic();
            
            List<int> movieIds = movieLogic.GetTopMovies(2);

            Assert.Equal(2, movieIds.Count);
        }
        
        
        //11. On input N, what are the reviewers that have reviewed movie N? The list
        //should be sorted decreasing by rate first, and date secondly
        [Fact]
        public void GetListOfReviewersTest()
        {
            IMovieLogic movieLogic = GetMovieLogic();
            List<MovieReview> reviews = movieLogic.GetListOfReviewers(1, 3);
 
            Assert.Equal(3, reviews.Count);
            Assert.Equal(4, reviews[0].ReviewerId);
            Assert.Equal(2, reviews[2].ReviewerId);
        }
        [Theory]
        [InlineData(-1)]
        [InlineData(9999)]
        public void GetListOfReviewersBadMovieIdTest(int mId)
        {
            IMovieLogic movieLogic = GetMovieLogic();
            List<MovieReview> reviews = movieLogic.GetListOfReviewers(mId, 3);

            Assert.Null(reviews);
        }

        private IMovieLogic GetMovieLogic()
        {
            var mock = new Mock<IDataContext>();
            IMovieLogic movieLogic = new MovieLogic(mock.Object);
            mock.SetupGet(dat => dat.Movies).Returns(() => _movies);
            return movieLogic;
        }

        private static Dictionary<int, List<MovieReview>> GenerateData()
        {
            List<MovieReview> movies = new List<MovieReview>
            {
                new MovieReview() {Date = DateTime.Now.AddDays(-283), Rating = 5, MovieId = 1, ReviewerId = 1},
                new MovieReview() {Date = DateTime.Now.AddDays(-32), Rating = 5, MovieId = 1, ReviewerId = 2},
                new MovieReview() {Date = DateTime.Now.AddDays(-900), Rating = 5, MovieId = 1, ReviewerId = 4},
 
                new MovieReview() {Date = DateTime.Now.AddDays(-94), Rating = 5, MovieId = 2, ReviewerId = 4},
                new MovieReview() {Date = DateTime.Now.AddDays(-842), Rating = 5, MovieId = 2, ReviewerId = 1},
                new MovieReview() {Date = DateTime.Now.AddDays(-48), Rating = 5, MovieId = 2, ReviewerId = 3},
 
                new MovieReview() {Date = DateTime.Now.AddDays(0), Rating = 1, MovieId = 3, ReviewerId = 1},
                new MovieReview() {Date = DateTime.Now.AddDays(-43), Rating = 5, MovieId = 3, ReviewerId = 4},
 
                new MovieReview() {Date = DateTime.Now.AddDays(-43), Rating = 5, MovieId = 4, ReviewerId = 3},
            };

            Dictionary<int, List<MovieReview>> Movies = new Dictionary<int, List<MovieReview>>();
            foreach (var r in movies)
            {
                if (!Movies.ContainsKey(r.MovieId))
                    Movies[r.MovieId] = new List<MovieReview>();
                Movies[r.MovieId].Add(r);
            }

            return Movies;
        }
    }
}