using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WpfMvvmEf6Example.Interfaces;
using WpfMvvmEf6Example.Models;

namespace WpfMvvmEf6Example.Services
{
    public class DataAccess : IDataAccess
    {
        private readonly IDbContextFactory dbContextFactory;

        public DataAccess(IDbContextFactory dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Topic>> GetAllTopicsAsync()
        {
            using (var context = dbContextFactory.GetDbContext())
            {
                return await context.Topics.ToListAsync();
            }
        }

        public async Task<Topic> GetTopicDetailsAsync(Topic topic)
        {
            using (var context = dbContextFactory.GetDbContext())
            {
                Topic fullTopic = await context.Topics.FindAsync(topic.TopicId);

                // kapcsolódó rekordok (Posts) betöltése és rendezés a Post dátuma szerint
                await context.Entry(fullTopic).Collection(p => p.Posts).Query().OrderBy(p => p.Created).LoadAsync();

                return fullTopic;
            }
        }

        public async Task<Topic> CreateOrUpdateTopicAsync(Topic topic)
        {
            using (var context = dbContextFactory.GetDbContext())
            {
                context.Topics.Add(topic);
                context.ApplyStateChanges();

                await context.SaveChangesAsync();

                return topic;
            }
        }

        public async Task<Topic> DeleteTopicAsync(Topic topic)
        {
            using (var context = dbContextFactory.GetDbContext())
            {
                Topic deletedTopic = await context.Topics.FindAsync(topic.TopicId);
                context.Topics.Remove(deletedTopic);

                await context.SaveChangesAsync();

                return deletedTopic;
            }
        }
    }
}
