using Microsoft.AspNetCore.Mvc;
using Moq;
using MusicEvent.Application.Interfaces;
using MusicEvent.Application.ViewModels;
using MusicEvent.Core.Interfaces;
using MusicEvent.Core.Notifications;
using MusicEvent.Web.Controllers;

namespace MusicEvent.Test.UnitTest
{
    public class SubscriptionControllerTests
    {
        private readonly Mock<ISubscriptionAppService> _mockAppService;
        private readonly Mock<IMediatorHandler> _mockMediatorHandler;
        private readonly SubscriptionController _controller;
        private readonly Mock<DomainNotificationHandler> _mockNotificationHandler;

        public SubscriptionControllerTests()
        {
            _mockAppService = new Mock<ISubscriptionAppService>();
            _mockNotificationHandler = new Mock<DomainNotificationHandler>();
            _mockMediatorHandler = new Mock<IMediatorHandler>();
            _controller = new SubscriptionController(_mockAppService.Object, _mockNotificationHandler.Object, _mockMediatorHandler.Object);
        }

        [Theory]
        [InlineData("594F6E84-85B0-4141-825F-00B068F74DEC")]
        public async Task GetAllById_ReturnsOkResult_WhenCalled(Guid id)
        {
            // Arrange
            var expectedList = new List<EventoViewModel> { new EventoViewModel(), new EventoViewModel() };
            _mockAppService.Setup(service => service.GetAllById(id)).ReturnsAsync(expectedList);

            // Act
            var result = await _controller.GetAllById(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = okResult.Value;
            var data = returnValue?.GetType().GetProperty("data")?.GetValue(returnValue);
            var success = returnValue?.GetType().GetProperty("success")?.GetValue(returnValue);
            Assert.NotNull(data);
            Assert.NotNull(success);
            Assert.True((bool)success);
            Assert.Equal(expectedList, data as List<EventoViewModel>);
        }

    }

}
