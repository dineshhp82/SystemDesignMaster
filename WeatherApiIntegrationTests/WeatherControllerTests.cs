using FluentAssertions;

namespace WeatherApiIntegrationTests
{
    public class WeatherControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        //All HTTP requests are real but no external server needed.
        public WeatherControllerTests(CustomWebApplicationFactory customWebApplicationFactory)
        {
            _client = customWebApplicationFactory.CreateClient();//spins up an in-memory server.
        }

        [Fact]
        public async Task GetWeather_ReturnsOk_WithExpectedContent()
        {
            // Arrange
            var city = "London";

            // Act
            var response = await _client.GetAsync($"/WeatherForecast");

            response.EnsureSuccessStatusCode(); // StatusCode 200-299
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            content.Should().Be($"\"Sunny in {city}\"");  // Note: API returns JSON string
        }
    }
}
