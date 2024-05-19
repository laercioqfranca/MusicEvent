using Microsoft.AspNetCore.Mvc.Testing;
using MusicEvent.Application.DTO;
using MusicEvent.Domain.Interfaces.Infra.Data.Repositories;
using System.Net.Http.Json;
using System.Collections.Generic;

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
        public async Task Post_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            var eventoDTO = new EventoDTO { Id = Guid.NewGuid(), Descricao = "Evento Integration Tests Post", Data = DateTime.Now };

            // Act
            var response = await client.PostAsJsonAsync("/v1/Evento/Create", eventoDTO);

            // Assert
            response.EnsureSuccessStatusCode();
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
            var eventoDTO = new EventoDTO { Id = Guid.NewGuid(), Descricao = "Evento Integration Tests GetById", Data = DateTime.Now };
            await client.PostAsJsonAsync("/v1/Evento/Create", eventoDTO);

            // Act
            var response = await client.GetAsync($"/v1/Evento/GetById/{eventoDTO.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Update_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            var eventoDTO = new EventoDTO { Id = Guid.NewGuid(), Descricao = "Evento Integration Tests Put", Data = DateTime.Now };
            await client.PostAsJsonAsync("/v1/Evento/Create", eventoDTO);

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
            var eventoDTO = new EventoDTO { Id = Guid.NewGuid(), Descricao = "Evento Integration Tests Delete", Data = DateTime.Now };
            await client.PostAsJsonAsync("/v1/Evento/Create", eventoDTO);

            // Act
            var response = await client.DeleteAsync($"/v1/Evento/Delete/{eventoDTO.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
        }

    }
}
