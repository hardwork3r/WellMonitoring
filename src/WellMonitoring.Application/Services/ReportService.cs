using System;
using System.Collections.Generic;
using WellMonitoring.Domain.DTOs;
using WellMonitoring.Application.Interfaces;
using WellMonitoring.Application.Common;

namespace WellMonitoring.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly IMeasurementRepository _measurementRepository;

        public ReportService(IMeasurementRepository measurementRepository)
        {
            _measurementRepository = measurementRepository ?? throw new ArgumentNullException(nameof(measurementRepository));
        }

        public List<MeasurementReportDto> GetMeasurementReport(DateTime? filterDate)
        {
            return _measurementRepository.GetMeasurementReport(filterDate);
        }

        public List<MeasurementReportDto> GetMeasurementReportByRange(DateRange dateRange)
        {
            return _measurementRepository.GetMeasurementReportByRange(dateRange);
        }
    }
}