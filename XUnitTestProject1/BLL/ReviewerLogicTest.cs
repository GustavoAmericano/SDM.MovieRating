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
            var mock = new Mock<IDataContext>();

            IReviewerLogic reviewerLogic = new ReviewerLogic(mock.Object);
            mock.SetupGet(dat => dat.Reviewers).Returns(() => _reviewers);


            int reviewer1Count = reviewerLogic.GetReviews(1).Count;
            int reviewer2Count = reviewerLogic.GetReviews(2).Count;
            int reviewer3Count = reviewerLogic.GetReviews(3).Count;
            Assert.Equal(3, reviewer1Count);
            Assert.Equal(3, reviewer2Count);
            Assert.Equal(1, reviewer3Count);
        }

        private static Dictionary<int, List<MovieReview>> GenerateData()
        {
            List<MovieReview> reviews = new List<MovieReview>
            {
                new MovieReview() {Date = DateTime.Now, Rating = 10, MovieId = 1, ReviewerId = 1},
                new MovieReview() {Date = DateTime.Now, Rating = 10, MovieId = 1, ReviewerId = 1},
                new MovieReview() {Date = DateTime.Now, Rating = 10, MovieId = 1, ReviewerId = 1},
                new MovieReview() {Date = DateTime.Now, Rating = 10, MovieId = 1, ReviewerId = 3},
                new MovieReview() {Date = DateTime.Now, Rating = 10, MovieId = 1, ReviewerId = 2},
                new MovieReview() {Date = DateTime.Now, Rating = 10, MovieId = 1, ReviewerId = 2},
                new MovieReview() {Date = DateTime.Now, Rating = 10, MovieId = 1, ReviewerId = 2}
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