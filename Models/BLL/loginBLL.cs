using MadinaRestaurant.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WebAPICode.Helpers;


namespace MadinaRestaurant.Models.BLL
{
    public class loginBLL
    {
        public int CustomerID { get; set; }
        public string FullName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public int StatusID { get; set; }
        public int IsVerified { get; set; }
        public int LastUpdatedBy { get; set; }

        public string Mobile { get; set; }

        public static DataTable _dt;
        public static DataSet _ds;
        public loginBLL login()
        {
            try
            {
                var obj = new loginBLL();
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@email", Email);
                p[1] = new SqlParameter("@password", Password);
                _ds = (new DBHelper().GetDatasetFromSP)("sp_login_Web",p);
                if (_ds != null)
                {
                    if (_ds.Tables[0].Rows.Count > 0)
                    {
                        obj = _ds.Tables[0].DataTableToList<loginBLL>().FirstOrDefault();
                    }
                }
                return obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int Register()
        {
            try
            {
                int rtn = 0;
                SqlParameter[] p = new SqlParameter[4];
                p[0] = new SqlParameter("@FullName", FullName);               
                p[1] = new SqlParameter("@Email", Email);
                p[2] = new SqlParameter("@Password", Password);
                p[3] = new SqlParameter("@Mobile", Mobile);
                rtn = (new DBHelper().ExecuteNonQueryReturn)("dbo.sp_register_Web", p);

                return rtn;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}