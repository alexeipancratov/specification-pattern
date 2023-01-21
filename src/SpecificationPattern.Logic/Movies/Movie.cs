using SpecificationPattern.Logic.Utils;

namespace SpecificationPattern.Logic.Movies;

public class Movie : Entity
{
    public Movie(string name, DateTime releaseDate, MpaaRating mpaaRating)
    {
        Name = name;
        ReleaseDate = releaseDate;
        MpaaRating = mpaaRating;
    }
    
    public string Name { get; }
    public DateTime ReleaseDate { get; }
    public MpaaRating MpaaRating { get; }
    public string Genre { get; }
    public double Rating { get; }
    public Director Director { get; }

    // public bool IsSuitableForChildren => MpaaRating <= MpaaRating.PG;

    // public bool HasCdVersion => ReleaseDate <= DateTime.Now.AddMonths(-6);
}

public enum MpaaRating
{
    G = 1,
    PG = 2,
    PG13 = 3,
    R = 4
}