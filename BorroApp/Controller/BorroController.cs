﻿using BorroApp.Data.Models;
using BorroApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BorroApp.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorroController : ControllerBase
    {
        private readonly BorroDbContext _context;
        public BorroController(BorroDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            return Ok(await _context.Post.ToListAsync());
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOnePost(int id)
        {
            var post = await _context.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

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

            return CreatedAtAction(nameof(GetOnePost), new { id = newPost.Id }, newPost);
        }

    }
}
