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

    public Task<Movie[]> GetListAsync(bool forKidsOnly)
    {
        return _dbContext.Set<Movie>()
            .Where(m => m.MpaaRating <= MpaaRating.PG || !forKidsOnly)
            .ToArrayAsync();
    }
}