using WpfMvvmEf6Example.Models;

namespace WpfMvvmEf6Example.Interfaces
{
    public interface IDialogService
    {
        bool ShowConfirmation(string title, string question);
        void ShowError(string title, string message);
        void ShowDetails(Topic editedTopic);
        void ShowMessageEditor(Post editedPost);
    }
}
