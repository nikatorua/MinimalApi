using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace WebAPI.Example.Tests;

public class SumTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public SumTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetSum_WithValidNumbers_ReturnsSuccess()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/sum?a=5&b=3");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Theory]
    [InlineData(5, 3, 8)]
    [InlineData(0, 0, 0)]
    [InlineData(-5, 5, 0)]
    [InlineData(100, 200, 300)]
    [InlineData(-10, -20, -30)]
    public async Task GetSum_WithVariousNumbers_CalculatesCorrectly(int a, int b, int expected)
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync($"/api/sum?a={a}&b={b}");
        var content = await response.Content.ReadAsStringAsync();
        using var jsonDoc = JsonDocument.Parse(content);
        var root = jsonDoc.RootElement;
        var sum = root.GetProperty("sum").GetInt32();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(expected, sum);
    }

    [Fact]
    public async Task GetSum_ReturnsCorrectResponseStructure()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/sum?a=10&b=20");
        var content = await response.Content.ReadAsStringAsync();
        using var jsonDoc = JsonDocument.Parse(content);
        var root = jsonDoc.RootElement;

        // Assert
        Assert.True(root.TryGetProperty("a", out var aProperty));
        Assert.True(root.TryGetProperty("b", out var bProperty));
        Assert.True(root.TryGetProperty("sum", out var sumProperty));
        Assert.Equal(10, aProperty.GetInt32());
        Assert.Equal(20, bProperty.GetInt32());
        Assert.Equal(30, sumProperty.GetInt32());
    }

    [Fact]
    public async Task GetSum_ReturnsContentTypeJson()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/sum?a=5&b=3");

        // Assert
        Assert.NotNull(response.Content.Headers.ContentType);
        Assert.Contains("application/json", response.Content.Headers.ContentType.ToString());
    }

    [Fact]
    public async Task GetSum_WithMissingParameters_ReturnsBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/sum?a=5");

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetSum_WithNonNumericParameters_ReturnsBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/sum?a=abc&b=def");

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
