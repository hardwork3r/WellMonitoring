using LinqToDB.Mapping;

namespace WellMonitoring.Domain.Entities
{
    [Table(Name = "measurement_types")]
    public class MeasurementType
    {
        [Column(Name = "id"), PrimaryKey, Identity]
        public int Id { get; set; }

        [Column(Name = "name"), NotNull]
        public string Name { get; set; }

        [Column(Name = "description")]
        public string Description { get; set; }
    }
}