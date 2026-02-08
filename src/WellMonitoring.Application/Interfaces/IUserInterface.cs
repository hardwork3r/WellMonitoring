using System;
using WellMonitoring.Application.Common;

namespace WellMonitoring.Application.Interfaces
{
    public interface IUserInterface
    {
        void ShowWelcomeMessage();

        MenuOption ShowMainMenu();

        DateTime? RequestDate();

        DateRange RequestDateRange();

        void ShowSuccessMessage(string filePath, int recordCount);

        void ShowErrorMessage(string message, Exception exception = null);

        void ShowExitMessage();

        void WaitForKeyPress();

        void Clear();
    }
    public enum MenuOption
    {
        ExportAllData = 1,
        ExportByDate = 2,
        ExportByDateRange = 3,
        Exit = 0
    }
}