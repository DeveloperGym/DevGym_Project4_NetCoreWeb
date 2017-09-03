using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using src.DB;
using src.Models;

namespace src.Controllers
{
    public class ListController : Controller
    {
        public IActionResult Index()
        {
            var model = ListDbManager.AllLists();

            return View(model);
        }

        public IActionResult Show(int id)
        {
            var model = ListDbManager.OneList(id);

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = new AListViewModel();
            if (id > 0)
            {
                model = ListDbManager.OneList(id);
                model.Message = "Item Count: "+model.Items.Count;
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(int id, AListViewModel model)
        {
            if (ModelState.IsValid)
            {
                AList toSave = model;
                toSave.ID = id;
                ListDbManager.SaveList(toSave);
                return RedirectToAction("Show", new {id=id});
            }

            model.Message = "Item Count: " + model.Items.Count;
            return View(model);
        }
    }
}