using System.Collections.Generic;

namespace SDM.MovieRating.BE
{
    public class Reviewer
    {
        public int Id { get; set; }
        public List<MovieReview> Reviews { get; set; }
    }
}