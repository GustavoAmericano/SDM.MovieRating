using System.Collections.Generic;
using SDM.MovieRating.BE;

namespace SDM.MovieRating.DAL
{
    public interface IDataContext
    {
        Dictionary<int, List<MovieReview>> Movies { get; set; }
        Dictionary<int, List<MovieReview>> Reviewers { get; set; }
    }
}