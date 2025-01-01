using DataTables.Models.Coffee;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace DataTables.Controllers.Coffee
{
    [CheckAccess]
    public class ProductController : Controller
    {
        #region Configurtions
        private IConfiguration _configuration;
        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region Product List
        public IActionResult ProductList()
        {
            string connectionString = _configuration.GetConnectionString("myConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_Product_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            return View(table);
        }
        #endregion

        #region Check Add Or Edit 
        public IActionResult AddEditProduct(int? id)
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
            List<UserDropDownModel> userList = new List<UserDropDownModel>();
            foreach (DataRow data in dataTable1.Rows)
            {
                UserDropDownModel userDropDownModel = new UserDropDownModel();
                userDropDownModel.UserID = Convert.ToInt32(data["UserID"]);
                userDropDownModel.UserName = data["UserName"].ToString();
                userList.Add(userDropDownModel);
            }
            ViewBag.UserList = userList;
            if (id != null)
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_Product_SelectByPk";
                command.Parameters.Add("@ProductID", SqlDbType.Int).Value = id;
                SqlDataReader reader = command.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
                ProductModel data = new ProductModel();
                foreach (DataRow dr in table.Rows)
                {
                    data.ProductID = Convert.ToInt32(@dr["ProductID"]);
                    data.ProductName = @dr["ProductName"].ToString();
                    data.ProductCode = @dr["ProductCode"].ToString();
                    data.ProductPrice = Convert.ToDecimal(@dr["ProductPrice"]);
                    data.Description = @dr["Description"].ToString();
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

        #region Save Product
        [HttpPost]
        public IActionResult SaveProduct(ProductModel pm)
        {
            ModelState.Remove("ProductID");
            if (ModelState.IsValid)
            {
                string connectionString = this._configuration.GetConnectionString("myConnectionString");
                SqlConnection connection1 = new SqlConnection(connectionString);
                connection1.Open();
                SqlCommand command = connection1.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("ProductName", pm.ProductName);
                command.Parameters.AddWithValue("ProductPrice", pm.ProductPrice);
                command.Parameters.AddWithValue("ProductCode", pm.ProductCode);
                command.Parameters.AddWithValue("Description", pm.Description);
                command.Parameters.AddWithValue("UserID", pm.UserID);
                if (pm.ProductID > 0)
                {
                    command.CommandText = "PR_Product_UpdateByPk";
                    command.Parameters.Add("@ProductID", SqlDbType.Int).Value = pm.ProductID;
                    TempData["Message"] = "Record Updated Successfully";
                    command.ExecuteNonQuery();
                    return RedirectToAction("ProductList");

                }
                else
                {
                    command.CommandText = "PR_Product_Insert";
                    TempData["Message"] = "Record Inserted Successfully";
                    command.ExecuteNonQuery();
                    return RedirectToAction("AddEditProduct");
                }
                
            }
            else
            {
                return RedirectToAction("AddEditProduct");
            }
        }
        #endregion

        #region Delete Product
        public IActionResult ProductDelete(int ProductID)
        {
            try
            {
                string connectionString = this._configuration.GetConnectionString("myConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_Product_DeleteByPk";
                command.Parameters.Add("@ProductID", SqlDbType.Int).Value = ProductID;
                command.ExecuteNonQuery();
                TempData["Notification"] = "Record Deleted Successfully";

                return RedirectToAction("ProductList");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                TempData["Notification"] = "You can not delete this record due to Foreign Key Constraints ";

                Console.WriteLine(ex.ToString());
                return RedirectToAction("ProductList");
            }
        }
        #endregion
    }
}
