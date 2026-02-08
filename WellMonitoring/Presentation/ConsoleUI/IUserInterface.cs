using System;

namespace WellMonitoring.Presentation.ConsoleUI
{
    /// <summary>
    /// Интерфейс пользовательского интерфейса
    /// </summary>
    public interface IUserInterface
    {
        /// <summary>
        /// Отобразить приветственное сообщение
        /// </summary>
        void ShowWelcomeMessage();

        /// <summary>
        /// Показать главное меню и получить выбор пользователя
        /// </summary>
        /// <returns>Выбранная опция меню</returns>
        MenuOption ShowMainMenu();

        /// <summary>
        /// Запросить дату у пользователя
        /// </summary>
        /// <returns>Введенная дата или null</returns>
        DateTime? RequestDate();

        /// <summary>
        /// Запросить период (диапазон дат) у пользователя
        /// </summary>
        /// <returns>Период или null</returns>
        DateRange RequestDateRange();

        /// <summary>
        /// Показать сообщение об успехе
        /// </summary>
        void ShowSuccessMessage(string filePath, int recordCount);

        /// <summary>
        /// Показать сообщение об ошибке
        /// </summary>
        void ShowErrorMessage(string message, Exception exception = null);

        /// <summary>
        /// Ожидать нажатия клавиши
        /// </summary>
        void WaitForKeyPress();

        /// <summary>
        /// Очистить консоль
        /// </summary>
        void Clear();
    }

    /// <summary>
    /// Опции главного меню
    /// </summary>
    public enum MenuOption
    {
        ExportAllData = 1,
        ExportByDate = 2,
        ExportByDateRange = 3,
        Exit = 0
    }

    /// <summary>
    /// Диапазон дат для фильтрации
    /// </summary>
    public class DateRange
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DateRange(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        /// <summary>
        /// Проверка корректности диапазона
        /// </summary>
        public bool IsValid()
        {
            return StartDate <= EndDate;
        }

        /// <summary>
        /// Количество дней в диапазоне
        /// </summary>
        public int DaysCount => (EndDate - StartDate).Days + 1;

        public override string ToString()
        {
            return $"{StartDate:dd.MM.yyyy} - {EndDate:dd.MM.yyyy}";
        }
    }
}