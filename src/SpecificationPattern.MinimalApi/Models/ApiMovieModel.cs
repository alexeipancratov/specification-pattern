using SpecificationPattern.Logic.Movies;

namespace SpecificationPattern.MinimalApi.Models;

public class ApiMovieModel
{
    public string Name { get; set; }
    
    public DateTime ReleaseDate { get; set; }
    
    public MpaaRating MpaaRating { get; set; }
    
    public string Genre { get; set; }
    
    public double Rating { get; set; }

    public string Director { get; set; }
}