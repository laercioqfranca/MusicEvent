using Microsoft.AspNetCore.Mvc.Testing;
using MusicEvent.Application.DTO;
using System.Net.Http.Json;

namespace MusicEvent.Test.IntegrationTest
{
    public class EventoControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        //private Guid IdEvento = Guid.NewGuid();
        private Guid IdEvento = Guid.Parse("4F6C0DE7-20EF-4A2A-8EE7-D30E28B4B718");
        public EventoControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }


        [Fact, Priority(5)]
        public async Task Post_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            var eventoDTO = new EventoDTO { Id = IdEvento, Descricao = "Evento Integration Tests Post", Data = DateTime.Now };

            // Act
            var response = await client.PostAsJsonAsync("/v1/Evento/Create", eventoDTO);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact, Priority(4)]
        public async Task GetAll_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/v1/Evento/GetAll");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact, Priority(3)]
        public async Task GetById_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            var id = IdEvento;

            // Act
            var response = await client.GetAsync($"/v1/Evento/GetById/{id}");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact, Priority(2)]
        public async Task Update_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            var eventoDTO = new EventoDTO { Id = IdEvento, Descricao = "Evento Integration Tests Put", Data = DateTime.Now };

            // Act
            var response = await client.PutAsJsonAsync("/v1/Evento/Update", eventoDTO);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact, Priority(1)]
        public async Task Delete_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            var id = IdEvento;

            // Act
            var response = await client.DeleteAsync($"/v1/Evento/Delete/{id}");

            // Assert
            response.EnsureSuccessStatusCode();
        }

    }
}
