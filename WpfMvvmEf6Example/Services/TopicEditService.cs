using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMvvmEf6Example.Interfaces;
using WpfMvvmEf6Example.Models;

namespace WpfMvvmEf6Example.Services
{
    public class TopicEditService : ITopicEditService
    {
        private readonly IDataAccess dataAccess;

        public Topic EditedTopic { get; private set; }

        public TopicEditService(IDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public void InitializeEditedTopic(Topic editedTopic)
        {
            EditedTopic = editedTopic;
        }

        public void UpdateTopicTitle(string title)
        {
            EditedTopic.Title = title.Trim();
            EditedTopic.State = ItemState.Modified;
        }

        public Post CreatePost(string message)
        {
            Post post = new Post()
            {
                Message = message.Trim(),
                Created = DateTime.Now,
                State = ItemState.Added
            };

            EditedTopic.Posts.Add(post);
            return post;
        }

        public Post UpdatePost(Post post, string message)
        {
            post.Message = message.Trim();

            // ha új record lett módosítva, ne változzon a State
            if (post.PostId != default(int))
                post.State = ItemState.Modified;

            return post;
        }

        public Post DeletePost(Post post)
        {
            post.State = ItemState.Deleted;

            // ha új record lett törölve, bele se kerüljön a gráfba
            // mivel az EF megpróbálja törölni a 0-s ID-jű rekordot is
            if (post.PostId == default(int))
                EditedTopic.Posts.Remove(post);

            return post;
        }

        public async Task<Topic> Save()
        {
            return await dataAccess.CreateOrUpdateTopicAsync(EditedTopic);
        }
    }
}
