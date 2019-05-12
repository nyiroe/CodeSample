using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfMvvmEf6Example.ViewModel;

namespace WpfMvvmEf6Example.Services
{
    public static class ModalDialogService<T, U> where T : Window, new()
    {
        public static void ShowDialog(U parameter, string token)
        {
            T window = new T();
            window.Owner = ViewModelLocator.ActiveWindow;
            Messenger.Default.Send<U>(parameter, token);
            window.ShowDialog();
        }
    }
}
