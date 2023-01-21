using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SpecificationPattern.Logic.Specifications.Abstract;

namespace SpecificationPattern.Logic.Movies;

public class MovieRepository
{
    private readonly MoviesDbContext _dbContext;

    public MovieRepository(MoviesDbContext dbContext)
    {
        _dbContext = dbContext;
    }
        
    public ValueTask<Movie?> GetOneAsync(long id)
    {
        return _dbContext.Set<Movie>().FindAsync(id);
    }

    public Task<Movie[]> GetListAsync(
        Specification<Movie> specification,
        double minimumRating,
        int page = 0,
        int pageSize = 4)
    {
        return _dbContext.Set<Movie>()
            .Include(m => m.Director)
            .Where(specification.ToExpression())
            .Where(m => m.Rating >= minimumRating)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToArrayAsync();
    }
}