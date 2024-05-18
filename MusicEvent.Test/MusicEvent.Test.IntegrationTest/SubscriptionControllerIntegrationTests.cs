using Microsoft.AspNetCore.Mvc.Testing;
using MusicEvent.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MusicEvent.Test.IntegrationTest
{
    public class SubscriptionControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public SubscriptionControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAllById_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            var eventoDTO = new EventoDTO { Id = Guid.NewGuid(), Descricao = "Subscription Integration Tests GetAllById", Data = DateTime.Now };
            await client.PostAsJsonAsync("/v1/Evento/Create", eventoDTO);

            // Act
            var response = await client.GetAsync($"/v1/Subscription/GetAllById/{eventoDTO.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task CreateSubscription_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            var eventoDTO = new EventoDTO { Id = Guid.NewGuid(), Descricao = "Subscription Integration Tests Create", Data = DateTime.Now };
            await client.PostAsJsonAsync("/v1/Evento/Create", eventoDTO);

            var subscriptionDTO = new SubscriptionDTO { IdEvento = (Guid)eventoDTO.Id };

            // Act
            var response = await client.PostAsJsonAsync("/v1/Subscription/CreateSubscription", subscriptionDTO);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Delete_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = _factory.CreateClient();
            var eventoDTO = new EventoDTO { Id = Guid.NewGuid(), Descricao = "Subscription Integration Tests Delete", Data = DateTime.Now };
            await client.PostAsJsonAsync("/v1/Evento/Create", eventoDTO);

            var subscriptionDTO = new SubscriptionDTO { IdEvento = (Guid)eventoDTO.Id };
            await client.PostAsJsonAsync("/v1/Subscription/CreateSubscription", subscriptionDTO);

            // Act
            var response = await client.DeleteAsync($"/v1/Subscription/DeleteSubscription/{subscriptionDTO.IdEvento}");

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }

}
