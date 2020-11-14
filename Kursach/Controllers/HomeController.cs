using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Kursach.Models;
using Kursach.Services;
using CoursesMain;

namespace Kursach.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EmailService _mailer;
        ApplicationContext db = new ApplicationContext();

    public HomeController(ILogger<HomeController> logger, EmailService mailer)
        {
            _logger = logger;
            _mailer = mailer;
        }
        public IActionResult FanficMenu()
        {
            return View();
        }
        public IActionResult Index()
        {
           return View(db.Fanfics);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
