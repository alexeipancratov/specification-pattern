using Ardalis.Specification;
using SpecificationPattern.Logic.Movies;

namespace SpecificationPattern.Logic.Specifications;

public class AvailableOnCdSpecification : Specification<Movie>
{
    private const int NumberOfMonthsDvdIsOut = 6;

    public AvailableOnCdSpecification()
    {
        Query.Where(m => m.ReleaseDate <= DateTime.Now.AddMonths(-NumberOfMonthsDvdIsOut));
    }
}