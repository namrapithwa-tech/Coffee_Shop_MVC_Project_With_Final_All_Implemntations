using DataTables.Models.Coffee;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace DataTables.Controllers.Coffee
{
    [CheckAccess]
    public class OrderDetailsController : Controller
    {
        #region Configurations
        private IConfiguration _configuration;
        public OrderDetailsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region Order Details List
        public IActionResult OrderDetailsList()
        {
            string connectionString = _configuration.GetConnectionString("myConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_OrderDetail_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            return View(table);
        }
        #endregion

        #region Check Edit or Add
        public IActionResult AddEditOrderDetail(int? id)
        {
            string connectionString = this._configuration.GetConnectionString("myConnectionString");
            SqlConnection connection1 = new SqlConnection(connectionString);
            connection1.Open();
            SqlCommand command1 = connection1.CreateCommand();
            command1.CommandType = System.Data.CommandType.StoredProcedure;
            command1.CommandText = "PR_Order_DropDown";
            SqlDataReader reader1 = command1.ExecuteReader();
            DataTable dataTable1 = new DataTable();
            dataTable1.Load(reader1);
            List<OrderDropDownModel> orderList = new List<OrderDropDownModel>();
            foreach (DataRow data in dataTable1.Rows)
            {
                OrderDropDownModel orderDropDownModel = new OrderDropDownModel();
                orderDropDownModel.OrderID = Convert.ToInt32(data["OrderID"]);
                orderDropDownModel.OrderNumber = data["OrderNumber"].ToString();
                orderList.Add(orderDropDownModel);
            }
            ViewBag.OrderList = orderList;
            command1.CommandText = "PR_Product_DropDown";
            reader1 = command1.ExecuteReader();
            DataTable dataTable2 = new DataTable();
            dataTable2.Load(reader1);
            List<ProductDropDownModel> productList = new List<ProductDropDownModel>();
            foreach (DataRow data in dataTable2.Rows)
            {
                ProductDropDownModel productDropDownModel = new ProductDropDownModel();
                productDropDownModel.ProductID = Convert.ToInt32(data["ProductID"]);
                productDropDownModel.ProductName = data["ProductName"].ToString();
                productList.Add(productDropDownModel);
            }
            ViewBag.ProductList = productList;

            command1.CommandText = "PR_User_DropDown";
            reader1 = command1.ExecuteReader();
            DataTable dataTable3 = new DataTable();
            dataTable3.Load(reader1);
            List<UserDropDownModel> usertList = new List<UserDropDownModel>();
            foreach (DataRow data in dataTable3.Rows)
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
                command.CommandText = "PR_OrderDetail_SelectByPk";
                command.Parameters.Add("@OrderDetailID", SqlDbType.Int).Value = id;
                SqlDataReader reader = command.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
                OrderDetailsModel data = new OrderDetailsModel();
                foreach (DataRow dr in table.Rows)
                {
                    data.OrderDetailID = Convert.ToInt32(@dr["OrderDetailID"]);
                    data.Amount = Convert.ToDecimal(@dr["Amount"]);
                    data.TotalAmount = Convert.ToDecimal(@dr["TotalAmount"]);
                    data.OrderID = Convert.ToInt32(@dr["OrderID"]);
                    data.ProductID = Convert.ToInt32(@dr["ProductID"]);
                    data.Quantity = Convert.ToInt32(@dr["Quantity"]);
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

        #region Save Order Detail
        [HttpPost]
        public IActionResult SaveOrderDetail(OrderDetailsModel odm)
        {
            ModelState.Remove("OrderDetailID");
            if (ModelState.IsValid)
            {
                string connectionString = this._configuration.GetConnectionString("myConnectionString");
                SqlConnection connection1 = new SqlConnection(connectionString);
                connection1.Open();
                SqlCommand command = connection1.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("OrderID", odm.OrderID);
                command.Parameters.AddWithValue("ProductID", odm.ProductID);
                command.Parameters.AddWithValue("Quantity", odm.Quantity);
                command.Parameters.AddWithValue("Amount", odm.Amount);
                command.Parameters.AddWithValue("TotalAmount", odm.TotalAmount);
                command.Parameters.AddWithValue("UserID", odm.UserID);
                if (odm.OrderDetailID > 0)
                {
                    command.CommandText = "PR_OrderDetail_UpdateByPk";
                    command.Parameters.Add("@OrderDetailID", SqlDbType.Int).Value = odm.OrderDetailID;
                    TempData["Message"] = "Record Updated Successfully";
                    command.ExecuteNonQuery();
                    return RedirectToAction("OrderDetailsList");
                }
                else
                {
                    command.CommandText = "PR_OrderDetail_Insert";
                    command.ExecuteNonQuery();
                    TempData["Message"] = "Record Inserted Successfully";
                    return RedirectToAction("AddEditOrderDetail");

                }
            }
            else
            {
                return RedirectToAction("AddEditOrderDetail");
            }
        }
        #endregion

        #region Delete Order Detail
        public IActionResult OrderDetailDelete(int OrderDetailID)
        {
            try
            {
                string connectionString = this._configuration.GetConnectionString("myConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_OrderDetail_DeleteByPk";
                command.Parameters.Add("@OrderDetailID", SqlDbType.Int).Value = OrderDetailID;
                command.ExecuteNonQuery();
                TempData["Notification"] = "Record Deleted Successfully";

                return RedirectToAction("OrderDetailsList");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                TempData["Notification"] = "You can not delete this record due to Foreign Key Constraints ";

                Console.WriteLine(ex.ToString());
                return RedirectToAction("OrderDetailsList");
            }
        }
        #endregion 
    }
}
