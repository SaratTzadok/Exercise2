using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace WebApplication1.Models
{
    public class User
    {
        public int Identity { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public string Telephone { get; set; }
        public DateTime? BornDate { get; set; }
        public DateTime? DateRecieveFirstVaccine { get; set; }
        public DateTime? DateRecieveSecondVaccine { get; set; }
        public DateTime? DateRecieveThirdVaccine { get; set; }
        public DateTime? DateRecieveFourthVaccine { get; set; }
        public string FirstVaccineManufacturer { get; set; }
        public string SecondVaccineManufacturer { get; set; }
        public string ThirdVaccineManufacturer { get; set; }
        public string FourthVaccineManufacturer { get; set; }
        public DateTime? DateRecievePositiveResult { get; set; }
        public DateTime? RecoveryDate { get; set; }
   


        static SqlConnection sqlCon = null;
        static String SqlconString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
        
        public static List<User> GetAllUsers()
        {
            List<User> lstUsers = new List<User>();
                      
            using (var command = new SqlCommand("GetAllUsers", new SqlConnection(SqlconString))
            {
                // Set command type and add Parameters
                CommandType = CommandType.StoredProcedure,
                Parameters = { }
            })
            {
                // Execute command in Adapter and store to dataset
                var adapter = new SqlDataAdapter(command);
                var dataset = new DataSet();
                adapter.Fill(dataset);
                // Display results in DatagridView
                DataTable dt = dataset.Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        User user = new User
                        {
                            Identity = Convert.ToInt32(dr["Identity"].ToString()),
                            FirstName = dr["FirstName"].ToString(),
                            LastName = dr["LastName"].ToString(),
                            Adress = dr["Adress"].ToString(),
                            Phone = dr["Phone"].ToString(),
                            Telephone = dr["Telephone"].ToString()
                            //TODO FILL ALL COLUMNS
                        };
                        lstUsers.Add(user);
                    }
                }
            }


            return lstUsers;
        }

        public User GetUserByID(int identity)
        {
            //User user = new User() ;
            using (var command = new SqlCommand("GetUserByID", new SqlConnection(SqlconString))
            {
                // Set command type and add Parameters
                CommandType = CommandType.StoredProcedure,
                Parameters = { new SqlParameter("@Identity", identity) }
            })
            {
                // Execute command in Adapter and store to dataset
                var adapter = new SqlDataAdapter(command);
                var dataset = new DataSet();
                adapter.Fill(dataset);
                // Display results in DatagridView
                DataTable dt = dataset.Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    this.Identity = Convert.ToInt32(dr["Identity"].ToString());
                    this.FirstName = dr["FirstName"].ToString();
                    this.LastName = dr["LastName"].ToString();
                    this.Adress = dr["Adress"].ToString();
                    this.Phone = dr["Phone"].ToString();
                    this.Telephone = dr["Telephone"].ToString();
                   // this.BornDate = Convert.ToDateTime(dr["BornDate"].ToString("dd/m/YYYY"));
                   // this.FirstVaccineManufacturer = dr["FirstVaccineManufacturer"].ToString();
                   // this.SecondVaccineManufacturer = dr["SecondVaccineManufacturer"].ToString();
                    //this.ThirdVaccineManufacturer = dr["ThirdVaccineManufacturer"].ToString();
                   // this.FourthVaccineManufacturer = dr["FourthVaccineManufacturer"].ToString();



                    //TODO FILL ALL COLUMNS

                }
    }
            return this;

        }

        public bool EditUser(User user)
        {
            int result;
            using (sqlCon = new SqlConnection(SqlconString))
            {
                sqlCon.Open();
                SqlCommand sql_cmnd = new SqlCommand("UpdateUser", sqlCon);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.Add(new SqlParameter("@Identity", user.Identity));
                sql_cmnd.Parameters.Add(new SqlParameter("@FirstName", user.FirstName));
                sql_cmnd.Parameters.Add(new SqlParameter("@LastName", user.LastName));
                sql_cmnd.Parameters.Add(new SqlParameter("@Adress", user.Adress));
                sql_cmnd.Parameters.Add(new SqlParameter("@Telephone", user.Telephone));
                sql_cmnd.Parameters.Add(new SqlParameter("@Phone", user.Phone));


                result = sql_cmnd.ExecuteNonQuery();
                sqlCon.Close();

            }
            if (result > 0)
                return true;
            else return false;
        }

        public bool CreateUser(User user)
        {
            int result;
            using (sqlCon = new SqlConnection(SqlconString))
            {
                sqlCon.Open();
                SqlCommand sql_cmnd = new SqlCommand("CreateUser", sqlCon);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.Add(new SqlParameter("@Identity",user.Identity));
                sql_cmnd.Parameters.Add(new SqlParameter("@FirstName",user.FirstName));
                sql_cmnd.Parameters.Add(new SqlParameter("@LastName", user.LastName));
                sql_cmnd.Parameters.Add(new SqlParameter("@Adress", user.Adress));
                sql_cmnd.Parameters.Add(new SqlParameter("@Telephone", user.Telephone));
                sql_cmnd.Parameters.Add(new SqlParameter("@Phone", user.Phone));

                result =  sql_cmnd.ExecuteNonQuery();              
                sqlCon.Close();
                
            }
            if (result > 0)
                return true;
            else return false;
        }

        public bool DeleteUser(int identity)
        {
            int result;
            using (sqlCon = new SqlConnection(SqlconString))
            {
                sqlCon.Open();
                SqlCommand sql_cmnd = new SqlCommand("DeleteUser", sqlCon);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.Add(new SqlParameter("@Identity", identity));
                
                result = sql_cmnd.ExecuteNonQuery();
                sqlCon.Close();

            }
            if (result > 0)
                return true;
            else return false;
        }
    }
}