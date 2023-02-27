using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookingSystem.Models;
using SimpleBookingSystem.Models;

namespace SimpleBookingSystem.Controllers
{
    public class ResourcesController : Controller
    {
        private readonly LocalDatabaseContext _context;

        public ResourcesController(LocalDatabaseContext context)
        {
            _context = context;
        }

        // GET: Resources
     
        public async Task<IActionResult> Index()
        {
              return View(await _context.Resources.ToListAsync());
        }
    }
}
