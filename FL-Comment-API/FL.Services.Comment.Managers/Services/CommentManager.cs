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
        private readonly IBus _bus;

        public CommentManager(IBus bus)
        {
            _bus = bus;
        }

        public async Task<Comment> GetAsync(Guid id)
        {
            return await Task.FromResult(new Comment
            {
                Id = id,
                Description = "",
                PostId = id,
                Reactions = new List<Reaction>()
            });
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            // save to database here, beep, beep, beep, ...

            // testing pubsub
            try
            {
                if (comment.PostId == Guid.Empty)
                {
                    await _bus.PubSub.PublishAsync(comment, "test.privatecomment");
                }
                else
                {
                    await _bus.PubSub.PublishAsync(comment, "test.publiccomment");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"EasyNetQ error : {ex.Message}");
                Console.WriteLine($"EasyNetQ error : {ex.StackTrace}");
            }

            return await Task.FromResult(new Comment
            {
                Id = Guid.NewGuid(),
                Description = comment.Description,
                PostId = comment.PostId,
                Reactions = new List<Reaction>()
            });
        }
    }
}
