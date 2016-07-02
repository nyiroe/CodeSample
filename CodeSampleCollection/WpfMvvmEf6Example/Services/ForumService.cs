using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMvvmEf6Example.Interfaces;
using WpfMvvmEf6Example.Models;

namespace WpfMvvmEf6Example.Services
{
    public class ForumService : IForumService
    {
        private readonly IDataAccess dataAccess;

        public ForumService(IDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public async Task<IEnumerable<Topic>> GetAllTopicsAsync()
        {
            return await dataAccess.GetAllTopicsAsync();
        }

        public async Task<Topic> GetTopicDetailsAsync(Topic topic)
        {
            return await dataAccess.GetTopicDetailsAsync(topic);
        }

        public async Task<Topic> CreateTopic(string title, string message)
        {
            DateTime created = DateTime.Now;

            Topic topic = new Topic()
            {
                Title = title.Trim(),
                Created = created,
                State = ItemState.Added
            };

            Post post = new Post()
            {
                Message = message.Trim(),
                Created = created,
                State = ItemState.Added
            };

            topic.Posts.Add(post);

            return await dataAccess.CreateOrUpdateTopicAsync(topic);
        }

        public async Task<Topic> DeleteTopicAsync(Topic topic)
        {
            return await dataAccess.DeleteTopicAsync(topic);
        }
    }
}
