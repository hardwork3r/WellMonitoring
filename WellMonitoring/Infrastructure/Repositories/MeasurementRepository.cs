using System;
using System.Collections.Generic;
using System.Linq;
using LinqToDB;
using WellMonitoring.Domain.DTOs;
using WellMonitoring.Domain.Entities;
using WellMonitoring.Infrastructure.Data;
using WellMonitoring.Presentation.ConsoleUI;

namespace WellMonitoring.Infrastructure.Repositories
{
    public class MeasurementRepository : IMeasurementRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public MeasurementRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        public List<MeasurementReportDto> GetMeasurementReport(DateTime? filterDate)
        {
            using (var db = _connectionFactory.CreateConnection())
            {
                return GetReportData(db, filterDate, null);
            }
        }

        public List<MeasurementReportDto> GetMeasurementReportByRange(DateRange dateRange)
        {
            if (dateRange == null)
                throw new ArgumentNullException(nameof(dateRange));

            if (!dateRange.IsValid())
                throw new ArgumentException("Некорректный диапазон дат", nameof(dateRange));

            using (var db = _connectionFactory.CreateConnection())
            {
                return GetReportData(db, null, dateRange);
            }
        }

        /// <summary>
        /// Общий метод получения отчета с разными фильтрами
        /// </summary>
        private List<MeasurementReportDto> GetReportData(
            LinqToDB.Data.DataConnection db,
            DateTime? filterDate,
            DateRange dateRange)
        {
            var measurements = db.GetTable<Measurement>();
            var wells = db.GetTable<Well>();
            var departments = db.GetTable<Department>();
            var measurementTypes = db.GetTable<MeasurementType>();

            var query = from m in measurements
                        join w in wells on m.WellId equals w.Id
                        join d in departments on w.DepartmentId equals d.Id
                        join mt in measurementTypes on m.MeasurementTypeId equals mt.Id
                        select new
                        {
                            Department = d.Name,
                            WellName = w.Name,
                            MeasurementTime = m.MeasurementTime,
                            MeasurementType = mt.Name,
                            Value = m.Value
                        };

            // Применение фильтра по дате
            if (filterDate.HasValue)
            {
                var startDate = filterDate.Value.Date;
                var endDate = startDate.AddDays(1);
                query = query.Where(x => x.MeasurementTime >= startDate && x.MeasurementTime < endDate);
            }
            // Применение фильтра по диапазону
            else if (dateRange != null)
            {
                var startDate = dateRange.StartDate.Date;
                var endDate = dateRange.EndDate.Date.AddDays(1); // Включаем конечную дату полностью
                query = query.Where(x => x.MeasurementTime >= startDate && x.MeasurementTime < endDate);
            }

            // Группировка и агрегация
            var result = query
                .AsEnumerable()
                .GroupBy(x => new
                {
                    x.Department,
                    x.WellName,
                    MeasurementDate = x.MeasurementTime.Date,
                    x.MeasurementType
                })
                .Select(g => new MeasurementReportDto
                {
                    Department = g.Key.Department,
                    WellName = g.Key.WellName,
                    MeasurementDate = g.Key.MeasurementDate,
                    MeasurementType = g.Key.MeasurementType,
                    MinValue = g.Min(x => x.Value),
                    MaxValue = g.Max(x => x.Value),
                    Count = g.Count()
                })
                .OrderBy(x => x.Department)
                .ThenBy(x => x.WellName)
                .ThenBy(x => x.MeasurementDate)
                .ThenBy(x => x.MeasurementType)
                .ToList();

            return result;
        }
    }
}