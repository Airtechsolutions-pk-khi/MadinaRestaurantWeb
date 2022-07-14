using MadinaRestaurant.Models.BLL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace MadinaRestaurant.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Cart()
        {
            return View();
        }
        public ActionResult Checkout(int id = -1)
        {
            int CustomerID = id;
            if (CustomerID == 0)
            {
                Session["CustomerID"] = 0;
                return View();
            }
            else
            {
                if (Session["CustomerID"] != null && Convert.ToInt32(Session["CustomerID"]) != 0)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login_Register", "Account");
                }
            }
        }

        public JsonResult PunchOrder(checkoutBLL data)
        {
            try
            {

                checkoutBLL _service = new checkoutBLL();
                //orderdetails
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(JArray.Parse(data.OrderDetailString));
                JArray jsonResponse = JArray.Parse(json);
                data.OrderDetail = jsonResponse.ToObject<List<checkoutBLL.OrderDetails>>();
                //gifts
                try
                {
                    if (data.OrderGiftsString != "")
                    {
                        //string jsonGift = Newtonsoft.Json.JsonConvert.SerializeObject(JArray.Parse(data.OrderGiftsString));
                        //JArray jsonResponseGift = JArray.Parse(jsonGift);
                        //data.OrderGifts = jsonResponseGift.ToObject<List<OrderGiftDetails>>();
                    }
                }
                catch (Exception ex)
                { }
                int rtn = _service.InsertOrder(data);

                if (data.PaymentMethodID == 7)
                {
                    return Json(new { data = "DebitCreditCard", OrderID = rtn }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { data = rtn }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { data = 0 }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult OrderComplete(int OrderID = 0, string OrderNo = "")
        {
            try
            {
                checkoutBLL check = new checkoutBLL();
                if (OrderNo == "Reject")
                {
                    ViewBag.OrderNo = "Reject";
                    //check.OrderUpdate(OrderID, 103);//Rejected 
                }
                else
                {
                    var data = new myorderBLL().GetDetails(OrderID);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
           
               
            return View();
        }
    }
}