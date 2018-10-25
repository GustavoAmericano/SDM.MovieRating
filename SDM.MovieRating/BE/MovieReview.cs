using System;

namespace SDM.MovieRating.BE
{
    public class MovieReview
    {
        public int Rating { get; set; }
        public DateTime Date { get; set; }
        public int ReviewerId { get; set; }
        public int MovieId { get; set; }
    }
}