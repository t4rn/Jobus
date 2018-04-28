using AutoFixture;
using FluentAssertions;
using Jobus.Api.Controllers;
using Jobus.Core.Services.WsClients;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Jobus.Tests.Api.Controllers
{
    public class InvestControllerTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<ILogger<InvestController>> _loggerMock;
        private readonly Mock<IActionContextAccessor> _actionContextMock;
        private readonly Mock<IWsClientService> _wsClientServiceMock;
        private readonly InvestController _controller;

        public InvestControllerTests()
        {
            _fixture = new Fixture();
            _fixture.Customize(new RandomBooleanSequenceCustomization());

            _loggerMock = new Mock<ILogger<InvestController>>();
            _actionContextMock = new Mock<IActionContextAccessor>();
            _wsClientServiceMock = new Mock<IWsClientService>();
            _controller = new InvestController(_loggerMock.Object, _actionContextMock.Object, 
                _wsClientServiceMock.Object);

            // for mocking headers
            _controller.ControllerContext = new ControllerContext();
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
        }

        [Fact(DisplayName = "Ping")]
        public void Ping()
        {
            // Arrange

            // Act
            IActionResult actionResult = _controller.Ping();

            // Assert
            actionResult.Should().NotBeNull().And.BeOfType<OkObjectResult>();
            (actionResult as OkObjectResult).Value.Should().NotBeNull().And.BeOfType<string>();
            string message = (actionResult as OkObjectResult).Value.ToString();
            message.Should().StartWith("Ping at ");

        }
    }
}
