using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

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

    public Task<Movie[]> GetListAsync(Expression<Func<Movie, bool>> expression)
    {
        return _dbContext.Set<Movie>()
            .Where(expression)
            .ToArrayAsync();
    }
}