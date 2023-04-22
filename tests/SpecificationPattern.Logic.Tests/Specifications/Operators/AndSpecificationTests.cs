using FluentAssertions;
using SpecificationPattern.Logic.Movies;
using SpecificationPattern.Logic.Specifications;

namespace SpecificationPattern.Logic.Tests.Specifications.Operators;

public class AndSpecificationTests
{
    [Fact]
    public void Test()
    {
        // Arrange
        var movie = new Movie("Movie 1", new DateTime(2017, 01, 01), MpaaRating.PG);
        var forKids = new MovieForKidsSpecification();
        var onCd = new AvailableOnCdSpecification();
        
        // Act/Assert
        onCd.And(forKids).IsSatisfiedBy(movie).Should().BeTrue();
    }
}