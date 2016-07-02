using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfMvvmEf6Example.Interfaces;
using WpfMvvmEf6Example.Models;
using WpfMvvmEf6Example.Services;
using WpfMvvmEf6Example.ViewModel;
using WpfMvvmEf6Example.Views;

namespace WpfMvvmEf6Example.Services
{
    public class DialogService : IDialogService
    {
        public bool ShowConfirmation(string title, string question)
        {
            return MessageBox.Show(question, title, MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK;
        }

        public void ShowError(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void ShowDetails(Topic editedTopic)
        {
            ModalDialogService<DetailsWindow, Topic>
                .ShowDialog(editedTopic, ViewModelLocator.SetEditTopicMessage);
        }

        public void ShowMessageEditor(Post editedPost)
        {
            ModalDialogService<MessageEditWindow, Post>
                .ShowDialog(editedPost, ViewModelLocator.SetEditPostMessage);
        }
    }
}
