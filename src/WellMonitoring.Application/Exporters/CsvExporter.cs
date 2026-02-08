using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using WellMonitoring.Domain.DTOs;

namespace WellMonitoring.Application.Exporters
{
    public class CsvExporter : IDataExporter<MeasurementReportDto>
    {
        private const string Delimiter = ";";

        public string FileExtension => ".csv";

        public string Export(IEnumerable<MeasurementReportDto> data, string fileName)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("Имя файла не может быть пустым", nameof(fileName));

            string exeDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            string projectDirectory = Path.GetFullPath(Path.Combine(exeDirectory, "..", "..", ".."));

            string fullPath = Path.Combine(projectDirectory, fileName);

            using (var writer = new StreamWriter(fullPath, false, Encoding.UTF8))
            {
                WriteHeader(writer);

                foreach (var item in data)
                {
                    WriteDataRow(writer, item);
                }
            }

            return fullPath;
        }

        private void WriteHeader(StreamWriter writer)
        {
            var headers = new[]
            {
                "Подразделение",
                "Название скважины",
                "Дата замера",
                "Тип замера",
                "Минимальное значение замера",
                "Максимальное значение замера",
                "Количество замеров"
            };

            writer.WriteLine(string.Join(Delimiter, headers));
        }

        private void WriteDataRow(StreamWriter writer, MeasurementReportDto item)
        {
            var values = new[]
            {
                item.Department,
                item.WellName,
                item.MeasurementDate.ToString("dd.MM.yyyy"),
                item.MeasurementType,
                item.MinValue.ToString("F4"),
                item.MaxValue.ToString("F4"),
                item.Count.ToString()
            };

            writer.WriteLine(string.Join(Delimiter, values));
        }
    }
}