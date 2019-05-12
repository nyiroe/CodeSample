using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMvvmEf6Example.Models;

namespace WpfMvvmEf6Example.Interfaces
{
    public interface IForumService
    {
        Task<IEnumerable<Topic>> GetAllTopicsAsync();
        Task<Topic> GetTopicDetailsAsync(Topic topic);
        Task<Topic> CreateTopic(string title, string message);
        Task<Topic> DeleteTopicAsync(Topic topic);
    }
}
