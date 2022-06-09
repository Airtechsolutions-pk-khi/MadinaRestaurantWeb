using MadinaRestaurant.Models.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MadinaRestaurant.Controllers
{
    public class HomeController : Controller
    {
        productService _service;

        public HomeController()
        {
            _service = new productService();
        }

        // GET: Home
        public ActionResult Index()
        {
            var _items = new itemService().GetSelecteditems();
            ViewBag.itemList = _items.Take(3).ToList();
            return View();
        }
        public ActionResult Aboutus()
        {
            return View();
        }
        public ActionResult Contactus()
        {
            return View();
        }
    }
}