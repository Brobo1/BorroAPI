using BorroApp.Data;
using BorroApp.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BorroApp.Controller.Authorized
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BorroAuthorizedController : ControllerBase
    {
        private readonly BorroDbContext _context;

        public BorroAuthorizedController(BorroDbContext context)
        {
            _context = context;
        }

        /*
        [HttpPost]
        public async Task<IActionResult> CreatePost(Post createPost)
        {

            var newPost = new Post
            {
                Title = createPost.Title,
                Image = createPost.Image,
                Price = createPost.Price,
                DateFrom = createPost.DateFrom,
                DateTo = createPost.DateTo,
                Description = createPost.Description,
                Location = createPost.Location,
                CategoryId = createPost.CategoryId,
                UserId = createPost.UserId,
            };

            _context.Post.Add(newPost);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Post created", new { id = newPost.Id }, newPost);
        }*/
    }
}
