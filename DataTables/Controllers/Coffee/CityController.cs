using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using DataTables.Models.Coffee;
using DataTables.Models;

namespace DataTables.Controllers.Coffee
{
    public class CityController : Controller
    {
        private readonly IConfiguration _configuration;

        #region configuration
        public CityController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region City List
        public IActionResult CityList()
        {
            string connectionstr = this._configuration.GetConnectionString("myConnectionString");
            //PrePare a connection
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();

            //Prepare a Command
            SqlCommand objCmd = conn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_LOC_City_SelectAll";

            SqlDataReader objSDR = objCmd.ExecuteReader();
            dt.Load(objSDR);
            conn.Close();
            return View("CityList", dt);
        }
        #endregion

        #region Add/Edit City
        public IActionResult AddEditCity(int? CityID)
        {
            CityModel model = new CityModel();
            if (CityID != null && CityID > 0) // Edit mode
            {
                string connectionstr = this._configuration.GetConnectionString("myConnectionString");
                using (SqlConnection conn = new SqlConnection(connectionstr))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "PR_LOC_City_SelectByPK";
                        cmd.Parameters.AddWithValue("@CityID", CityID);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                model.CityID = Convert.ToInt32(dr["CityID"]);
                                model.CityName = dr["CityName"].ToString();
                                model.CityCode = dr["CityCode"].ToString();
                                model.StateID = Convert.ToInt32(dr["StateID"]);
                                model.CountryID = Convert.ToInt32(dr["CountryID"]);
                            }
                        }
                    }
                }
            }
            return View("AddEditCity", model);
        }
        #endregion

        #region Save City
        [HttpPost]
        [HttpPost]
        public IActionResult SaveCity(CityModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string connectionstr = this._configuration.GetConnectionString("myConnectionString");
                    using (SqlConnection conn = new SqlConnection(connectionstr))
                    {
                        conn.Open();
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            if (model.CityID > 0)
                            {
                                cmd.CommandText = "PR_LOC_City_Update";
                                cmd.Parameters.AddWithValue("@CityID", model.CityID);
                            }
                            else
                            {
                                cmd.CommandText = "PR_LOC_City_Insert";
                            }

                            cmd.Parameters.AddWithValue("@CityName", model.CityName);
                            cmd.Parameters.AddWithValue("@CityCode", model.CityCode);
                            cmd.Parameters.AddWithValue("@StateID", model.StateID);
                            cmd.Parameters.AddWithValue("@CountryID", model.CountryID);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    TempData["Message"] = model.CityID > 0 ? "City updated successfully." : "City added successfully.";
                    return RedirectToAction("CityList");
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"An error occurred: {ex.Message}";
            }
            return View("AddEditCity", model);
        }

        #endregion


        #region Delete City
        public IActionResult Delete(int CityID)
        {
            string connectionstr = this._configuration.GetConnectionString("myConnectionString");
            using (SqlConnection conn = new SqlConnection(connectionstr))
            {
                conn.Open();
                using (SqlCommand sqlCommand = conn.CreateCommand())
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandText = "PR_LOC_City_Delete";
                    sqlCommand.Parameters.AddWithValue("@CityID", CityID);
                    sqlCommand.ExecuteNonQuery();
                }
            }
            return RedirectToAction("CityList");
        }
        #endregion
    }
}
