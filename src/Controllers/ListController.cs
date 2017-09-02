using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace src.Controllers
{
    public class ListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}