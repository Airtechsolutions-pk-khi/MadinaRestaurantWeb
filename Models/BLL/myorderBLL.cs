using MadinaRestaurant.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WebAPICode.Helpers;

namespace MadinaRestaurant.Models.BLL
{
    public class myorderBLL
    {
        public int? CustoerOrderDetail { get; set; }
        public int OrderID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string LocationURL { get; set; }
        public int? StatusID { get; set; }
        [DisplayFormat(DataFormatString = "{0: dd, MMM, yyyy}")]
        public DateTime OrderDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public int? LastUpdatedBy { get; set; }
        public string AddressType { get; set; }
        public string AddressNickName { get; set; }
        public string DeliveryArea { get; set; }
        
        /*Cust Order Info*/
        public string CouponCode { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string SenderContact { get; set; }
        public int CustOrderInfoID { get; set; }
       
        public string NearestPlace { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ContactNo { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime? DeliveryTime { get; set; }
        public string CustomerName { get; set; }
       
       
        public string PlaceType { get; set; }
        
        public string CardNotes { get; set; }
        public string SelectedTime { get; set; }
        public int Row_Counter { get; set; }
        public List<OrderDetails> OrderDetail = new List<OrderDetails>();
        
        /*Order Details*/
        public class OrderDetails
        {
            public int OrderDetailID { get; set; }
            
            public int? Quantity { get; set; }
            public double Price { get; set; }
        }
        public class GiftDetails
        {
            public int OrderDetailID { get; set; }
            public string GiftTitle { get; set; }
            public string GiftImage { get; set; }
            public int Quantity { get; set; }
            public double Price { get; set; }
        }

        public static DataTable _dt;
        public static DataSet _ds;
        public List<myorderBLL> GetAll(int CustomerID)
        {
            try
            {
                var lst = new List<myorderBLL>();
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("@CustomerID", CustomerID);
                _dt = (new DBHelper().GetTableFromSP)("sp_GetMyOrders",p);
                if (_dt != null)
                {
                    if (_dt.Rows.Count > 0)
                    {
                        lst = _dt.DataTableToList<myorderBLL>();
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public myorderBLL GetDetails(int OrderID)
        {
            try
            {
                var obj = new myorderBLL();
                List<OrderDetails> lstIM = new List<OrderDetails>();
                 
                SqlParameter[] p = new SqlParameter[1];

                p[0] = new SqlParameter("@OrderID", OrderID);
                _ds = (new DBHelper().GetDatasetFromSP)("sp_GetMyOrderDetails_Web", p);
                if (_ds != null)
                {
                    if (_ds.Tables.Count > 0)
                    {
                        if (_ds.Tables[0] != null)
                        {
                            obj = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_ds.Tables[0])).ToObject<List<myorderBLL>>().FirstOrDefault();
                        }
                        if (_ds.Tables[1] != null)
                        {
                            lstIM = JArray.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(_ds.Tables[1])).ToObject<List<OrderDetails>>();
                        }
                         
                        obj.OrderDetail = lstIM;
                         
                    }
                }
                return obj;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}