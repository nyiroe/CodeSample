using CommonServiceLocator;
using System.Linq;
using System.Windows;
using Unity;
using Unity.Lifetime;
using Unity.ServiceLocation;
using WpfMvvmEf6Example.Interfaces;
using WpfMvvmEf6Example.Services;

namespace WpfMvvmEf6Example.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        public const string SetEditTopicMessage = "SetEditTopic";
        public const string SetEditPostMessage = "SetEditPost";

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            var unityContainer = new UnityContainer()
                .RegisterType<MainViewModel>(new ContainerControlledLifetimeManager())
                .RegisterType<DetailsViewModel>(new ContainerControlledLifetimeManager())
                .RegisterType<MessageEditViewModel>(new ContainerControlledLifetimeManager())
                .RegisterType<IDbContextFactory, DbContextFactory>(new ContainerControlledLifetimeManager())
                .RegisterType<IDataAccess, DataAccess>(new ContainerControlledLifetimeManager())
                .RegisterType<IForumService, ForumService>(new ContainerControlledLifetimeManager())
                .RegisterType<ITopicEditService, TopicEditService>(new ContainerControlledLifetimeManager())
                .RegisterType<IDialogService, DialogService>(new ContainerControlledLifetimeManager());

            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(unityContainer));
        }

        public static Window ActiveWindow
        {
            get
            {
                return Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            }
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public DetailsViewModel Details
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DetailsViewModel>();
            }
        }

        public MessageEditViewModel MessageEdit
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MessageEditViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
