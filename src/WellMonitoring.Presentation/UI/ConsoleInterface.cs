using System;
using System.Globalization;
using WellMonitoring.Application.Interfaces;
using WellMonitoring.Application.Common;

namespace WellMonitoring.Presentation.UI
{
    public class ConsoleInterface : IUserInterface
    {
        public void ShowWelcomeMessage()
        {
            Console.WriteLine("+----------------------------------------------------------+");
            Console.WriteLine("|     Система экспорта данных замеров скважин              |");
            Console.WriteLine("+----------------------------------------------------------+");
            Console.WriteLine();
        }

        public MenuOption ShowMainMenu()
        {
            Console.WriteLine("\n+------------------ ГЛАВНОЕ МЕНЮ -------------------------+");
            Console.WriteLine("|                                                         |");
            Console.WriteLine("|  1. Экспорт данных за весь период                       |");
            Console.WriteLine("|  2. Экспорт данных за конкретную дату                   |");
            Console.WriteLine("|  3. Экспорт данных за указанный период                  |");
            Console.WriteLine("|  0. Выход из программы                                  |");
            Console.WriteLine("|                                                         |");
            Console.WriteLine("+---------------------------------------------------------+");
            Console.Write("\nВыберите действие: ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                if (Enum.IsDefined(typeof(MenuOption), choice))
                {
                    return (MenuOption)choice;
                }
            }

            Console.WriteLine("Некорректный выбор. Попробуйте снова.");
            System.Threading.Thread.Sleep(1500);
            return ShowMainMenu();
        }

        public DateTime? RequestDate()
        {
            Console.WriteLine("\n+------------------- ВВОД ДАТЫ ---------------------------+");
            Console.WriteLine("  Нажмите Enter без ввода для выгрузки за весь период");
            Console.Write("  Введите дату (формат: дд.мм.гггг): ");
            string dateInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(dateInput))
            {
                Console.WriteLine("  → Выбрана выгрузка за весь период");
                return null;
            }

            if (DateTime.TryParseExact(dateInput, "dd.MM.yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
            {
                Console.WriteLine($"  → Выбрана дата: {parsedDate:dd.MM.yyyy}");
                return parsedDate;
            }

            Console.WriteLine("  ⚠ Некорректный формат даты.");
            Console.Write("  Попробовать снова? (y/n): ");

            if (Console.ReadLine()?.ToLower() == "y")
            {
                return RequestDate();
            }

            Console.WriteLine("  → По умолчанию будет выгрузка за весь период");
            return null;
        }

        public DateRange RequestDateRange()
        {
            Console.WriteLine("\n+--------------- ВВОД ДИАПАЗОНА ДАТ ----------------------+");
            Console.WriteLine("  Нажмите Enter без ввода для выгрузки за весь период");
            Console.WriteLine("  Формат ввода: дд.мм.гггг - дд.мм.гггг");
            Console.WriteLine("  Пример: 01.02.2026 - 05.02.2026");
            Console.Write("\n  Введите диапазон: ");

            string rangeInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(rangeInput))
            {
                Console.WriteLine("  → Выбрана выгрузка за весь период");
                return null;
            }

            var parts = rangeInput.Split(new[] { '-', '–', '—' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2)
            {
                Console.WriteLine("  ⚠ Некорректный формат диапазона.");
                return RetryOrNull();
            }

            if (!DateTime.TryParseExact(parts[0].Trim(), "dd.MM.yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate))
            {
                Console.WriteLine($"  ⚠ Некорректная начальная дата: {parts[0].Trim()}");
                return RetryOrNull();
            }

            if (!DateTime.TryParseExact(parts[1].Trim(), "dd.MM.yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate))
            {
                Console.WriteLine($"  ⚠ Некорректная конечная дата: {parts[1].Trim()}");
                return RetryOrNull();
            }

            var dateRange = new DateRange(startDate, endDate);

            if (!dateRange.IsValid())
            {
                Console.WriteLine($"  ⚠ Ошибка: начальная дата ({startDate:dd.MM.yyyy}) не может быть позже конечной ({endDate:dd.MM.yyyy})");
                return RetryOrNull();
            }

            Console.WriteLine($"  → Выбран диапазон: {dateRange}");
            Console.WriteLine($"  → Количество дней: {dateRange.DaysCount}");

            return dateRange;
        }

        private DateRange RetryOrNull()
        {
            Console.Write("  Попробовать снова? (y/n): ");

            if (Console.ReadLine()?.ToLower() == "y")
            {
                return RequestDateRange();
            }

            Console.WriteLine("  → По умолчанию будет выгрузка за весь период");
            return null;
        }

        public void ShowSuccessMessage(string filePath, int recordCount)
        {
            Console.WriteLine("\n+------------------- РЕЗУЛЬТАТ ---------------------------+");
            Console.WriteLine("|  Экспорт завершен успешно!                              |");
            Console.WriteLine("+---------------------------------------------------------+");
            Console.WriteLine($"\nФайл сохранен: {filePath}");
            Console.WriteLine($"Всего записей: {recordCount}");
        }

        public void ShowErrorMessage(string message, Exception exception = null)
        {
            Console.WriteLine("\n+-------------------- ОШИБКА -----------------------------+");
            Console.WriteLine($"|  {message}");
            Console.WriteLine("+---------------------------------------------------------+");

            if (exception != null)
            {
                Console.WriteLine($"\nДетали: {exception.Message}");

                if (exception.InnerException != null)
                {
                    Console.WriteLine($"Внутренняя ошибка: {exception.InnerException.Message}");
                }
            }
        }

        public void ShowExitMessage()
        {
            Console.WriteLine("\n+----------------------------------------------------------+");
            Console.WriteLine("|                                                          |");
            Console.WriteLine("|           Спасибо за использование программы!            |");
            Console.WriteLine("|                                                          |");
            Console.WriteLine("+----------------------------------------------------------+\n");
        }

        public void WaitForKeyPress()
        {
            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey(true);
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}