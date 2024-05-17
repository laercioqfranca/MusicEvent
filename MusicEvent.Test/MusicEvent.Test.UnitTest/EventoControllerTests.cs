using Moq;
using MusicEvent.Web.Controllers;
using MusicEvent.Application.Interfaces;
using MusicEvent.Core.Interfaces;
using MusicEvent.Core.Notifications;
using Microsoft.AspNetCore.Mvc;
using MusicEvent.Application.ViewModels;
using MusicEvent.Application.DTO;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace MusicEvent.Test.UnitTest
{
    public class EventoControllerTests
    {
        private readonly Mock<IEventoAppService> _mockAppService;
        private readonly Mock<IMediatorHandler> _mockMediatorHandler;
        private readonly EventoController _controller;
        private readonly Mock<DomainNotificationHandler> _mockNotificationHandler;


        public EventoControllerTests()
        {
            _mockAppService = new Mock<IEventoAppService>();
            _mockNotificationHandler = new Mock<DomainNotificationHandler>();
            _mockMediatorHandler = new Mock<IMediatorHandler>();
            _controller = new EventoController(_mockAppService.Object, _mockNotificationHandler.Object, _mockMediatorHandler.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WhenCalled()
        {
            // Arrange
            var expectedList = new List<EventoViewModel> { new EventoViewModel(), new EventoViewModel() };
            _mockAppService.Setup(service => service.GetAll()).ReturnsAsync(expectedList);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = okResult.Value;
            var data = returnValue?.GetType().GetProperty("data")?.GetValue(returnValue);
            var success = returnValue?.GetType().GetProperty("success")?.GetValue(returnValue);
            Assert.NotNull(data);
            Assert.NotNull(success);
            Assert.True((bool)success);
            Assert.Equal(expectedList, data as List<EventoViewModel>);
            _mockAppService.Verify(service => service.GetAll(), Times.Once);
        }

        [Theory]
        [InlineData("594F6E84-85B0-4141-825F-00B068F74DEC")]
        public async Task GetById_ReturnsOkResult_WhenCalled(Guid id)
        {
            // Arrange
            var expectedObject = new EventoViewModel(){};
            _mockAppService.Setup(service => service.GetById(id)).ReturnsAsync(expectedObject);

            // Act
            var result = await _controller.GetById(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = okResult.Value;
            var data = returnValue?.GetType().GetProperty("data")?.GetValue(returnValue);
            var success = returnValue?.GetType().GetProperty("success")?.GetValue(returnValue);
            Assert.NotNull(data);
            Assert.NotNull(success);
            Assert.True((bool)success);
            Assert.Equal(expectedObject, data as EventoViewModel);
        }

    }
}
