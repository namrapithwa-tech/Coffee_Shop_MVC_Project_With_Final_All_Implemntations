using DataTables.Models.Coffee;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace DataTables.Controllers.Coffee
{
    [CheckAccess]
    public class OrderController : Controller
    {
        #region Configuration
        private IConfiguration _configuration;
        public OrderController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region Order List
        public IActionResult OrderList()
        {
            string connectionString = _configuration.GetConnectionString("myConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Order_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            return View(table);
        }
        #endregion

        #region Check Add Edit
        public IActionResult AddEditOrder(int? id)
        {
            string connectionString = this._configuration.GetConnectionString("myConnectionString");
            SqlConnection connection1 = new SqlConnection(connectionString);
            connection1.Open();
            SqlCommand command1 = connection1.CreateCommand();
            command1.CommandType = System.Data.CommandType.StoredProcedure;
            command1.CommandText = "PR_Customer_DropDown";
            SqlDataReader reader1 = command1.ExecuteReader();
            DataTable dataTable1 = new DataTable();
            dataTable1.Load(reader1);
            List<CustomerDropDownModel> customerList = new List<CustomerDropDownModel>();
            foreach (DataRow data in dataTable1.Rows)
            {
                CustomerDropDownModel customerDropDownModel = new CustomerDropDownModel();
                customerDropDownModel.CustomerID = Convert.ToInt32(data["CustomerID"]);
                customerDropDownModel.CustomerName = data["CustomerName"].ToString();
                customerList.Add(customerDropDownModel);
            }
            ViewBag.CustomerList = customerList;

            command1.CommandText = "PR_User_DropDown";
            reader1 = command1.ExecuteReader();
            DataTable dataTable2 = new DataTable();
            dataTable2.Load(reader1);
            List<UserDropDownModel> usertList = new List<UserDropDownModel>();
            foreach (DataRow data in dataTable2.Rows)
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
                command.CommandText = "PR_Order_SelectByPk";
                command.Parameters.Add("@OrderID", SqlDbType.Int).Value = id;
                SqlDataReader reader = command.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
                OrderModel data = new OrderModel();
                foreach (DataRow dr in table.Rows)
                {
                    data.OrderID = Convert.ToInt32(@dr["OrderID"]);
                    data.CustomerID = Convert.ToInt32(@dr["CustomerID"]);
                    data.PaymentMode = @dr["PaymentMode"].ToString();
                    data.OrderNumber = @dr["OrderNumber"].ToString();
                    data.OrderDate = Convert.ToDateTime(@dr["OrderDate"]);
                    data.TotalAmount = Convert.ToDecimal(@dr["TotalAmount"]);
                    data.ShippingAddress = @dr["ShippingAddress"].ToString();
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

        #region Save Order
        [HttpPost]
        public IActionResult SaveOrder(OrderModel om)
        {
            ModelState.Remove("OrderID");
            if (ModelState.IsValid)
            {
                string connectionString = this._configuration.GetConnectionString("myConnectionString");
                SqlConnection connection1 = new SqlConnection(connectionString);
                connection1.Open();
                SqlCommand command = connection1.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("OrderDate", om.OrderDate);
                command.Parameters.AddWithValue("CustomerID", om.CustomerID);
                command.Parameters.AddWithValue("OrderNumber", om.OrderNumber);
                command.Parameters.AddWithValue("PaymentMode", om.PaymentMode);
                command.Parameters.AddWithValue("TotalAmount", om.TotalAmount);
                command.Parameters.AddWithValue("ShippingAddress", om.ShippingAddress);
                command.Parameters.AddWithValue("UserID", om.UserID);
                if (om.OrderID > 0)
                {
                    command.CommandText = "PR_Order_UpdateByPk";
                    command.Parameters.Add("@OrderID", SqlDbType.Int).Value = om.OrderID;
                    command.ExecuteNonQuery();
                    TempData["Message"] = "Record Updated Successfully";
                    return RedirectToAction("OrderList");
                }
                else
                {
                    command.CommandText = "PR_Order_Insert";
                    command.ExecuteNonQuery();
                    TempData["Message"] = "Record Inserted Successfully";
                    return RedirectToAction("AddEditOrder");
                }
                
            }
            else
            {
                return RedirectToAction("AddEditOrder");
            }
        }
        #endregion

        #region Delete Order
        public IActionResult OrderDelete(int OrderID)
        {
            try
            {
                string connectionString = this._configuration.GetConnectionString("myConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_Order_DeleteByPk";
                command.Parameters.Add("@OrderID", SqlDbType.Int).Value = OrderID;
                command.ExecuteNonQuery();
                TempData["Notification"] = "Record Deleted Successfully";

                return RedirectToAction("OrderList");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                TempData["Notification"] = "You can not delete this record due to Foreign Key Constraints ";

                Console.WriteLine(ex.Message);
                return RedirectToAction("OrderList");
            }
        }
        #endregion
    }
}
