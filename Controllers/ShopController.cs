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

            var itemlist = new filterBLL().GetAll();
            ViewBag.TodaysSpecial = itemlist.Take(4).ToList();
            return View();
        }

        public JsonResult Filter(filterBLL data)
        {
            ViewBag.shopList = filterService.GetAll(data);
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

                    TempData["Searchtext"].ToString() != ""
                     )
                    {
                        filterBLL data = new filterBLL();
                        data.Category = TempData["CategoryIDs"].ToString();

                        data.Searchtxt = TempData["Searchtext"].ToString();


                        ViewBag.shopList = filterService.GetAll(data);
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