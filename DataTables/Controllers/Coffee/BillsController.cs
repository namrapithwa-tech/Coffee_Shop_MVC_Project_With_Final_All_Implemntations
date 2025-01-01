using DataTables.Models.Coffee;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace DataTables.Controllers.Coffee
{
    [CheckAccess]
    public class BillsController : Controller
    {
        #region Configurtions
        private IConfiguration _configuration;
        public BillsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region Bills List
        public IActionResult BillsList()
        {
            string connectionString = _configuration.GetConnectionString("myConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Bills_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            return View(table);
        }
        #endregion

        #region Check Add Or Edit 
        public IActionResult AddEditBill(int? id)
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
                command.CommandText = "PR_Bills_SelectByPk";
                command.Parameters.Add("@BillID", SqlDbType.Int).Value = id;
                SqlDataReader reader = command.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
                BillsModel data = new BillsModel();
                foreach (DataRow dr in table.Rows)
                {
                    data.BillID = Convert.ToInt32(@dr["BillID"]);
                    data.BillNumber = @dr["BillNumber"].ToString();
                    data.BillDate = Convert.ToDateTime(@dr["BillDate"]);
                    data.OrderID = Convert.ToInt32(@dr["OrderID"]);
                    data.TotalAmount = Convert.ToInt32(@dr["TotalAmount"]);
                    data.Discount = Convert.ToInt32(@dr["Discount"]);
                    data.NetAmount = Convert.ToInt32(@dr["NetAmount"]);
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

        #region Save Bill
        [HttpPost]
        public IActionResult SaveBill(BillsModel bm)
        {
            ModelState.Remove("BillID");
            if (ModelState.IsValid)
            {
                string connectionString = this._configuration.GetConnectionString("myConnectionString");
                SqlConnection connection1 = new SqlConnection(connectionString);
                connection1.Open();
                SqlCommand command = connection1.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("BillNumber", bm.BillNumber);
                command.Parameters.AddWithValue("BillDate", bm.BillDate);
                command.Parameters.AddWithValue("OrderID", bm.OrderID);
                command.Parameters.AddWithValue("TotalAmount", bm.TotalAmount);
                command.Parameters.AddWithValue("Discount", bm.Discount);
                command.Parameters.AddWithValue("NetAmount", bm.NetAmount);
                command.Parameters.AddWithValue("UserID", bm.UserID);
                if (bm.BillID > 0)
                {
                    command.CommandText = "PR_Bills_UpdateByPk";
                    command.Parameters.Add("@BillID", SqlDbType.Int).Value = bm.BillID;
                    command.ExecuteNonQuery();
                    TempData["Message"] = "Record Updated Successfully";
                    return RedirectToAction("BillsList");
                }
                else
                {
                    command.CommandText = "PR_Bills_Insert";
                    command.ExecuteNonQuery();
                    TempData["Message"] = "Record Inserted Successfully";
                    return RedirectToAction("AddEditBill");
                }
                
            }
            else
            {
                return RedirectToAction("AddEditBill");
            }
        }
        #endregion

        #region Delete Bill
        [HttpPost]
        public IActionResult BillDelete(int BillID)
        {
            try
            {
                string connectionString = this._configuration.GetConnectionString("myConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_Bills_DeleteByPk";
                command.Parameters.Add("@BillID", SqlDbType.Int).Value = BillID;
                command.ExecuteNonQuery();
                TempData["Notification"] = "Record Deleted Successfully";

                return RedirectToAction("BillsList");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                TempData["Notification"] = "You can not delete this record due to Foreign Key Constraints ";

                Console.WriteLine(ex.ToString());
                return RedirectToAction("BillsList");
            }
        }
        #endregion

    }
}
