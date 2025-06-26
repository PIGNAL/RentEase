using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RentEase.Application.Contracts;

namespace RentEase.Infrastructure.Services
{
    public class CarMaintenanceWorker : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<CarMaintenanceWorker> _logger;

        public CarMaintenanceWorker(
            IServiceScopeFactory scopeFactory,
            ILogger<CarMaintenanceWorker> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var carMaintenanceService = scope.ServiceProvider.GetRequiredService<ICarMaintenanceService>();
                        var from = DateTime.UtcNow;
                        var to = from.AddMonths(2);

                        await carMaintenanceService.ScheduleServicesForAllCars(from, to);
                    }

                    _logger.LogInformation("Maintenance services successfully scheduled for all cars.");
                    await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error scheduling maintenance services.");
                    await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
                }
            }
        }
    }
}
