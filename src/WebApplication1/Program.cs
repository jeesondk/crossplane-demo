using Microsoft.EntityFrameworkCore;
using WebApplication1.context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Configuration.AddEnvironmentVariables();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<WeatherContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("WeatherContext")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.MapPost("/weatherforecast", (WeatherForecast forecast) =>
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider
            .GetRequiredService<WeatherContext>();
        dbContext.WeatherForecasts.Add(forecast);
        dbContext.SaveChanges();
    })
    .WithName("AddWeatherForecast")
    .WithOpenApi();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<WeatherContext>();
    
    // Here is the migration executed
    dbContext.Database.Migrate();
}

app.Run();

public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public Guid Id { get; set; }
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}