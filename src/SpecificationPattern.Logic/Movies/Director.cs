using SpecificationPattern.Logic.Utils;

namespace SpecificationPattern.Logic.Movies;

public class Director : Entity
{
    public Director(string name)
    {
        Name = name;
    }

    public string Name { get; }
}