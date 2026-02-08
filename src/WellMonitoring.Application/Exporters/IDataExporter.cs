using System.Collections.Generic;

namespace WellMonitoring.Application.Exporters
{
    public interface IDataExporter<T>
    {
        string Export(IEnumerable<T> data, string fileName);

        string FileExtension { get; }
    }
}