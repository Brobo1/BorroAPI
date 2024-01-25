﻿using BorroApp.Data;
using BorroApp.Data.Models;
using Microsoft.AspNetCore.Authorization;
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

   // [Authorize]
    [HttpGet("{id:int}")]
	public async Task<IActionResult> GetUser(int id) {
		var user = await _context.User.FindAsync(id);
		if (user == null) {
			return NotFound();
		}

		return Ok(user);
	}



	[HttpPost]
	public async Task<IActionResult> CreateUser(UserObject createUser) {
		User newUser = new() {
			Email    = createUser.Email,
			Password = createUser.Password
		};

		_context.User.Add(newUser);
		await _context.SaveChangesAsync();

		return CreatedAtRoute(new { id = newUser.Id }, new { userId = newUser.Id });
	}
    [Authorize]
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
    [Authorize]
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

    [Authorize]
    [HttpPut("changePassword/{id:int}")]
    public async Task<IActionResult> ChangePassword(int id, ChangePasswordObject changePassword)
    {
        var user = await _context.User.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        // Validate the old password before changing
        if (!VerifyPassword(changePassword.OldPassword, user.Password))
        {
            return BadRequest("Invalid old password");
		}
		else
		{
			user.Password=changePassword.NewPassword;
            await _context.SaveChangesAsync();
			return Ok(user);

        }

       

       
    }

    private bool VerifyPassword(string inputPassword, string storedPassword)
    {
        return inputPassword == storedPassword;
    }
}

public class UserObject {
	public string? Email    { get; set; }
	public string? Password { get; set; }

}

public class ChangePasswordObject
{
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}