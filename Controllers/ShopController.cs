using MadinaRestaurant.Models.BLL;
using MadinaRestaurant.Models.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MadinaRestaurant.Controllers
{
    public class ShopController : Controller
    {
        filterService filterService;
        public ShopController()
        {         
            filterService = new filterService();

        }
        // GET: Shop
        public ActionResult Shop()
        {
            var catlist = new categoryBLL().GetAll();
            ViewBag.Category = catlist.Take(6).ToList();

            ViewBag.BestProduct = new shopService().BestProducts();

            var itemlist = new itemBLL().GetAll();
            ViewBag.TodaysSpecial = itemlist.Take(4).ToList();
            return View();
        }
       


    }
}