using Microsoft.AspNetCore.Mvc.Testing;
using MusicEvent.Application.DTO;
using System.Net.Http.Json;

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

        [Fact]
        public async Task Post_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            var eventoDTO = new EventoDTO { /* set properties here */ };

            // Act
            var response = await client.PostAsJsonAsync("/v1/Evento/Create", eventoDTO);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Update_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            var eventoDTO = new EventoDTO { /* set properties here */ };

            // Act
            var response = await client.PutAsJsonAsync("/v1/Evento/Update", eventoDTO);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Delete_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            var id = Guid.NewGuid();

            // Act
            var response = await client.DeleteAsync($"/v1/Evento/Delete/{id}");

            // Assert
            response.EnsureSuccessStatusCode();
        }

    }
}
