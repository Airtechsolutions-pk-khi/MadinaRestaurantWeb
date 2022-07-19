using MadinaRestaurant.Models.BLL;
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
        filterService filterService;
        productService _service;

        public HomeController()
        {
            _service = new productService();
            filterService = new filterService();
        }

        // GET: Home
        public ActionResult Index()
        {
            var _items = new itemService().GetSelecteditems();
            ViewBag.TenItems = _items;
            ViewBag.itemList = _items.Take(3).Where(x => x.StatusID == 1 ).ToList();

            var catlist = new categoryBLL().GetAll();
            ViewBag.Category = catlist.Take(6).ToList();

            var AllItems = new shopService().BestProducts();
            ViewBag.BestProduct = AllItems.Take(20).ToList();
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
        public ActionResult MenuList()
        {
            return View();
        }
        public ActionResult Menu()
        {
            return View();
        }
        public JsonResult Filter(filterBLL data)
        {
            ViewBag.shopList = filterService.GetAllCategory(data);
            return Json(new { data = ViewBag.shopList }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Products(List<filterBLL> Products)
        {
            ViewBag.Message = "";
            if (Products != null)
            {
                ViewBag.shopList = Products;
                if (ViewBag.shopList.Count < 1)
                {
                    ViewBag.Message = "No Product Found";
                }
                return PartialView("_Products");
            }
            else
            {
                if (TempData.Count > 1)
                {
                    if (TempData["CategoryIDs"].ToString() != "" ||
                    
                    TempData["Searchtext"].ToString() != "" )
                    
                    {
                        filterBLL data = new filterBLL();
                        data.Category = TempData["CategoryIDs"].ToString();                       
                        data.Searchtxt = TempData["Searchtext"].ToString();                                              
                        ViewBag.shopList = filterService.GetAllCategory(data);
                        if (ViewBag.shopList.Count < 1)
                        {
                            ViewBag.Message = "No Product Found";
                        }
                    }
                }
                else
                {                    
                    ViewBag.shopList = "";
                    ViewBag.Message = "No Product Found";

                }

                return PartialView("_Products");
            }
        }
    }
}