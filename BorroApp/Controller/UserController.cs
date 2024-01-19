using BorroApp.Data;
using BorroApp.Data.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BorroApp.Controller.Unauthorized;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase {
	private readonly BorroDbContext _context;
	public UserController(BorroDbContext context) {
		_context = context;
	}
	
	[HttpGet("{id:int}")]
	public async Task<IActionResult> GetUser(int id) {
		var user = await _context.User.FindAsync(id);
		if (user == null) {
			return NotFound();
		}

		return Ok(user);
	}

	[HttpGet]
	public async Task<IActionResult> GetUsers() {
		return Ok(await _context.User.ToListAsync());
	}

	[HttpPost]
	public async Task<IActionResult> CreateUser(UserObject createUser) {
		User newUser = new() {
			Email    = createUser.Email,
			Password = createUser.Password
		};

		_context.User.Add(newUser);
		await _context.SaveChangesAsync();

		return CreatedAtRoute(new { id = newUser.Id }, newUser);
	}

	[HttpPut("{id:int}")]
	public async Task<IActionResult> UpdateUser(int id, UserObject updateUser) {
		var user = await _context.User.FindAsync(id);
		if (user == null) {
			return NotFound();
		}

		user.Email    = updateUser.Email;
		user.Password = updateUser.Password;
		await _context.SaveChangesAsync();

		return NoContent();
	}

	[HttpDelete("{id:int}")]
	public async Task<IActionResult> DeleteUser(int id) {
		var user = await _context.User.FindAsync(id);
		if (user == null) {
			return NotFound();
		}

		_context.User.Remove(user);
		await _context.SaveChangesAsync();

		return NoContent();
	}
}

public class UserObject {
	public string? Email    { get; set; }
	public string? Password { get; set; }

}