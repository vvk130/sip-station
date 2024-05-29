using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SipStation.Controllers
{
    [Route("[controller]")]
    public class DrinksController : Controller
    {
        private readonly ILogger<DrinksController> _logger;
        private readonly postgresContext _context;

        public DrinksController(ILogger<DrinksController> logger, postgresContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var drinks = await _context.Drinks.ToListAsync();
            return Ok(drinks);
        }

        [HttpGet("id/{drinkId}")]
        public IActionResult GetProductByIdUsingFromRoute([FromRoute(Name = "drinkId")] int id)
        {
            return Ok(_context.Drinks
                .Where(x => x.Id == id)
                .FirstOrDefault());
        }
    }
}