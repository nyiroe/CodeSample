using System.Threading.Tasks;
using WpfMvvmEf6Example.Models;

namespace WpfMvvmEf6Example.Interfaces
{
    public interface ITopicEditService
    {
        Topic EditedTopic { get; }
        void InitializeEditedTopic(Topic editedTopic);
        void UpdateTopicTitle(string title);
        Post CreatePost(string message);
        Post UpdatePost(Post post, string message);
        Post DeletePost(Post post);
        Task<Topic> Save();
    }
}
