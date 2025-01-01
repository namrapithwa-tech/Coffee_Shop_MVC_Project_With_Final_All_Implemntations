using DataTables.Models.Coffee;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataTables.Controllers.Coffee
{
    [CheckAccess]
    public class CustomerController : Controller
    {
        #region Configuration
        private IConfiguration _configuration;
        public CustomerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region Customer List
        public IActionResult CustomerList()
        {
            string connectionString = _configuration.GetConnectionString("myConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Customer_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            return View(table);
        }
        #endregion

        #region Check Add Or Edit 
        public IActionResult AddEditCustomer(int? id)
        {
            string connectionString = this._configuration.GetConnectionString("myConnectionString");
            SqlConnection connection1 = new SqlConnection(connectionString);
            connection1.Open();
            SqlCommand command1 = connection1.CreateCommand();
            command1.CommandType = System.Data.CommandType.StoredProcedure;
            command1.CommandText = "PR_User_DropDown";
            SqlDataReader reader1 = command1.ExecuteReader();
            DataTable dataTable1 = new DataTable();
            dataTable1.Load(reader1);
            List<UserDropDownModel> usertList = new List<UserDropDownModel>();
            foreach (DataRow data in dataTable1.Rows)
            {
                UserDropDownModel userDropDownModel = new UserDropDownModel();
                userDropDownModel.UserID = Convert.ToInt32(data["UserID"]);
                userDropDownModel.UserName = data["UserName"].ToString();
                usertList.Add(userDropDownModel);
            }
            ViewBag.UserList = usertList;

            if (id != null)
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_Customer_SelectByPk";
                command.Parameters.Add("@CustomerID", SqlDbType.Int).Value = id;
                SqlDataReader reader = command.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
                CustomerModel data = new CustomerModel();
                foreach (DataRow dr in table.Rows)
                {
                    data.CustomerID = Convert.ToInt32(@dr["CustomerID"]);
                    data.CustomerName = @dr["CustomerName"].ToString();
                    data.HomeAddress = @dr["HomeAddress"].ToString();
                    data.Email = @dr["Email"].ToString();
                    data.MobileNo = @dr["MobileNo"].ToString();
                    data.GSTNO = @dr["GSTNO"].ToString();
                    data.CityName = @dr["CityName"].ToString();
                    data.PinCode = @dr["PinCode"].ToString();
                    data.NetAmount = Convert.ToDecimal(@dr["NetAmount"]);
                    data.UserID = Convert.ToInt32(@dr["UserID"]);
                }
                return View(data);
            }
            else
            {
                return View();
            }
        }
        #endregion

        #region Save Customer
        [HttpPost]
        public IActionResult SaveCustomer(CustomerModel cm)
        {
            ModelState.Remove("CustomerID");
            if (ModelState.IsValid)
            {
                string connectionString = this._configuration.GetConnectionString("myConnectionString");
                SqlConnection connection1 = new SqlConnection(connectionString);
                connection1.Open();
                SqlCommand command = connection1.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("CustomerName", cm.CustomerName);
                command.Parameters.AddWithValue("HomeAddress", cm.HomeAddress);
                command.Parameters.AddWithValue("Email", cm.Email);
                command.Parameters.AddWithValue("MobileNo", cm.MobileNo);
                command.Parameters.AddWithValue("GSTNO", cm.GSTNO);
                command.Parameters.AddWithValue("CityName", cm.CityName);
                command.Parameters.AddWithValue("PinCode", cm.PinCode);
                command.Parameters.AddWithValue("NetAmount", cm.NetAmount);
                command.Parameters.AddWithValue("UserID", cm.UserID);
                if (cm.CustomerID > 0)
                {
                    command.CommandText = "PR_Customer_UpdateByPk";
                    command.Parameters.Add("@CustomerID", SqlDbType.Int).Value = cm.CustomerID;
                    command.ExecuteNonQuery();
                    TempData["Message"] = "Record Updated Successfully";
                    return RedirectToAction("CustomerList");
                }
                else
                {
                    command.CommandText = "PR_Customer_Insert";
                    command.ExecuteNonQuery();
                    TempData["Message"] = "Record Inserted Successfully";
                    return RedirectToAction("AddEditCustomer");
                }
            }
            else
            {
                return RedirectToAction("AddEditCustomer");
            }
        }
        #endregion

        #region Delete Customer
        public IActionResult CustomerDelete(int CustomerID)
        {
            try
            {
                string connectionString = this._configuration.GetConnectionString("myConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_Customer_DeleteByPk";
                command.Parameters.Add("@CustomerID", SqlDbType.Int).Value = CustomerID;
                command.ExecuteNonQuery();
                TempData["Notification"] = "Record Deleted Successfully";

                return RedirectToAction("CustomerList");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                TempData["Notification"] = "You can not delete this record due to Foreign Key Constraints ";

                Console.WriteLine(ex.ToString());
                return RedirectToAction("CustomerList");
            }
        }
        #endregion
    }
}
