using Microsoft.AspNetCore.Mvc;
using SpecificationPattern.Logic.Movies;
using SpecificationPattern.Logic.Specifications;
using SpecificationPattern.MinimalApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(_ =>
    new MoviesDbContext(builder.Configuration.GetConnectionString("MoviesDb") ?? string.Empty));
builder.Services.AddScoped<MovieRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/movies", async (MovieRepository movieRepository,
            [FromQuery] bool? forKidsOnly,
            [FromQuery] double? minimumRating,
            [FromQuery] bool? availableOnCd)
        =>
    {
        // var spec = Specification<Movie>.All;
        // if (forKidsOnly.HasValue && forKidsOnly.Value)
        // {
        //     spec = spec.And(new MovieForKidsSpecification());
        // }
        //
        // if (availableOnCd.HasValue && availableOnCd.Value)
        // {
        //     spec = spec.And(new AvailableOnCdSpecification());
        // }
        var spec = new MovieDirectedBySpecification("Bill Condon");

        var movies = await movieRepository.GetListAsync(spec, minimumRating ?? 0);

        return movies.Select(m => new ApiMovieModel
        {
            Name = m.Name,
            Director = m.Director.Name,
            Genre = m.Genre,
            MpaaRating = m.MpaaRating,
            Rating = m.Rating,
            ReleaseDate = m.ReleaseDate
        }).ToList();
    })
.WithName("GetAllMovies")
.WithOpenApi();

app.MapPost("/ticketPurchases/adult/{movieId}", () => true);
app.MapPost("/ticketPurchases/children/{movieId}", async (long movieId, MovieRepository movieRepository) => {
    Movie? movie = await movieRepository.GetOneAsync(movieId);
    if (movie is null)
    {
        return Results.BadRequest("Movie not found.");
    }

    var spec = new MovieForKidsSpecification();
    if (!spec.IsSatisfiedBy(movie))
    {
        return Results.BadRequest("Movie is not suitable for children");
    }

    return Results.Ok("Children ticket purchase successful!");
});
app.MapPost("/moviePurchases/cd/{movieId}", async (long movieId, MovieRepository movieRepository) => {
    Movie? movie = await movieRepository.GetOneAsync(movieId);
    if (movie is null)
    {
        return Results.BadRequest("Movie not found.");
    }

    var spec = new AvailableOnCdSpecification();
    if (!spec.IsSatisfiedBy(movie))
    {
        return Results.BadRequest("Movie does not have a CD version yet.");
    }

    return Results.Ok("CD purchase successful!");
});

app.Run();
