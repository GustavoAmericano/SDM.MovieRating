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



        private IReviewerLogic logic = new ReviewerLogic();

        [Fact]
        public void GetNumberOfReviewsFromReviewerTest()
        {

        }

        private static Dictionary<int, List<MovieReview>> GenerateData()
        {
            Random rnd = new Random();
            List<MovieReview> reviews = new List<MovieReview>();
            for (int i = 0; i < 100; i++)
            {
                reviews.Add(new MovieReview()
                {
                    Date = DateTime.Now.AddDays(rnd.Next(0, 100)),
                    MovieId = rnd.Next(0, 100),
                    Rating = rnd.Next(0, 10),
                    ReviewerId = rnd.Next(0, 10)
                });
            }

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