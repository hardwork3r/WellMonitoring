using System;
using System.IO;
using WellMonitoring.Application.Exporters;
using WellMonitoring.Application.Services;
using WellMonitoring.Domain.DTOs;
using WellMonitoring.Presentation.ConsoleUI;

namespace WellMonitoring.Application
{
    /// <summary>
    /// Главный класс приложения, управляющий основным циклом работы
    /// </summary>
    public class WellMonitoringApplication
    {
        private readonly IReportService _reportService;
        private readonly IDataExporter<MeasurementReportDto> _exporter;
        private readonly IUserInterface _ui;
        private bool _isRunning;

        public WellMonitoringApplication(
            IReportService reportService,
            IDataExporter<MeasurementReportDto> exporter,
            IUserInterface ui)
        {
            _reportService = reportService ?? throw new ArgumentNullException(nameof(reportService));
            _exporter = exporter ?? throw new ArgumentNullException(nameof(exporter));
            _ui = ui ?? throw new ArgumentNullException(nameof(ui));
        }

        public void Run()
        {
            Initialize();

            while (_isRunning)
            {
                try
                {
                    RunMainLoop();
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                }
            }

            Shutdown();
        }

        private void Initialize()
        {
            _isRunning = true;
            _ui.Clear();
            _ui.ShowWelcomeMessage();
        }

        private void RunMainLoop()
        {
            MenuOption selectedOption = _ui.ShowMainMenu();
            ProcessMenuOption(selectedOption);
        }

        private void ProcessMenuOption(MenuOption option)
        {
            switch (option)
            {
                case MenuOption.ExportAllData:
                    ExportData(filterDate: null);
                    break;

                case MenuOption.ExportByDate:
                    ExportDataByDate();
                    break;

                case MenuOption.ExportByDateRange:
                    ExportDataByDateRange();
                    break;

                case MenuOption.Exit:
                    _isRunning = false;
                    break;

                default:
                    _ui.ShowErrorMessage("Неизвестная опция меню");
                    break;
            }

            if (_isRunning && option != MenuOption.Exit)
            {
                WaitAndRefresh();
            }
        }

        private void ExportDataByDate()
        {
            DateTime? date = _ui.RequestDate();

            if (!date.HasValue)
            {
                Console.WriteLine("\nДата не указана. Будет выполнена выгрузка за весь период.");
                System.Threading.Thread.Sleep(1000);
            }

            ExportData(date);
        }

        private void ExportDataByDateRange()
        {
            DateRange dateRange = _ui.RequestDateRange();

            if (dateRange == null)
            {
                Console.WriteLine("\nДиапазон не указан. Будет выполнена выгрузка за весь период.");
                System.Threading.Thread.Sleep(1000);
                ExportData(filterDate: null);
            }
            else
            {
                ExportDataByRange(dateRange);
            }
        }

        private void ExportData(DateTime? filterDate)
        {
            try
            {
                Console.WriteLine("\nЗагрузка данных...");
                var reportData = _reportService.GetMeasurementReport(filterDate);

                if (reportData.Count == 0)
                {
                    _ui.ShowErrorMessage("Данные не найдены за указанный период");
                    return;
                }

                Console.WriteLine($"Загружено записей: {reportData.Count}");
                Console.WriteLine("Экспорт в файл...");

                string fileName = FileNameGenerator.Generate(filterDate, _exporter.FileExtension);
                _exporter.Export(reportData, fileName);

                _ui.ShowSuccessMessage(Path.GetFullPath(fileName), reportData.Count);
            }
            catch (Exception ex)
            {
                _ui.ShowErrorMessage("Ошибка при экспорте данных", ex);
            }
        }

        private void ExportDataByRange(DateRange dateRange)
        {
            try
            {
                Console.WriteLine($"\nЗагрузка данных за период: {dateRange}");
                var reportData = _reportService.GetMeasurementReportByRange(dateRange);

                if (reportData.Count == 0)
                {
                    _ui.ShowErrorMessage("Данные не найдены за указанный период");
                    return;
                }

                Console.WriteLine($"Загружено записей: {reportData.Count}");
                Console.WriteLine("Экспорт в файл...");

                string fileName = FileNameGenerator.GenerateForRange(dateRange, _exporter.FileExtension);
                _exporter.Export(reportData, fileName);

                _ui.ShowSuccessMessage(Path.GetFullPath(fileName), reportData.Count);
            }
            catch (Exception ex)
            {
                _ui.ShowErrorMessage("Ошибка при экспорте данных", ex);
            }
        }

        private void HandleError(Exception ex)
        {
            _ui.ShowErrorMessage("Произошла ошибка при выполнении программы", ex);
            _ui.WaitForKeyPress();
        }

        private void WaitAndRefresh()
        {
            _ui.WaitForKeyPress();
            _ui.Clear();
            _ui.ShowWelcomeMessage();
        }

        private void Shutdown()
        {
            _ui.Clear();
            ShowExitMessage();
        }

        private void ShowExitMessage()
        {
            Console.WriteLine("\n+----------------------------------------------------------+");
            Console.WriteLine("|                                                          |");
            Console.WriteLine("|           Спасибо за использование программы!            |");
            Console.WriteLine("|                                                          |");
            Console.WriteLine("+----------------------------------------------------------+\n");
        }
    }
}