using BorroApp.Data.Models;
using BorroApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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
        [HttpPost("user")]
        public async Task <IActionResult> CreateUser(CreateUserObject userInfo)
        {
            if (userInfo.Email==null||userInfo.Password==null)
            {
                return BadRequest("please write fill inn the email and/or password");
            }
            User newUser = new()
            {
                Email = userInfo.Email,
                Password = userInfo.Password
            };
          await _context.User.AddAsync(newUser);
          int rowsEffected= await _context.SaveChangesAsync();
            if (rowsEffected!=1)
            {
                return BadRequest("user not created");
            }
            return Ok();


        }
        [HttpGet("user/{id}")]
       public async Task <IActionResult> GetOneUser(int id)
        {
          User? user=  await _context.User.FirstOrDefaultAsync(x=>x.Id==id);
            if (user==null)
            {
                return NotFound(); 
            }
            return Ok(user);
        }

        [HttpGet("/users")]
        public async Task<IActionResult> GetAllUsers()
        {
         List<User> users = await _context.User.ToListAsync();
            if (users.Count== 0)
            {
                return NotFound();
            }
            return Ok(users);
        }



    }
}
public class CreateUserObject
{
    public string Email {  get; set; }
    public string Password { get; set; }

}
