using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using src.DB;

namespace src.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var model = ListDbManager.RecentLists();

            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "A simple To Do list application, to play around with \"dotnet core\".";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
