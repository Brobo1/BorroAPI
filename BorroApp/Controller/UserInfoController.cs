﻿using BorroApp.Data;
using BorroApp.Data.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BorroApp.Controller.Unauthorized;

[Route("api/[controller]")]
[ApiController]
public class UserInfoController : ControllerBase {
	private readonly BorroDbContext _context;

	public UserInfoController(BorroDbContext context) {
		_context = context;
	}
	
	[HttpGet("{id:int}")]
	public async Task<IActionResult> GetUserInfo(int id) {
		var userInfo = await _context.UserInfo.FindAsync(id);
		if (userInfo == null) {
			return NotFound();
		}

		return Ok(userInfo);
	}

	[HttpGet]
	public async Task<IActionResult> GetUserInfos() {
		return Ok(await _context.UserInfo.ToListAsync());
	}

	[HttpPost]
	public async Task<IActionResult> CreateUserInfo(UserInfoObject createUserInfo) {
		UserInfo newUserInfo = new() {
			FirstName    = createUserInfo.FirstName,
			LastName     = createUserInfo.LastName,
			ProfileImage = createUserInfo.ProfileImage,
			Address      = createUserInfo.Address,
			PostCode     = createUserInfo.PostCode,
			City         = createUserInfo.City,
			PhoneNumber  = createUserInfo.PhoneNumber,
			BirthDate    = createUserInfo.BirthDate,
			About        = createUserInfo.About,
			UserId       = createUserInfo.UserId
		};

		_context.UserInfo.Add(newUserInfo);
		await _context.SaveChangesAsync();

		return CreatedAtRoute(new { id = newUserInfo.Id }, newUserInfo);
	}

	[HttpPut("{id:int}")]
	public async Task<IActionResult> UpdateUserInfo(int id, UserInfoObject updateUserInfo) {
		var userInfo = await _context.UserInfo.FindAsync(id);
		if (userInfo == null) {
			return NotFound();
		}

		userInfo.FirstName    = updateUserInfo.FirstName;
		userInfo.LastName     = updateUserInfo.LastName;
		userInfo.ProfileImage = updateUserInfo.ProfileImage;
		userInfo.Address      = updateUserInfo.Address;
		userInfo.PostCode     = updateUserInfo.PostCode;
		userInfo.City         = updateUserInfo.City;
		userInfo.PhoneNumber  = updateUserInfo.PhoneNumber;
		userInfo.BirthDate    = updateUserInfo.BirthDate;
		userInfo.About        = updateUserInfo.About;
		userInfo.UserId       = updateUserInfo.UserId;
		await _context.SaveChangesAsync();

		return NoContent();
	}

	[HttpDelete("{id:int}")]
	public async Task<IActionResult> DeleteUserInfo(int id) {
		var userInfo = await _context.UserInfo.FindAsync(id);
		if (userInfo == null) {
			return NotFound();
		}

		_context.UserInfo.Remove(userInfo);
		await _context.SaveChangesAsync();

		return NoContent();
	}
	
}

public class UserInfoObject {
	public string?   FirstName    { get; set; }
	public string?   LastName     { get; set; }
	public string?   ProfileImage { get; set; }
	public string?   Address      { get; set; }
	public int?      PostCode     { get; set; }
	public string?   City         { get; set; }
	public string?   PhoneNumber  { get; set; }
	public DateTime? BirthDate    { get; set; }
	public string?   About        { get; set; }
	public int?      UserId       { get; set; }
}