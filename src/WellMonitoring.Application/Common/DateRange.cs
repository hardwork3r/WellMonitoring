using System;

namespace WellMonitoring.Application.Common
{
    public class DateRange
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DateRange(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public bool IsValid()
        {
            return StartDate <= EndDate;
        }

        public int DaysCount => (EndDate - StartDate).Days + 1;

        public override string ToString()
        {
            return $"{StartDate:dd.MM.yyyy} - {EndDate:dd.MM.yyyy}";
        }
    }
}