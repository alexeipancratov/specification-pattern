using System.Linq.Expressions;
using SpecificationPattern.Logic.Movies;
using SpecificationPattern.Logic.Specifications.Abstract;

namespace SpecificationPattern.Logic.Specifications;

public class AvailableOnCdSpecification : Specification<Movie>
{
    private const int NumberOfMonthsDvdIsOut = 6;
    
    public override Expression<Func<Movie, bool>> ToExpression()
    {
        return m => m.ReleaseDate <= DateTime.Now.AddMonths(-NumberOfMonthsDvdIsOut);
    }
}