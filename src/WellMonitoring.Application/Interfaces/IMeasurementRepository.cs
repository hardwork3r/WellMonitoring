using System;
using System.Collections.Generic;
using WellMonitoring.Domain.DTOs;
using WellMonitoring.Application.Common;

namespace WellMonitoring.Application.Interfaces
{
    public interface IMeasurementRepository
    {
        List<MeasurementReportDto> GetMeasurementReport(DateTime? filterDate);
        List<MeasurementReportDto> GetMeasurementReportByRange(DateRange dateRange);
    }
}