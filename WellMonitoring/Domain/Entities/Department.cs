using System;
using LinqToDB.Mapping;

namespace WellMonitoring.Domain.Entities
{
    [Table(Name = "departments")]
    public class Department
    {
        [Column(Name = "id"), PrimaryKey, Identity]
        public int Id { get; set; }

        [Column(Name = "name"), NotNull]
        public string Name { get; set; }
    }
}