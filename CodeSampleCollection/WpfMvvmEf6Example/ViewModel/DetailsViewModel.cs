using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMvvmEf6Example.Interfaces;
using WpfMvvmEf6Example.Models;

namespace WpfMvvmEf6Example.ViewModel
{
    public class DetailsViewModel : ExtendedViewModelBase
    {
        private readonly ITopicEditService topicEditService;
        private readonly IDialogService dialogService;

        #region Properties

        private ObservableCollection<Post> posts = new ObservableCollection<Post>();

        public ObservableCollection<Post> Posts
        {
            get
            {
                return posts;
            }
            set
            {
                if (posts == value)
                    return;

                posts = value;
                RaisePropertyChanged();
            }
        }

        private Post selectedPost;

        public Post SelectedPost
        {
            get
            {
                return selectedPost;
            }
            set
            {
                if (selectedPost == value)
                    return;

                selectedPost = value;
                RaisePropertyChanged();
            }
        }

        private string topicTitle;

        public string TopicTitle
        {
            get
            {
                return topicTitle;
            }
            set
            {
                if (topicTitle == value)
                    return;

                topicTitle = value;
                RaisePropertyChanged();
            }
        }

        private string newPostMessage;

        public string NewPostMessage
        {
            get
            {
                return newPostMessage;
            }
            set
            {
                if (newPostMessage == value)
                    return;

                newPostMessage = value;
                RaisePropertyChanged();
            }
        }

        private RelayCommand createNewPostCommand;

        public RelayCommand CreateNewPostCommand
        {
            get
            {
                return createNewPostCommand ?? (createNewPostCommand = new RelayCommand(() => ProcessCreateNewPostCommand(), () => !NewPostMessage.IsEmpty()));
            }
        }

        private RelayCommand editPostCommand;

        public RelayCommand EditPostCommand
        {
            get
            {
                return editPostCommand ?? (editPostCommand = new RelayCommand(() => ProcessEditPostCommand()));
            }
        }

        private RelayCommand<Post> deletePostCommand;

        public RelayCommand<Post> DeletePostCommand
        {
            get
            {
                return deletePostCommand ?? (deletePostCommand = new RelayCommand<Post>(d => ProcessDeletePostCommand(d)));
            }
        }

        private RelayCommand saveCommand;

        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ?? (saveCommand = new RelayCommand(async () => await ProcessSaveCommand(), () => !TopicTitle.IsEmpty()));
            }
        }

        private RelayCommand cancelCommand;

        public RelayCommand CancelCommand
        {
            get
            {
                return cancelCommand ?? (cancelCommand = new RelayCommand(() => Close()));
            }
        }

        #endregion

        public DetailsViewModel(ITopicEditService topicEditService, IDialogService dialogService)
        {
            this.topicEditService = topicEditService;
            this.dialogService = dialogService;

            Messenger.Default.Register<Topic>(this, ViewModelLocator.SetEditTopicMessage, Initialize);
        }

        private void Initialize(Topic editedTopic)
        {
            topicEditService.InitializeEditedTopic(editedTopic);
            TopicTitle = editedTopic.Title;
            NewPostMessage = string.Empty;
            UpdatePosts();
        }

        private void UpdatePosts()
        {
            Posts = new ObservableCollection<Post>(topicEditService.EditedTopic.Posts.Where(p => p.State != ItemState.Deleted));
        }

        private void ProcessCreateNewPostCommand()
        {
            Post post = topicEditService.CreatePost(NewPostMessage);
            Posts.Add(post);

            NewPostMessage = string.Empty;
        }

        private void ProcessEditPostCommand()
        {
            if (SelectedPost != null)
                dialogService.ShowMessageEditor(SelectedPost);

            UpdatePosts();
            SelectedPost = null;
        }

        private void ProcessDeletePostCommand(Post post)
        {
            if (dialogService.ShowConfirmation("Hozzászólás törlése", "Biztos vagy benne?"))
            {
                post = topicEditService.DeletePost(post);
                Posts.Remove(post);
            }
        }

        private async Task ProcessSaveCommand()
        {
            try
            {
                topicEditService.UpdateTopicTitle(TopicTitle);
                await topicEditService.Save();

                Close();
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
                case "TopicTitle":
                    if (TopicTitle.IsEmpty())
                        validationMessage = "Kötelező";
                    break;
                case "NewPostMessage":
                    if (NewPostMessage.IsEmpty())
                        validationMessage = "Kötelező";
                    break;
            }

            return validationMessage;
        }
    }
}
