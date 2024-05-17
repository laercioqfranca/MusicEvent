using Microsoft.AspNetCore.Mvc.Testing;

namespace MusicEvent.Test.IntegrationTest
{
    public class EventoControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public EventoControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAll_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/v1/Evento/GetAll");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetById_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            var id = Guid.NewGuid();

            // Act
            var response = await client.GetAsync($"/v1/Evento/GetById/{id}");

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
