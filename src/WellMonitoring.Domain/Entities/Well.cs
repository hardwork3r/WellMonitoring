using System;
using LinqToDB.Mapping;

namespace WellMonitoring.Domain.Entities
{
    [Table(Name = "wells")]
    public class Well
    {
        [Column(Name = "id"), PrimaryKey, Identity]
        public int Id { get; set; }

        [Column(Name = "name"), NotNull]
        public string Name { get; set; }

        [Column(Name = "commissioning_date"), NotNull]
        public DateTime CommissioningDate { get; set; }

        [Column(Name = "department_id"), NotNull]
        public int DepartmentId { get; set; }
    }
}