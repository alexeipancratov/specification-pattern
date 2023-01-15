using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SpecificationPattern.Logic.Movies;

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

app.MapGet("/movies", (MovieRepository movieRepository,
    [FromQuery] bool? forKidsOnly,
    [FromQuery] double? minimumRating,
    [FromQuery] bool? availableOnCd)
        => movieRepository.GetListAsync(
            forKidsOnly ?? false,
            minimumRating ?? 0d,
            availableOnCd ?? false))
.WithName("GetAllMovies")
.WithOpenApi();

app.MapPost("/ticketPurchases/adult/{movieId}", () => true);
app.MapPost("/ticketPurchases/children/{movieId}", async (long movieId, MovieRepository movieRepository) => {
    Movie? movie = await movieRepository.GetOneAsync(movieId);
    if (movie is null)
    {
        return Results.BadRequest("Movie not found.");
    }

    if (movie.MpaaRating > MpaaRating.PG)
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

    if (movie.ReleaseDate >= DateTime.Now.AddMonths(-6))
    {
        return Results.BadRequest("Movie does not have a CD version yet.");
    }

    return Results.Ok("CD purchase successful!");
});

app.Run();
