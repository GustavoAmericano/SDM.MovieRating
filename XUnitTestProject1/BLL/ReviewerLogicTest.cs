using System;
using System.Collections.Generic;
using Moq;
using SDM.MovieRating.BE;
using SDM.MovieRating.BLL.Implementation;
using SDM.MovieRating.BLL.Interfaces;
using SDM.MovieRating.DAL;
using Xunit;

namespace MovieRatingTest.BLL
{

    public class ReviewerLogicTest
    {
        private Dictionary<int, List<MovieReview>> _reviewers = GenerateData();




        [Fact]
        public void GetNumberOfReviewsFromReviewerTest()
        {
            IReviewerLogic reviewerLogic = GetReviewerLogic();
            int reviewer1Count = reviewerLogic.GetReviews(1).Count;
            int reviewer2Count = reviewerLogic.GetReviews(2).Count;
            int reviewer3Count = reviewerLogic.GetReviews(3).Count;
            Assert.Equal(3, reviewer1Count);
            Assert.Equal(3, reviewer2Count);
            Assert.Equal(1, reviewer3Count);
        }

        [Fact]
        public void GetAverageRatingFromReviewerTest()
        {
            IReviewerLogic reviewerLogic = GetReviewerLogic();
            int avg = reviewerLogic.GetAverageRating(1);
            Assert.Equal(7, avg);
        }

        [Fact]
        // HOW MANY TIMES HAS REVIEWER GIVEN GRADE N
        public void GetTimesSameGradingFromReviewerTest()
        {
            IReviewerLogic reviewerLogic = GetReviewerLogic();
            int count = reviewerLogic.GetTimesRatingGiven(1, 10);

            Assert.Equal(2, count);
        }
        private IReviewerLogic GetReviewerLogic()
        {
            var mock = new Mock<IDataContext>();
            IReviewerLogic reviewerLogic = new ReviewerLogic(mock.Object);
            mock.SetupGet(dat => dat.Reviewers).Returns(() => _reviewers);
            return reviewerLogic;
        }

        private static Dictionary<int, List<MovieReview>> GenerateData()
        {
            List<MovieReview> reviews = new List<MovieReview>
            {
                new MovieReview() {Date = DateTime.Now, Rating = 10, MovieId = 1, ReviewerId = 1},
                new MovieReview() {Date = DateTime.Now, Rating = 10, MovieId = 2, ReviewerId = 1},
                new MovieReview() {Date = DateTime.Now, Rating = 1, MovieId = 3, ReviewerId = 1},
                new MovieReview() {Date = DateTime.Now, Rating = 10, MovieId = 4, ReviewerId = 3},
                new MovieReview() {Date = DateTime.Now, Rating = 10, MovieId = 1, ReviewerId = 2},
                new MovieReview() {Date = DateTime.Now, Rating = 10, MovieId = 2, ReviewerId = 2},
                new MovieReview() {Date = DateTime.Now, Rating = 10, MovieId = 3, ReviewerId = 2}
            };


            Dictionary<int, List<MovieReview>> Reviewers = new Dictionary<int, List<MovieReview>>();
            foreach (var r in reviews)
            {
                if (!Reviewers.ContainsKey(r.ReviewerId))
                    Reviewers[r.ReviewerId] = new List<MovieReview>();
                Reviewers[r.ReviewerId].Add(r);
            }

            return Reviewers;
        }
    }
}