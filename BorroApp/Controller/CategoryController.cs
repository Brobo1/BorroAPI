using BorroApp.Data;
using BorroApp.Data.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BorroApp.Controller.Unauthorized;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase {
	private readonly BorroDbContext _context;

	public CategoryController(BorroDbContext context) {
		_context = context;
	}

	[HttpGet]
	public async Task<IActionResult> GetCategories() {
		return Ok(await _context.Category.ToListAsync());
	}

	[HttpGet("{id:int}")]
	public async Task<IActionResult> GetCategory(int id) {
		var category = await _context.Category.FindAsync(id);
		if (category == null) {
			return NotFound();
		}
		return Ok(category);
	}

	[HttpPost]
	public async Task<IActionResult> CreateCategory(CategoryObject categoryObject) {
		Category newCategory = new() {
			Type = categoryObject.Type,
		};

		_context.Category.Add(newCategory);
		await _context.SaveChangesAsync();

		return CreatedAtRoute(new { id = newCategory.Id }, newCategory);
	}

	[HttpPut("{id:int}")]
	public async Task<IActionResult> UpdateCategory(int id, CategoryObject title) {
		var category = await _context.Category.FindAsync(id);
		if (category == null) {
			return NotFound();
		}

		category.Type = title.Type;
		await _context.SaveChangesAsync();

		return NoContent();
	}

	[HttpDelete("{id:int}")]
	public async Task<IActionResult> DeleteCategory(int id) {
		var category = await _context.Category.FindAsync(id);
		if (category == null) {
			return NotFound();
		}

		_context.Category.Remove(category);
		await _context.SaveChangesAsync();

		return NoContent();
	}
}

public class CategoryObject {
	public string Type { get; set; }
}

