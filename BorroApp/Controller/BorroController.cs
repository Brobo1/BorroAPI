using BorroApp.Data.Models;
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

    }
}
