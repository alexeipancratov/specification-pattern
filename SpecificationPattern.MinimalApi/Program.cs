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

app.MapGet("/movies", (MovieRepository movieRepository, [FromQuery] bool? forKidsOnly)
        => movieRepository.GetListAsync(forKidsOnly ?? false))
.WithName("GetAllMovies")
.WithOpenApi();

app.Run();
