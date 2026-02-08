using System;
using WellMonitoring.Presentation.ConsoleUI;

namespace WellMonitoring.Application
{
    /// <summary>
    /// Генератор имен файлов для экспорта
    /// </summary>
    public static class FileNameGenerator
    {
        private const string AllPeriodPrefix = "WellMeasurements_AllPeriod";
        private const string DateFilterPrefix = "WellMeasurements";
        private const string RangeFilterPrefix = "WellMeasurements_Range";

        /// <summary>
        /// Генерирует имя файла на основе даты фильтрации
        /// </summary>
        public static string Generate(DateTime? filterDate, string fileExtension)
        {
            var now = DateTime.Now;
            string timestamp = FormatTimestamp(now);
            string prefix = GetPrefix(filterDate);

            return $"{prefix}_{timestamp}{fileExtension}";
        }

        /// <summary>
        /// Генерирует имя файла на основе диапазона дат
        /// </summary>
        public static string GenerateForRange(DateRange dateRange, string fileExtension)
        {
            var now = DateTime.Now;
            string timestamp = FormatTimestamp(now);
            string prefix = GetPrefixForRange(dateRange);

            return $"{prefix}_{timestamp}{fileExtension}";
        }

        private static string FormatTimestamp(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd_HH-mm-ss");
        }

        private static string GetPrefix(DateTime? filterDate)
        {
            if (filterDate.HasValue)
            {
                return $"{DateFilterPrefix}_{filterDate.Value:yyyy-MM-dd}";
            }

            return AllPeriodPrefix;
        }

        private static string GetPrefixForRange(DateRange dateRange)
        {
            if (dateRange == null)
            {
                return AllPeriodPrefix;
            }

            return $"{RangeFilterPrefix}_{dateRange.StartDate:yyyy-MM-dd}_to_{dateRange.EndDate:yyyy-MM-dd}";
        }
    }
}