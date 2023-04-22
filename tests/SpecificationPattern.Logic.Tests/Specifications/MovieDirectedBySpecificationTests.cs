using FluentAssertions;
using SpecificationPattern.Logic.Movies;
using SpecificationPattern.Logic.Specifications;

namespace SpecificationPattern.Logic.Tests.Specifications;

public class MovieDirectedBySpecificationTests
{
    [Theory]
    [InlineData("Bob Higgins", true)]
    [InlineData("bob higgins", false)]
    [InlineData("Tom Higgins", false)]
    public void MovieDirectedByBobHiggins_ShouldReturnCorrespondingResult(string requestedDirectorName, bool expectedResult)
    {
        // Arrange
        var director = new Director("Bob Higgins");
        var movie = new Movie("Movie 1", new DateTime(2017, 01, 01), MpaaRating.G, director);
        var sut = new MovieDirectedBySpecification(requestedDirectorName);
        
        // Act/Assert
        sut.IsSatisfiedBy(movie).Should().Be(expectedResult);
    }
}