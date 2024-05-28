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
    public class DrinkController : Controller
    {
        private readonly ILogger<DrinkController> _logger;
        private readonly KmzgwuhhContext _context;

        public DrinkController(ILogger<DrinkController> logger, KmzgwuhhContext context)
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
        public ActionResult<IEnumerable<Drink>> GetAll()
        {
            var drinks = _context.Drinks.ToListAsync();
            return Ok(drinks);
        }
    }
}