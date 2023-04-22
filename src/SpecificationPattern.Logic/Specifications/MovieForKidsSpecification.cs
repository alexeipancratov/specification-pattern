using Ardalis.Specification;
using SpecificationPattern.Logic.Movies;

namespace SpecificationPattern.Logic.Specifications;

// NOTE: Make specifications as specific as possible => to have a clear business meaning => repeated logic.
// Bad examples of specs: MpaaRatingAtMost(MpaaRating rating), MovieForAdultsOnly.
public class MovieForKidsSpecification : Specification<Movie>
{
    public MovieForKidsSpecification()
    {
        Query.Where(m => m.MpaaRating <= MpaaRating.PG);
    }
}