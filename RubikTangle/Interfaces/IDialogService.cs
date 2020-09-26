namespace RubikTangle.Interfaces
{
    public interface IDialogService
    {
        void ShowMessage(string title, string message);
        void ShowError(string title, string message);
    }
}
