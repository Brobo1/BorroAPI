using BorroApp.Data.Models;
using BorroApp.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

namespace BorroApp.Controller {
	[Route("api/[controller]")]
	[ApiController]
	public class BorroController : ControllerBase {
		private readonly BorroDbContext _context;

		public BorroController(BorroDbContext context) {
			_context = context;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllPosts() {
			return Ok(await _context.Post.ToListAsync());
		}

		
		//LIGGER HER FOR TESTING
		[HttpPost]
		public async Task<IActionResult> CreatePost(CreatePostObject createPost) {
			var newPost = new Post {
				Title       = createPost.Title,
				Image       = createPost.Image,
				Price       = createPost.Price,
				DateFrom    = createPost.DateFrom,
				DateTo      = createPost.DateTo,
				Description = createPost.Description,
				Location    = createPost.Location,
				CategoryId  = createPost.CategoryId,
				UserId      = createPost.UserId,
			};

			_context.Post.Add(newPost);
			await _context.SaveChangesAsync();

			return CreatedAtRoute(new { id = newPost.Id }, newPost);
		}
		
		[HttpPost("category")]
		public async Task<IActionResult> CreateCategory(CreateCategoryObject createCategory) {
			Category newPost = new () {
				Type = createCategory.Type,
			};

			_context.Category.Add(newPost);
			await _context.SaveChangesAsync();

			return CreatedAtRoute(new { id = newPost.Id }, newPost);
		}

		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetOnePost(int id) {
			var post = await _context.Post.FindAsync(id);
			if (post == null) {
				return NotFound();
			}

			return Ok(post);
		}

		[HttpPost("user")]
		public async Task<IActionResult> CreateUser(CreateUserObject userInfo) {
			if (userInfo.Email == null || userInfo.Password == null) {
				return BadRequest("please write fill inn the email and/or password");
			}
			User newUser = new() {
				Email    = userInfo.Email,
				Password = userInfo.Password
			};
			await _context.User.AddAsync(newUser);
			int rowsEffected = await _context.SaveChangesAsync();
			if (rowsEffected != 1) {
				return BadRequest("user not created");
			}
			return Ok();
		}

		[HttpGet("user/{id}")]
		public async Task<IActionResult> GetOneUser(int id) {
			User? user = await _context.User.FirstOrDefaultAsync(x => x.Id == id);
			if (user == null) {
				return NotFound();
			}
			return Ok(user);
		}

		[HttpGet("/users")]
		public async Task<IActionResult> GetAllUsers() {
			List<User> users = await _context.User.ToListAsync();
			if (users.Count == 0) {
				return NotFound();
			}
			return Ok(users);
		}
	}

	public class CreateCategoryObject {
		public string? Type { get; set; }
	}

	public class CreatePostObject {
		public string?                   Title        { get; set; }
		public string?                   Image        { get; set; }
		public double?                   Price        { get; set; }
		public DateTime?                 DateFrom     { get; set; }
		public DateTime?                 DateTo       { get; set; }
		public string?                   Description  { get; set; }
		public string?                   Location     { get; set; }
		public int?                      CategoryId   { get; set; }
		public int?                      UserId       { get; set; }
	}

	public class CreateUserObject {
		public string Email    { get; set; }
		public string Password { get; set; }
	}
}