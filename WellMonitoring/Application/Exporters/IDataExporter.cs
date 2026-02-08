using System.Collections.Generic;

namespace WellMonitoring.Application.Exporters
{
    /// <summary>
    /// Интерфейс для экспорта данных
    /// </summary>
    /// <typeparam name="T">Тип экспортируемых данных</typeparam>
    public interface IDataExporter<T>
    {
        /// <summary>
        /// Экспортировать данные в файл
        /// </summary>
        /// <param name="data">Данные для экспорта</param>
        /// <param name="fileName">Имя файла</param>
        void Export(IEnumerable<T> data, string fileName);

        /// <summary>
        /// Получить расширение файла
        /// </summary>
        string FileExtension { get; }
    }
}