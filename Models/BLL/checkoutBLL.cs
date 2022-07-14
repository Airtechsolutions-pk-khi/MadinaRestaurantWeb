using MadinaRestaurant.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using WebAPICode.Helpers;

namespace MadinaRestaurant.Models.BLL
{
    public class checkoutBLL
    {
        public int? PaymentMethodID { get; set; }


        public string OrderType { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public string SessionID { get; set; }
        public int? OrderID { get; set; }
        public string OrderNo { get; set; }
        public int? CustomerID { get; set; }

        public int? StatusID { get; set; }
        public int? OrderTakerID { get; set; }
        public int? DeliverUserID { get; set; }
        
        public Nullable<System.DateTime> OrderPreparedDate { get; set; }
        public Nullable<System.DateTime> OrderOFDDate { get; set; }
        public Nullable<System.DateTime> OrderDoneDate { get; set; }
        public string Remarks { get; set; }
        public int? LocationID { get; set; }
        public double? AmountTotal { get; set; }
        public double? GrandTotal { get; set; }
        public double? Tax { get; set; }
        public double? DeliveryAmount { get; set; }
        public double? DiscountAmount { get; set; }
        public int? TotalItems { get; set; }
       
       
        public Nullable<System.DateTime> LastUpdateDT { get; set; }
        public int? LastUpdateBy { get; set; }
        public int? CouponID { get; set; }

        /*Cust Order Info*/
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Description { get; set; }
        public string LocationURL { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public string CouponCode { get; set; }
        public string SenderName { get; set; }
        public string SenderAddress { get; set; }
     
        public string SenderContact { get; set; }
        public int? CustOrderInfoID { get; set; }
        public string AddressType { get; set; }

        public string AddressNickName { get; set; }
        public string DeliveryArea { get; set; }
        public string City { get; set; }
        public string ContactNo { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public Nullable<System.DateTime> DeliveryTime { get; set; }
        public string CustomerName { get; set; }
        /*public int CustomerID { get; set; }*/
      
        public string PlaceType { get; set; }
        
        public string CardNotes { get; set; }
        public string SelectedTime { get; set; }
        public List<OrderDetails> OrderDetail = new List<OrderDetails>();
        public string OrderDetailString { get; set; }

        
        public string OrderGiftsString { get; set; }

        /*Order Details*/
        public class OrderDetails
        {
            public int OrderDetailID { get; set; }
            public int? OrderID { get; set; }
            public int ItemID { get; set; }
            public string OrderMode { get; set; }
            public string ProNote { get; set; }
            public string Image { get; set; }
            public int GiftID { get; set; }
            public int Quantity { get; set; }
            public int Qty { get; set; }
            public double Price { get; set; }
            public double Cost { get; set; }
            public double DiscountAmount { get; set; }
            public Nullable<System.DateTime> LastUpdateDT { get; set; }
            public string LastUpdateBy { get; set; }
            public int? StatusID { get; set; }
            public int? TotalItems { get; set; }
            public int Key { get; set; }
        }
        /*Order Checkout*/
        public class OrderCheckout
        {
            public int OrderCheckoutID { get; set; }
            public int OrderID { get; set; }
            public int? PaymentMode { get; set; }
            public float? AmountPaid { get; set; }
            public float? AmountTotal { get; set; }
            public float? GrandTotal { get; set; }
            public float? ServiceCharges { get; set; }
            public float? Tax { get; set; }
            public Nullable<System.DateTime> CheckoutDate { get; set; }
            public string SessionID { get; set; }
            public int? StatusID { get; set; }
            public Nullable<System.DateTime> LastUpdatedDate { get; set; }
            public int? LastUpdateBy { get; set; }
            public int ItemKey { get; set; }

        }
        public int InsertOrder(checkoutBLL data)
        {

            try
            {
                int rtn = 0;

                SqlParameter[] p = new SqlParameter[25];
                //ORDER MASTER
          
                p[0] = new SqlParameter("@OrderNo", data.OrderNo);
                if (data.CustomerID == 0)
                {
                    p[1] = new SqlParameter("@CustomerID", DBNull.Value);
                }
                else
                {
                    p[1] = new SqlParameter("@CustomerID", data.CustomerID);
                }
                p[2] = new SqlParameter("@OrderType", data.OrderType);
                p[3] = new SqlParameter("@OrderDate", data.OrderDate);                
                p[4] = new SqlParameter("@SessionID", data.SessionID);
                p[5] = new SqlParameter("@OrderTakerID", data.OrderTakerID);
                p[6] = new SqlParameter("@DeliverUserID", data.DeliverUserID);
                p[7] = new SqlParameter("@LastUpdatedBy", data.LastUpdateBy);
                p[8] = new SqlParameter("@LastUpdatedDT", data.LastUpdateDT);
                p[9] = new SqlParameter("@LocationID", data.LocationID);
                p[10] = new SqlParameter("@OrderPreparedDate", data.OrderPreparedDate);
                p[11] = new SqlParameter("@OrderOFDDate", data.OrderOFDDate);
                p[12] = new SqlParameter("@OrderDoneDate", data.OrderDoneDate);
                p[13] = new SqlParameter("@Remarks", data.Remarks);

                if (data.PaymentMethodID == 1)
                {
                    p[14] = new SqlParameter("@StatusID", 101);
                }
                else
                {
                    p[14] = new SqlParameter("@StatusID", 103);
                }
               

                //CUSTOMER ORDER INFO
                

                p[15] = new SqlParameter("@Name", data.Name);
                p[16] = new SqlParameter("@Email", data.Email);
                p[17] = new SqlParameter("@Mobile", data.Mobile);
                p[18] = new SqlParameter("@Description", data.Description);
                p[19] = new SqlParameter("@Address", data.Address);
                p[20] = new SqlParameter("@LocationURL", data.LocationURL);
                p[21] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);
                p[22] = new SqlParameter("@AddressType", data.AddressType);
                p[23] = new SqlParameter("@AddressNickName", data.AddressNickName);
                p[24] = new SqlParameter("@DeliveryArea", data.DeliveryArea);
                

                int OrderID = int.Parse(new DBHelper().GetTableFromSP("sp_InsertOrder_Web", p).Rows[0]["ID"].ToString());
                rtn = OrderID;
                //Payment 
                try
                {

                    SqlParameter[] pay = new SqlParameter[12];
                    pay[0] = new SqlParameter("@OrderID", OrderID);
                    pay[1] = new SqlParameter("@PaymentMode", data.PaymentMethodID);
                    pay[2] = new SqlParameter("@AmountPaid", data.AmountTotal);
                    pay[3] = new SqlParameter("@AmountTotal", data.AmountTotal);
                    pay[4] = new SqlParameter("@GrandTotal", data.GrandTotal);
                    pay[5] = new SqlParameter("@ServiceCharges", DBNull.Value);
                    pay[6] = new SqlParameter("@Tax", DBNull.Value);
                    pay[7] = new SqlParameter("@CheckoutDate", data.OrderDate);
                    pay[8] = new SqlParameter("@SessionID", DBNull.Value);
                    pay[9] = new SqlParameter("@StatusID", data.StatusID);
                    pay[10] = new SqlParameter("@LastUpdateBy", data.LastUpdateBy);
                    pay[11] = new SqlParameter("@LastUpdatedDate", data.LastUpdatedDate);
                    
                    
                    (new DBHelper().ExecuteNonQueryReturn)("sp_InsertOrderCheckout_Web", pay);

                }
                catch(Exception ex) { }
                try
                {
                    int OrderDetailID = 0;
                    foreach (var item in data.OrderDetail)
                    {
                        SqlParameter[] para = new SqlParameter[9];
                        para[0] = new SqlParameter("@OrderID", OrderID);//Hard Coded Value Pass
                        para[1] = new SqlParameter("@ItemID", item.ItemID);
                        
                        para[2] = new SqlParameter("@Quantity", item.Qty);
                        para[3] = new SqlParameter("@Price", item.Price);
                        para[4] = new SqlParameter("@Cost", item.Cost);
                        para[5] = new SqlParameter("@OrderMode", item.OrderMode);
                        para[6] = new SqlParameter("@StatusID", item.StatusID);
                        para[7] = new SqlParameter("@LastUpdateDT", item.LastUpdateDT);
                        para[8] = new SqlParameter("@LastUpdateBy", item.LastUpdateBy);
                        
                        OrderDetailID = int.Parse(new DBHelper().GetTableFromSP("sp_InsertOrderDetail_Web", para).Rows[0]["ID"].ToString());
                        
                    }

                }
                catch (Exception ex)
                {
                    using (StreamWriter writer = new StreamWriter(System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/Template") + "\\" + "error.txt"), true))
                    {
                        writer.WriteLine("-----------------------------------------------------------------------------");
                        writer.WriteLine("Date : " + DateTime.Now.ToString());
                        writer.WriteLine();

                        while (ex != null)
                        {
                            writer.WriteLine(ex.GetType().FullName);
                            writer.WriteLine("Message : " + ex.Message + "##" + data);
                            writer.WriteLine("Object : " + data);
                            writer.WriteLine("StackTrace : " + ex.StackTrace);

                            ex = ex.InnerException;
                        }
                    }
                }
                return rtn;
            }
            catch (Exception ex)
            {
                using (StreamWriter writer = new StreamWriter(System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/Template") + "\\" + "error.txt"), true))
                {
                    writer.WriteLine("-----------------------------------------------------------------------------");
                    writer.WriteLine("Date : " + DateTime.Now.ToString());
                    writer.WriteLine();

                    while (ex != null)
                    {
                        writer.WriteLine(ex.GetType().FullName);
                        writer.WriteLine("Message : " + ex.Message + "##" + data);
                        writer.WriteLine("Object : " + data);
                        writer.WriteLine("StackTrace : " + ex.StackTrace);

                        ex = ex.InnerException;
                    }
                }
                return 0;
            }
        }
        public int OrderUpdate(int OrderID, int StatusID)
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@OrderID", OrderID);
                p[1] = new SqlParameter("@StatusID", StatusID);
                rtn = (new DBHelper().ExecuteNonQueryReturn)("sp_OrderReject", p);

                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}