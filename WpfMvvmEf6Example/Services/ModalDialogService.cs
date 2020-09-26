using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using WpfMvvmEf6Example.ViewModel;

namespace WpfMvvmEf6Example.Services
{
    public static class ModalDialogService<T, U> where T : Window, new()
    {
        public static void ShowDialog(U parameter, string token)
        {
            T window = new T
            {
                Owner = ViewModelLocator.ActiveWindow
            };

            Messenger.Default.Send<U>(parameter, token);
            window.ShowDialog();
        }
    }
}
