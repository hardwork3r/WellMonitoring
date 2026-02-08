using System;
using System.Collections.Generic;
using WellMonitoring.Domain.DTOs;
using WellMonitoring.Presentation.ConsoleUI;

namespace WellMonitoring.Infrastructure.Repositories
{
    /// <summary>
    /// Репозиторий для работы с замерами
    /// </summary>
    public interface IMeasurementRepository
    {
        /// <summary>
        /// Получить отчет по замерам за указанный период
        /// </summary>
        /// <param name="filterDate">Дата фильтрации (null = за весь период)</param>
        /// <returns>Список данных отчета</returns>
        List<MeasurementReportDto> GetMeasurementReport(DateTime? filterDate);

        /// <summary>
        /// Получить отчет по замерам за диапазон дат
        /// </summary>
        /// <param name="dateRange">Диапазон дат</param>
        /// <returns>Список данных отчета</returns>
        List<MeasurementReportDto> GetMeasurementReportByRange(DateRange dateRange);
    }
}