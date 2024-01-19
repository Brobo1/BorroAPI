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
	public async Task<IActionResult> GetAllCategories() {
		return Ok(await _context.Category.ToListAsync());
	}
	
	[HttpPost]
	public async Task<IActionResult> CreateCategory(CreateCategory createCategory) {
		Category newCategory = new() {
			Type = createCategory.Type,
		};

		_context.Category.Add(newCategory);
		await _context.SaveChangesAsync();

		return CreatedAtRoute(new { id = newCategory.Id }, newCategory);
	}
	
	
	
}

public class CreateCategory {
	public string Type { get; set; }
}