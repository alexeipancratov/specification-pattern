﻿using SpecificationPattern.Logic.Utils;

namespace SpecificationPattern.Logic.Movies;

public class Movie : Entity
{
    public string Name { get; }
    public DateTime ReleaseDate { get; }
    public MpaaRating MpaaRating { get; }
    public string Genre { get; }
    public double Rating { get; }
}

public enum MpaaRating
{
    G = 1,
    PG = 2,
    PG13 = 3,
    R = 4
}