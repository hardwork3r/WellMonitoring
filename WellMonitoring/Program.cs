using System;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using WellMonitoring.Application;
using WellMonitoring.Application.Exporters;
using WellMonitoring.Application.Services;
using WellMonitoring.Domain.DTOs;
using WellMonitoring.Infrastructure.Data;
using WellMonitoring.Infrastructure.Repositories;
using WellMonitoring.Presentation.ConsoleUI;

namespace WellMonitoring
{
    class Program
    {
        private const string ConnectionString = "Host=localhost;Port=5432;Database=well_monitoring;Username=postgres;Password=1234";

        static void Main(string[] args)
        {
            ConfigureEnvironment();

            // Создание DI контейнера
            var serviceProvider = ConfigureServices();

            // Получение и запуск приложения
            var application = serviceProvider.GetRequiredService<WellMonitoringApplication>();
            application.Run();
        }

        private static void ConfigureEnvironment()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
        }

        private static ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Infrastructure
            services.AddSingleton<IDbConnectionFactory>(
                new PostgreSqlConnectionFactory(ConnectionString));
            services.AddScoped<IMeasurementRepository, MeasurementRepository>();

            // Application
            services.AddScoped<IReportService, ReportService>();
            services.AddSingleton<IDataExporter<MeasurementReportDto>, CsvExporter>();

            // Presentation
            services.AddSingleton<IUserInterface, ConsoleInterface>();

            // Application
            services.AddTransient<WellMonitoringApplication>();

            return services.BuildServiceProvider();
        }
    }
}