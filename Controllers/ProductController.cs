using MadinaRestaurant.Models.BLL;
using MadinaRestaurant.Models.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MadinaRestaurant.Controllers
{
    public class ProductController : Controller
    {
        productService _service;
        public ProductController()
        {
            _service = new productService();

        }
        // GET: Product
        public ActionResult ProductDetails(int ItemID)
        {
            ViewBag.ProductDetails = _service.GetAll(ItemID);

            var _items = new itemService().GetSelecteditems();
            ViewBag.TenItems = _items;
            ViewBag.itemList = _items.Take(3).Where(x => x.StatusID == 1).ToList();

            return View(_service.GetAll(ItemID));
        }

        public JsonResult PostProductReview(productBLL.ReviewsBLL data)
        {
            var revData = new productBLL().InsertProductReview(data);
            if (revData != 0)
            {
                return Json(new { Success = true });
            }
            else
            {
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
            }
            
        }

        public ActionResult Wishlist()
        {           
            return View();
        }
    }
}