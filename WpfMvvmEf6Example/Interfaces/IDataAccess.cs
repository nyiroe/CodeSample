using System.Collections.Generic;
using System.Threading.Tasks;
using WpfMvvmEf6Example.Models;

namespace WpfMvvmEf6Example.Interfaces
{
    public interface IDataAccess
    {
        Task<IEnumerable<Topic>> GetAllTopicsAsync();
        Task<Topic> GetTopicDetailsAsync(Topic topic);
        Task<Topic> CreateOrUpdateTopicAsync(Topic topic);
        Task<Topic> DeleteTopicAsync(Topic topic);
    }
}
