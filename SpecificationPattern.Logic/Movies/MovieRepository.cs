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

    public Task<Movie[]> GetListAsync(bool forKidsOnly, double minimumRating, bool availableOnCd)
    {
        return _dbContext.Set<Movie>()
            .Where(m => 
                (m.MpaaRating <= MpaaRating.PG || !forKidsOnly)
                && m.Rating >= minimumRating
                && (m.ReleaseDate <= DateTime.Now.AddMonths(-6) || !availableOnCd))
            .ToArrayAsync();
    }
}