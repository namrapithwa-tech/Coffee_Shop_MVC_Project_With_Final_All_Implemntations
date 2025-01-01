using DataTables.Models.Coffee;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace DataTables.Controllers.Coffee
{
    public class UserController : Controller
    {

        #region Configuration
        private IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region User List
        public IActionResult UserList()
        {
            string connectionString = _configuration.GetConnectionString("myConnectionString");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PR_User_SelectAll";
            SqlDataReader reader = command.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            return View(table);
        }
        #endregion

        #region Check Add Or Edit 
        public IActionResult AddEditUser(int? id)
        {
            if (id != null)
            {
                string connectionString = this._configuration.GetConnectionString("myConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_User_SelectByPk";
                command.Parameters.Add("@UserID", SqlDbType.Int).Value = id;
                SqlDataReader reader = command.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
                UserModel data = new UserModel();
                foreach (DataRow dr in table.Rows)
                {
                    data.UserID = Convert.ToInt32(@dr["UserID"]);
                    data.UserName = @dr["UserName"].ToString();
                    data.Email = @dr["Email"].ToString();
                    data.Password = @dr["Password"].ToString();
                    data.MobileNo = @dr["MobileNo"].ToString();
                    data.Address = @dr["Address"].ToString();
                    data.IsActive = Convert.ToBoolean(@dr["IsActive"]);
                }
                return View(data);
            }
            else
            {
                return View();
            }
        }
        #endregion

        #region Save User
        [HttpPost]
        public IActionResult SaveUser(UserModel um)
        {
            ModelState.Remove("UserID");
            if (ModelState.IsValid)
            {
                string connectionString = this._configuration.GetConnectionString("myConnectionString");
                SqlConnection connection1 = new SqlConnection(connectionString);
                connection1.Open();
                SqlCommand command = connection1.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                
                command.Parameters.AddWithValue("UserName", um.UserName);
                command.Parameters.AddWithValue("Email", um.Email);
                command.Parameters.AddWithValue("Password", um.Password);
                command.Parameters.AddWithValue("MobileNo", um.MobileNo);
                command.Parameters.AddWithValue("Address", um.Address);
                command.Parameters.AddWithValue("IsActive", um.IsActive);
                if(um.UserID > 0)
                {
                    command.CommandText = "PR_User_UpdateByPk";
                    command.Parameters.Add("@UserID", SqlDbType.Int).Value = um.UserID;
                    command.ExecuteNonQuery();
                    TempData["Message"] = "Record Updated Successfully";
                    return RedirectToAction("UserList");
                }
                else
                {
                    command.CommandText = "PR_User_Insert";
                    command.ExecuteNonQuery();
                    TempData["Message"] = "Record Inserted Successfully";
                    return RedirectToAction("AddEditUser");
                }

            }
            else
            {
                return RedirectToAction("AddEditUser");
            }
        }
        #endregion

        #region Delete User
        public IActionResult UserDelete(int UserID)
        {
            try
            {
                string connectionString = this._configuration.GetConnectionString("myConnectionString");
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "PR_User_DeleteByPk";
                command.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
                command.ExecuteNonQuery();
                TempData["Notification"] = "Record Deleted Successfully";

                return RedirectToAction("UserList");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                TempData["Notification"] = "You can not delete this record due to Foreign Key Constraints ";
                Console.WriteLine(ex.ToString());
                return RedirectToAction("UserList");
            }
        }
        #endregion

        #region User Register
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UserRegister(UserModel userRegisterModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string connectionString = this._configuration.GetConnectionString("myConnectionString");
                    SqlConnection sqlConnection = new SqlConnection(connectionString);
                    sqlConnection.Open();
                    SqlCommand sqlCommand = sqlConnection.CreateCommand();
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandText = "PR_User_Insert";
                    sqlCommand.Parameters.Add("@UserName", SqlDbType.VarChar).Value = userRegisterModel.UserName;
                    sqlCommand.Parameters.Add("@Password", SqlDbType.VarChar).Value = userRegisterModel.Password;
                    sqlCommand.Parameters.Add("@Email", SqlDbType.VarChar).Value = userRegisterModel.Email;
                    sqlCommand.Parameters.Add("@MobileNo", SqlDbType.VarChar).Value = userRegisterModel.MobileNo;
                    sqlCommand.Parameters.Add("@Address", SqlDbType.VarChar).Value = userRegisterModel.Address;
                    sqlCommand.Parameters.Add("@IsActive", SqlDbType.VarChar).Value = 1;
                    sqlCommand.ExecuteNonQuery();
                    return RedirectToAction("Login");
                }
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Register");
            }
            return RedirectToAction("Register");
        }
        #endregion

        #region User Login and Logout

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult UserLogin(UserLoginModel userLoginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string connectionString = this._configuration.GetConnectionString("myConnectionString");
                    SqlConnection sqlConnection = new SqlConnection(connectionString);
                    sqlConnection.Open();
                    SqlCommand sqlCommand = sqlConnection.CreateCommand();
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandText = "PR_User_Login";
                    sqlCommand.Parameters.Add("@UserName", SqlDbType.VarChar).Value = userLoginModel.UserName;
                    sqlCommand.Parameters.Add("@Password", SqlDbType.VarChar).Value = userLoginModel.Password;
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    DataTable dataTable = new DataTable();
                    dataTable.Load(sqlDataReader);
                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            HttpContext.Session.SetString("UserID", dr["UserID"].ToString());
                            HttpContext.Session.SetString("UserName", dr["UserName"].ToString());
                        }

                        return RedirectToAction("ProductList", "Product");
                    }
                    else
                    {
                        return RedirectToAction("Login");
                    }

                }
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
            }

            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        #endregion
    }
}
