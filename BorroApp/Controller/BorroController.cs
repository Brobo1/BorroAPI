using BorroApp.Data.Models;
using BorroApp.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

namespace BorroApp.Controller;

[Route("api/[controller]")]
[ApiController]
public class BorroController : ControllerBase {
	private readonly BorroDbContext _context;

	public BorroController(BorroDbContext context) {
		_context = context;
	}

}
