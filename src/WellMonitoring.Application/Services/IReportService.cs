using System;
using System.Collections.Generic;
using WellMonitoring.Application.Common;
using WellMonitoring.Domain.DTOs;

namespace WellMonitoring.Application.Services
{
    public interface IReportService
    {
        List<MeasurementReportDto> GetMeasurementReport(DateTime? filterDate);

        List<MeasurementReportDto> GetMeasurementReportByRange(DateRange dateRange);
    }
}