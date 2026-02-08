using System;

namespace WellMonitoring.Domain.DTOs
{
    public class MeasurementReportDto
    {
        public string Department { get; set; }
        public string WellName { get; set; }
        public DateTime MeasurementDate { get; set; }
        public string MeasurementType { get; set; }
        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }
        public int Count { get; set; }

        public override string ToString()
        {
            return $"{Department} | {WellName} | {MeasurementDate:dd.MM.yyyy} | {MeasurementType} | Min: {MinValue:F4} | Max: {MaxValue:F4} | Count: {Count}";
        }
    }
}