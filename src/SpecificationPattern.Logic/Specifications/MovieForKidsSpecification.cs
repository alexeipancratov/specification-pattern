using System.Linq.Expressions;
using SpecificationPattern.Logic.Movies;
using SpecificationPattern.Logic.Specifications.Abstract;

namespace SpecificationPattern.Logic.Specifications;

// NOTE: Make specifications as specific as possible => to have a clear business meaning => repeated logic.
// Bad examples of specs: MpaaRatingAtMost(MpaaRating rating), MovieForAdultsOnly.
public class MovieForKidsSpecification : Specification<Movie>
{
    public override Expression<Func<Movie, bool>> ToExpression()
    {
        return m => m.MpaaRating <= MpaaRating.PG;
    }
}