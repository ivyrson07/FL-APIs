using FL.Common.Models;
using FL.Services.Comments.Managers;
using Microsoft.AspNetCore.Mvc;

namespace FL.Services.Comments.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentManager _commentManager;

        private readonly ILogger<CommentsController> _logger;

        public CommentsController(
            ICommentManager commentManager,
            
            ILogger<CommentsController> logger)
        {
            _commentManager = commentManager;

            _logger = logger;
        }

        [HttpGet]
        [Route("test")]
        public string Test()
        {
            return "TEST MESSAGE";
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<Comment> GetComment(Guid id)
        {
            Comment result = await _commentManager.GetAsync(id);
            
            return result;
        }

        [HttpPost]
        [Route("create")]
        public async Task<Comment> CreateComment(Comment comment)
        {
            Comment result = await _commentManager.CreateAsync(comment);

            return result;
        }
    }
}