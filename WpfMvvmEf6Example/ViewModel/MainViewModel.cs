using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WpfMvvmEf6Example.Interfaces;
using WpfMvvmEf6Example.Models;

namespace WpfMvvmEf6Example.ViewModel
{
    public class MainViewModel : ExtendedViewModelBase
    {
        private readonly IForumService forumService;
        private readonly IDialogService dialogService;

        #region Properties

        private Topic selectedTopic;

        public Topic SelectedTopic
        {
            get
            {
                return selectedTopic;
            }
            set
            {
                if (selectedTopic == value)
                    return;

                selectedTopic = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Topic> topics = new ObservableCollection<Topic>();

        public ObservableCollection<Topic> Topics
        {
            get
            {
                return topics;
            }
            set
            {
                if (topics == value)
                    return;

                topics = value;
                RaisePropertyChanged();
            }
        }

        private string newTopicTitle;

        public string NewTopicTitle
        {
            get
            {
                return newTopicTitle;
            }
            set
            {
                if (newTopicTitle == value)
                    return;

                newTopicTitle = value;
                RaisePropertyChanged();
            }
        }

        private string newTopicMessage;

        public string NewTopicMessage
        {
            get
            {
                return newTopicMessage;
            }
            set
            {
                if (newTopicMessage == value)
                    return;

                newTopicMessage = value;
                RaisePropertyChanged();
            }
        }

        private RelayCommand loadTopicsCommand;

        public RelayCommand LoadTopicsCommand
        {
            get
            {
                return loadTopicsCommand ?? (loadTopicsCommand = new RelayCommand(async () => await ProcessLoadTopicsCommand()));
            }
        }

        private RelayCommand loadPostsCommand;

        public RelayCommand LoadPostsCommand
        {
            get
            {
                return loadPostsCommand ?? (loadPostsCommand = new RelayCommand(async () => await ProcessLoadPostsCommand(), () => SelectedTopic != null));
            }
        }

        private RelayCommand saveNewTopicCommand;

        public RelayCommand SaveNewTopicCommand
        {
            get
            {
                return saveNewTopicCommand ?? (saveNewTopicCommand = new RelayCommand(async () => await ProcessSaveNewTopicCommand(), () => !NewTopicTitle.IsEmpty() && !NewTopicMessage.IsEmpty()));
            }
        }

        private RelayCommand<Topic> deleteTopicCommand;

        public RelayCommand<Topic> DeleteTopicCommand
        {
            get
            {
                return deleteTopicCommand ?? (deleteTopicCommand = new RelayCommand<Topic>(async t => await ProcessDeleteTopicCommand(t)));
            }
        }

        #endregion

        public MainViewModel(IForumService forumService, IDialogService dialogService)
        {
            this.forumService = forumService;
            this.dialogService = dialogService;
        }

        private async Task ProcessLoadTopicsCommand()
        {
            try
            {
                var result = await forumService.GetAllTopicsAsync();
                Topics = new ObservableCollection<Topic>(result);
            }
            catch (Exception ex)
            {
                dialogService.ShowError(ex.Message, ex.ToString());
            }
        }

        private async Task ProcessLoadPostsCommand()
        {
            try
            {
                Topic editedTopic = await forumService.GetTopicDetailsAsync(SelectedTopic);

                // leegyszerûsített modal dialog kezelés
                dialogService.ShowDetails(editedTopic);

                await ProcessLoadTopicsCommand();
            }
            catch (Exception ex)
            {
                dialogService.ShowError(ex.Message, ex.ToString());
            }
        }

        private async Task ProcessSaveNewTopicCommand()
        {
            try
            {
                var topic = await forumService.CreateTopic(NewTopicTitle, NewTopicMessage);
                topics.Add(topic);

                NewTopicTitle = string.Empty;
                NewTopicMessage = string.Empty;
            }
            catch (Exception ex)
            {
                dialogService.ShowError(ex.Message, ex.ToString());
            }
        }

        private async Task ProcessDeleteTopicCommand(Topic topic)
        {
            try
            {
                if (dialogService.ShowConfirmation("Téma törlése", "Biztos vagy benne?"))
                {
                    await forumService.DeleteTopicAsync(topic);
                    Topics.Remove(topic);
                }
            }
            catch (Exception ex)
            {
                dialogService.ShowError(ex.Message, ex.ToString());
            }
        }

        protected override string Validate(string propertyName)
        {
            string validationMessage = string.Empty;
            switch (propertyName)
            {
                case "NewTopicTitle":
                    if (NewTopicTitle.IsEmpty())
                        validationMessage = "Kötelezõ";
                    break;
                case "NewTopicMessage":
                    if (NewTopicMessage.IsEmpty())
                        validationMessage = "Kötelezõ";
                    break;
            }

            return validationMessage;
        }
    }
}
