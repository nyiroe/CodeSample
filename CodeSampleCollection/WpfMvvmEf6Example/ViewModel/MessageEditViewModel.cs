using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMvvmEf6Example.Interfaces;
using WpfMvvmEf6Example.Models;

namespace WpfMvvmEf6Example.ViewModel
{
    public class MessageEditViewModel : ExtendedViewModelBase
    {
        private readonly ITopicEditService topicEditService;

        private string message;
        private Post editedPost;

        #region Properties

        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                if (message == value)
                    return;

                message = value;
                RaisePropertyChanged();
            }
        }

        private DateTime created;

        public DateTime Created
        {
            get
            {
                return created;
            }
            set
            {
                if (created == value)
                    return;

                created = value;
                RaisePropertyChanged();
            }
        }

        private RelayCommand saveCommand;

        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ?? (saveCommand = new RelayCommand(() => ProcessSaveCommand(), () => !Message.IsEmpty()));
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

        public MessageEditViewModel(ITopicEditService topicEditService)
        {
            this.topicEditService = topicEditService;
            Messenger.Default.Register<Post>(this, ViewModelLocator.SetEditPostMessage, Initialize);
        }

        private void Initialize(Post editedPost)
        {
            this.editedPost = editedPost;
            Message = editedPost.Message;
            Created = editedPost.Created;
        }

        private void ProcessSaveCommand()
        {
            topicEditService.UpdatePost(editedPost, Message);
            Close();
        }

        protected override string Validate(string propertyName)
        {
            string validationMessage = string.Empty;
            switch (propertyName)
            {
                case "TopicTitle":
                    if (Message.IsEmpty())
                        validationMessage = "Kötelező";
                    break;
            }

            return validationMessage;
        }
    }
}
