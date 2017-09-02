using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using src.Models;

namespace src.Controllers
{
    public class ListController : Controller
    {
        public IActionResult Index()
        {
            var model = new ListsViewModel();

            return View(model);
        }

        public IActionResult Show(int id)
        {
            var model = new AList();

            model.Title = "test";

            return View(model);
        }
    }
}