using EtheralWeb.Data;
using EtheralWeb.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EtheralWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<IdentityUser> _userManger;


        public HomeController(AppDbContext context, IWebHostEnvironment webHostEnvironment, UserManager<IdentityUser> userManger)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _userManger = userManger;
        }

        public IActionResult Index()
        {
            return View(_context.Testimonials.ToList());
        }

        
    }
}
