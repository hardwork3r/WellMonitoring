using System;
using System.Collections.Generic;
using WellMonitoring.Domain.DTOs;
using WellMonitoring.Presentation.ConsoleUI;

namespace WellMonitoring.Application.Services
{
    /// <summary>
    /// Сервис для работы с отчетами
    /// </summary>
    public interface IReportService
    {
        /// <summary>
        /// Получить отчет по замерам
        /// </summary>
        /// <param name="filterDate">Дата фильтрации</param>
        /// <returns>Данные отчета</returns>
        List<MeasurementReportDto> GetMeasurementReport(DateTime? filterDate);

        /// <summary>
        /// Получить отчет по замерам за диапазон дат
        /// </summary>
        /// <param name="dateRange">Диапазон дат</param>
        /// <returns>Данные отчета</returns>
        List<MeasurementReportDto> GetMeasurementReportByRange(DateRange dateRange);
    }
}