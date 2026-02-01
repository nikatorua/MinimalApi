using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace WebAPI.Example.Tests;

public class GreetTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public GreetTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task PostGreet_WithValidName_ReturnsSuccess()
    {
        // Arrange
        var client = _factory.CreateClient();
        var request = new { name = "John" };

        // Act
        var response = await client.PostAsJsonAsync("/api/greet", request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Theory]
    [InlineData("John")]
    [InlineData("Alice")]
    [InlineData("Bob")]
    [InlineData("A")]
    [InlineData("VeryLongNameWithManyCharactersHere")]
    public async Task PostGreet_WithVariousValidNames_ReturnsCorrectGreeting(string name)
    {
        // Arrange
        var client = _factory.CreateClient();
        var request = new { name };

        // Act
        var response = await client.PostAsJsonAsync("/api/greet", request);
        var content = await response.Content.ReadAsStringAsync();
        using var jsonDoc = JsonDocument.Parse(content);
        var root = jsonDoc.RootElement;
        var message = root.GetProperty("message").GetString();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal($"Hello, {name}!", message);
    }

    [Fact]
    public async Task PostGreet_WithEmptyName_ReturnsBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();
        var request = new { name = "" };

        // Act
        var response = await client.PostAsJsonAsync("/api/greet", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PostGreet_WithWhitespaceName_ReturnsBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();
        var request = new { name = "   " };

        // Act
        var response = await client.PostAsJsonAsync("/api/greet", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PostGreet_WithoutName_ReturnsBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();
        var request = new { };

        // Act
        var response = await client.PostAsJsonAsync("/api/greet", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PostGreet_ReturnsBadRequestWithErrorMessage()
    {
        // Arrange
        var client = _factory.CreateClient();
        var request = new { name = "" };

        // Act
        var response = await client.PostAsJsonAsync("/api/greet", request);
        var content = await response.Content.ReadAsStringAsync();
        using var jsonDoc = JsonDocument.Parse(content);
        var root = jsonDoc.RootElement;

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.True(root.TryGetProperty("error", out var errorProperty));
        Assert.Equal("Name is required", errorProperty.GetString());
    }

    [Fact]
    public async Task PostGreet_ReturnsCorrectResponseStructure()
    {
        // Arrange
        var client = _factory.CreateClient();
        var request = new { name = "Test" };

        // Act
        var response = await client.PostAsJsonAsync("/api/greet", request);
        var content = await response.Content.ReadAsStringAsync();
        using var jsonDoc = JsonDocument.Parse(content);
        var root = jsonDoc.RootElement;

        // Assert
        Assert.True(root.TryGetProperty("message", out var messageProperty));
        Assert.NotNull(messageProperty.GetString());
    }

    [Fact]
    public async Task PostGreet_ReturnsContentTypeJson()
    {
        // Arrange
        var client = _factory.CreateClient();
        var request = new { name = "Test" };

        // Act
        var response = await client.PostAsJsonAsync("/api/greet", request);

        // Assert
        Assert.NotNull(response.Content.Headers.ContentType);
        Assert.Contains("application/json", response.Content.Headers.ContentType.ToString());
    }

    [Fact]
    public async Task PostGreet_WithSpecialCharacters_ReturnsSuccess()
    {
        // Arrange
        var client = _factory.CreateClient();
        var request = new { name = "José María" };

        // Act
        var response = await client.PostAsJsonAsync("/api/greet", request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
