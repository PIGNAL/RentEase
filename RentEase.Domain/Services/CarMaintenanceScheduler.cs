public class CarMaintenanceScheduler
{
    public IEnumerable<Service> GenerateScheduledServices(Car car, DateTime from, DateTime to)
    {
        var services = new List<Service>();
        var current = from;

        while (current < to)
        {
            services.Add(new Service
            {
                CarId = car.Id,
                Date = current
            });
            current = current.AddMonths(2);
        }

        return services;
    }
}