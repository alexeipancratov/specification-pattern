using FluentAssertions;
using SpecificationPattern.Logic.Movies;
using SpecificationPattern.Logic.Specifications;

namespace SpecificationPattern.Logic.Tests.Specifications;

public class MovieForKidsSpecificationTests
{
    [Theory]
    [InlineData(MpaaRating.G, true)]
    [InlineData(MpaaRating.PG, true)]
    [InlineData(MpaaRating.PG13, false)]
    [InlineData(MpaaRating.R, false)]
    public void GivenAMpaaRating_ReturnsCorrespondingResult(MpaaRating mpaaRating, bool expectedResult)
    {
        // Arrange
        var movie = new Movie("Movie 1", new DateTime(2017, 01, 01), mpaaRating);
        var sut = new MovieForKidsSpecification();
        
        // Act/Assert
        sut.IsSatisfiedBy(movie).Should().Be(expectedResult);
    }
}