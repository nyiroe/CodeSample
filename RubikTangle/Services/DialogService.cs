using RubikTangle.Interfaces;
using System.Windows;

namespace RubikTangle.Services
{
    public class DialogService : IDialogService
    {
        public void ShowMessage(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void ShowError(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
