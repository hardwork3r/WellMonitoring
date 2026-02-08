using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WellMonitoring.Application;
using WellMonitoring.Application.Exporters;
using WellMonitoring.Application.Interfaces;
using WellMonitoring.Application.Services;
using WellMonitoring.Domain.DTOs;
using WellMonitoring.Infrastructure.Data;
using WellMonitoring.Infrastructure.Repositories;
using WellMonitoring.Presentation.UI;

namespace WellMonitoring
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigureEnvironment();

            var configuration = BuildConfiguration();

            var serviceProvider = ConfigureServices(configuration);

            var application = serviceProvider.GetRequiredService<WellMonitoringApplication>();
            application.Run();
        }

        private static void ConfigureEnvironment()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
        }

        private static IConfiguration BuildConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        private static ServiceProvider ConfigureServices(IConfiguration configuration)
        {
            var services = new ServiceCollection();

            services.AddSingleton(configuration);

            string connectionString = configuration.GetConnectionString("WellMonitoring");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Строка подключения 'WellMonitoring' не найдена в конфигурации");
            }

            services.AddSingleton<IDbConnectionFactory>(new PostgreSqlConnectionFactory(connectionString));
            services.AddScoped<IMeasurementRepository, MeasurementRepository>();

            services.AddScoped<IReportService, ReportService>();
            services.AddSingleton<IDataExporter<MeasurementReportDto>, CsvExporter>();

            services.AddSingleton<IUserInterface, ConsoleInterface>();

            services.AddTransient<WellMonitoringApplication>();

            return services.BuildServiceProvider();
        }
    }
}