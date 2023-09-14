using EasyNetQ;
using FL.Common.Models;
using FL.Services.Comments.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FL.Services.Comments.Managers
{
    public interface ICommentManager
    {
        Task<Comment> GetAsync(Guid id);

        Task<Comment> CreateAsync(Comment comment);
    }

    public class CommentManager : ICommentManager
    {
        public Task<Comment> GetAsync(Guid id)
        {
            return Task.FromResult(new Comment
            {
                Id = id,
                Description = "",
                PostId = id,
                Reactions = new List<Reaction>()
            });
        }

        public Task<Comment> CreateAsync(Comment comment)
        {
            // save to database here, beep, beep, beep, ...

            // testing pubsub
            try
            {
                using (var bus = RabbitHutch.CreateBus("host=localhost"))
                {
                    bus.PubSub.PublishAsync(comment);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"EasyNetQ error : {ex.Message}");
                Console.WriteLine($"EasyNetQ error : {ex.StackTrace}");
            }

            return Task.FromResult(new Comment
            {
                Id = Guid.NewGuid(),
                Description = comment.Description,
                PostId = comment.PostId,
                Reactions = new List<Reaction>()
            });
        }
    }
}
