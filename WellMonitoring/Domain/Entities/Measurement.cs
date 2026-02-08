using System;
using LinqToDB.Mapping;

namespace WellMonitoring.Domain.Entities
{
    [Table(Name = "measurements")]
    public class Measurement
    {
        [Column(Name = "id"), PrimaryKey, Identity]
        public long Id { get; set; }

        [Column(Name = "well_id"), NotNull]
        public int WellId { get; set; }

        [Column(Name = "measurement_type_id"), NotNull]
        public int MeasurementTypeId { get; set; }

        [Column(Name = "value"), NotNull]
        public decimal Value { get; set; }

        [Column(Name = "measurement_time"), NotNull]
        public DateTime MeasurementTime { get; set; }
    }
}