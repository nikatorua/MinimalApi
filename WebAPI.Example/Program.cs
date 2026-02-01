namespace WebAPI.Example;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        app.MapGet("/weatherforecast", (HttpContext httpContext) =>
            {
                var forecast = Enumerable.Range(1, 5).Select(index =>
                        new WeatherForecast
                        {
                            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                            TemperatureC = Random.Shared.Next(-20, 55),
                            Summary = summaries[Random.Shared.Next(summaries.Length)]
                        })
                    .ToArray();
                return forecast;
            })
            .WithName("GetWeatherForecast")
            .WithOpenApi();
        
        app.MapGet("/api/sum", (int a, int b) =>
            {
                var result = a + b;
                return Results.Ok(new { a, b, sum = result });
            })
            .WithName("GetSum")
            .WithOpenApi()
            .Produces<SumResponse>(StatusCodes.Status200OK);
        
        app.MapPost("/api/greet", (GreetRequest request) =>
            {
                if (string.IsNullOrWhiteSpace(request.Name))
                {
                    return Results.BadRequest(new { error = "Name is required" });
                }

                var greeting = $"Hello, {request.Name}!";
                return Results.Ok(new { message = greeting });
            })
            .WithName("PostGreet")
            .WithOpenApi()
            .Produces<GreetResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);

        app.Run();
    }
}

public record SumResponse(int A, int B, int Sum);
public record GreetRequest(string Name);
public record GreetResponse(string Message);