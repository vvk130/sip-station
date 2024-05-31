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
    [ApiController]
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

        [HttpGet]
        public IActionResult Index(int? page)
        {
        const int pageSize = 8;
        int pageNumber = page ?? 0; 
        var drinks = _context.Drinks.OrderBy(b => b.Name)
                                    .Where(b => b.Id > pageNumber)
                                    .Take(pageSize)
                                    .ToList();

        return Ok(drinks);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

    //     [HttpGet]
    //     public async Task<IActionResult> GetAll()
    //     {
    //         var drinks = await _dbContext.Drinks.AsNoTracking()
    // .OrderBy(x => x.Id)
    // .Skip((pageNumber - 1) * pageSize)
    // .Take(pageSize)
    // .ToListAsync();
    //         var drinks = await _context.Drinks.Take(8).ToListAsync();
    //         return Ok(drinks);
    //     }

        [HttpGet("{drinkId}")]
        public IActionResult GetProductByIdUsingFromRoute([FromRoute(Name = "drinkId")] int id)
        {
            return Ok(_context.Drinks
                .Where(x => x.Id == id)
                .FirstOrDefault());
        }

        [HttpGet("averageprice")]
        public IActionResult CalculateAverageInDb()
        {
            return Ok(_context.Drinks.Average(b => b.Price));
        }

        [HttpGet("lowestprice")]
        public IActionResult CalculateMinInDb()
        {
            return Ok(_context.Drinks.Min(b => b.Price));
        }

        
        [HttpGet("highesttprice")]
        public IActionResult CalculateMaxInDb()
        {
            return Ok(_context.Drinks.Max(b => b.Price));
        }
    }
}