using System;
using System.Collections.Generic;
using SDM.MovieRating.BE;
using SDM.MovieRating.DAL;
using Xunit;

namespace MovieRatingTest.DAL
{
    public class ReadJSONTest
    {
        JSONReader _rj = new JSONReader();

        [Fact]
        public void ReadJSONOutputTest()
        {
            List<MovieReview> reviews = _rj.ReadJSON();
            MovieReview r = reviews[0];
            Assert.Equal(1,r.ReviewerId);
            Assert.Equal(1488844, r.MovieId);
            Assert.Equal(3, r.Rating);
            Assert.Equal(new DateTime(2005,09,06), r.Date);

            r = reviews[27];
            Assert.Equal(1, r.ReviewerId);
            Assert.Equal(814701, r.MovieId);
            Assert.Equal(5, r.Rating);
            Assert.Equal(new DateTime(2005, 09, 29), r.Date);

            //{ Reviewer:1, Movie:1488844, Grade:3, Date:'2005-09-06'}
            //{ Reviewer:1, Movie:814701, Grade:5, Date:'2005-09-29'}, 

        }
    }
}