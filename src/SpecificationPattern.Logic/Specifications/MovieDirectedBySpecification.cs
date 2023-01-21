using System.Linq.Expressions;
using SpecificationPattern.Logic.Movies;
using SpecificationPattern.Logic.Specifications.Abstract;

namespace SpecificationPattern.Logic.Specifications;

// An example of a specification working with related data.
// You need to use Eager Loading though, or DTOs.
public class MovieDirectedBySpecification : Specification<Movie>
{
    private readonly string _director;

    public MovieDirectedBySpecification(string director)
    {
        _director = director;
    }
    
    public override Expression<Func<Movie, bool>> ToExpression()
    {
        return m => m.Director.Name == _director;
    }
}