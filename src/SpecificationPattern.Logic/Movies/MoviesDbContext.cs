using Microsoft.EntityFrameworkCore;

namespace SpecificationPattern.Logic.Movies;

public class MoviesDbContext : DbContext
{
    private readonly string _connectionString;

    public MoviesDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>((eb) =>
        {
            eb.HasKey(m => m.Id);
            eb.Property(m => m.Id).HasColumnName("MovieID");
            eb.Property(m => m.Name);
            eb.Property(m => m.Genre);
            eb.Property(m => m.Rating);
            eb.Property(m => m.MpaaRating);
            eb.Property(m => m.ReleaseDate);
            eb.HasOne<Director>(m => m.Director);
        });

        modelBuilder.Entity<Director>(eb =>
        {
            eb.HasKey(m => m.Id);
            eb.Property(m => m.Id).HasColumnName("DirectorID");
            eb.Property(m => m.Name);
        });
    }
}