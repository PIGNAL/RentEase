using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using RentEase.Application.Contracts;
using RentEase.Infrastructure.Services;
using Xunit;

namespace RentEase.UnitTests.Infrastructure.Infrastructure.Services
{
    public class CarMaintenanceWorkerTests
    {
        [Fact]
        public async Task ExecuteAsync_SchedulesMaintenanceAndLogsInformation()
        {
            // Arrange
            var carMaintenanceServiceMock = new Mock<ICarMaintenanceService>();
            carMaintenanceServiceMock
                .Setup(s => s.ScheduleServicesForAllCars(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(Task.CompletedTask);

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock
                .Setup(sp => sp.GetService(typeof(ICarMaintenanceService)))
                .Returns(carMaintenanceServiceMock.Object);

            var scopeMock = new Mock<IServiceScope>();
            scopeMock.SetupGet(s => s.ServiceProvider).Returns(serviceProviderMock.Object);

            var scopeFactoryMock = new Mock<IServiceScopeFactory>();
            scopeFactoryMock
                .Setup(f => f.CreateScope())
                .Returns(scopeMock.Object);

            var loggerMock = new Mock<ILogger<CarMaintenanceWorker>>();

            var worker = new CarMaintenanceWorker(scopeFactoryMock.Object, loggerMock.Object);

            using var cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromSeconds(1)); 

            // Act
            await worker.StartAsync(cts.Token);

            // Assert
            carMaintenanceServiceMock.Verify(
                s => s.ScheduleServicesForAllCars(It.IsAny<DateTime>(), It.IsAny<DateTime>()),
                Times.AtLeastOnce);

            loggerMock.Verify(
                l => l.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Maintenance services successfully scheduled for all cars.")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.AtLeastOnce);
        }

        [Fact]
        public async Task ExecuteAsync_LogsErrorOnException()
        {
            // Arrange
            var carMaintenanceServiceMock = new Mock<ICarMaintenanceService>();
            carMaintenanceServiceMock
                .Setup(s => s.ScheduleServicesForAllCars(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ThrowsAsync(new Exception("Test exception"));

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock
                .Setup(sp => sp.GetService(typeof(ICarMaintenanceService)))
                .Returns(carMaintenanceServiceMock.Object);

            var scopeMock = new Mock<IServiceScope>();
            scopeMock.SetupGet(s => s.ServiceProvider).Returns(serviceProviderMock.Object);

            var scopeFactoryMock = new Mock<IServiceScopeFactory>();
            scopeFactoryMock
                .Setup(f => f.CreateScope())
                .Returns(scopeMock.Object);

            var loggerMock = new Mock<ILogger<CarMaintenanceWorker>>();

            var worker = new CarMaintenanceWorker(scopeFactoryMock.Object, loggerMock.Object);

            using var cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromSeconds(1));

            // Act
            await worker.StartAsync(cts.Token);

            // Assert
            loggerMock.Verify(
                l => l.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Error scheduling maintenance services.")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.AtLeastOnce);
        }
    }
}
