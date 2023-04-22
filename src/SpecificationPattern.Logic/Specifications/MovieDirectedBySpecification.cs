using Ardalis.Specification;
using SpecificationPattern.Logic.Movies;

namespace SpecificationPattern.Logic.Specifications;

// An example of a specification working with related data.
// You need to use Eager Loading though, or DTOs.
public class MovieDirectedBySpecification : Specification<Movie>
{
    private readonly string _director;

    public MovieDirectedBySpecification(string director)
    {
        _director = director;

        Query.Where(m => m.Director.Name == _director);
    }
}