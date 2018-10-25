using System;
using System.Collections.Generic;
using SDM.MovieRating.BE;
using SDM.MovieRating.DAL;
using Xunit;

namespace MovieRatingTest.DAL
{
    public class ReadJSONTest
    {
        ReadJSON _rj = new ReadJSON();

        [Fact]
        public void ReadJSONOutputTest()
        {
            List<MovieReview> reviews = _rj.ReadJSON();
            MovieReview r = reviews[0];
            Assert.Equal(1,r.ReviewerId);
            Assert.Equal(1488844, r.MovieId);
            Assert.Equal(3, r.Rating);
            Assert.Equal(new DateTime(2005,09,06), r.Date);

            //{ Reviewer:1, Movie:1488844, Grade:3, Date:'2005-09-06'}
        }
    }
}