using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace WebAPI.Example.Tests;

public class WeatherForecastTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public WeatherForecastTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetWeatherForecast_ReturnsSuccess()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/weatherforecast");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(response.Content);
    }

    [Fact]
    public async Task GetWeatherForecast_ReturnsValidJsonArray()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/weatherforecast");
        var content = await response.Content.ReadAsStringAsync();
        var forecasts = JsonSerializer.Deserialize<WeatherForecast[]>(content);

        // Assert
        Assert.NotNull(forecasts);
        Assert.Equal(5, forecasts.Length);
    }

    [Fact]
    public async Task GetWeatherForecast_ReturnsForecastWithValidProperties()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/weatherforecast");
        var content = await response.Content.ReadAsStringAsync();
        var forecasts = JsonSerializer.Deserialize<WeatherForecast[]>(content);

        // Assert
        Assert.NotNull(forecasts);
        foreach (var forecast in forecasts)
        {
            Assert.NotNull(forecast.Date);
            Assert.NotNull(forecast.Summary);
            Assert.InRange(forecast.TemperatureC, -20, 55);
        }
    }

    [Fact]
    public async Task GetWeatherForecast_TemperatureFIsCalculatedCorrectly()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/weatherforecast");
        var content = await response.Content.ReadAsStringAsync();
        var forecasts = JsonSerializer.Deserialize<WeatherForecast[]>(content);

        // Assert
        Assert.NotNull(forecasts);
        foreach (var forecast in forecasts)
        {
            var expectedF = 32 + (int)(forecast.TemperatureC / 0.5556);
            Assert.Equal(expectedF, forecast.TemperatureF);
        }
    }

    [Fact]
    public async Task GetWeatherForecast_ReturnsFuturesDates()
    {
        // Arrange
        var client = _factory.CreateClient();
        var today = DateOnly.FromDateTime(DateTime.Now);

        // Act
        var response = await client.GetAsync("/weatherforecast");
        var content = await response.Content.ReadAsStringAsync();
        var forecasts = JsonSerializer.Deserialize<WeatherForecast[]>(content);

        // Assert
        Assert.NotNull(forecasts);
        foreach (var forecast in forecasts)
        {
            Assert.True(forecast.Date > today, "Forecast date should be in the future");
        }
    }

    [Fact]
    public async Task GetWeatherForecast_ReturnsContentTypeJson()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/weatherforecast");

        // Assert
        Assert.NotNull(response.Content.Headers.ContentType);
        Assert.Contains("application/json", response.Content.Headers.ContentType.ToString());
    }
}
