using System.Collections.Generic;

namespace SDM.MovieRating.BE
{
    public class Movie
    {
        public int Id { get; set; }
        public List<MovieReview> Reviews { get; set; }
    }
}