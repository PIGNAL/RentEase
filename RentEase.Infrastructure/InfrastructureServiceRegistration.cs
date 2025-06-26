using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentEase.Application.Contracts;
using RentEase.Application.Contracts.Infrastructure;
using RentEase.Application.Contracts.Persistence;
using RentEase.Application.Models;
using RentEase.Infrastructure.Email;
using RentEase.Infrastructure.Persistence;
using RentEase.Infrastructure.Repositories;
using RentEase.Infrastructure.Services;

namespace RentEase.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RentEaseDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));


            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<ICarMaintenanceService, CarMaintenanceService>();
            services.AddHostedService<CarMaintenanceWorker>();
            return services;
        }
    }
}
