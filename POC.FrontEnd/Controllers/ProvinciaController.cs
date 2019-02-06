using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace POC.FrontEnd.Controllers
{
    public class ProvinciaController : Controller
    {
        public IActionResult List()
        {
            return View("listProvincia");
        }

        public IActionResult Add()
        {
            ViewBag.Id = -1;
            ViewBag.IsViewing = "false";
            ViewBag.IsEditing = "false";
            ViewBag.IsDeleting = "false";
            return View("Form");
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Id = id;
            ViewBag.IsViewing = "false";
            ViewBag.IsEditing = "true";
            ViewBag.IsDeleting = "false";
            return View("Form");
        }

        public IActionResult Delete(int id)
        {
            ViewBag.Id = id;
            ViewBag.IsViewing = "false";
            ViewBag.IsEditing = "false";
            ViewBag.IsDeleting = "true";
            return View("Form");
        }

        public IActionResult Viewing(int id)
        {
            ViewBag.Id = id;
            ViewBag.IsViewing = "true";
            ViewBag.IsEditing = "false";
            ViewBag.IsDeleting = "false";
            return View("Form");
        }
    }
}