using JobPortal.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace JobPortal.Repository
{
    public class AdminRepository
    {
        /// <summary>
        /// Connection veriable
        /// </summary>
        private SqlConnection con;

        /// <summary>
        /// To establish connection between server and the application
        /// </summary>
        private void connection()
        {
            string conn = ConfigurationManager.ConnectionStrings["mycon"].ToString();
            con = new SqlConnection(conn);

        }
        /// <summary>
        /// Admin login process
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        public bool AdminLogin(string username,string password)
        {
            try
            {
                connection();
                SqlCommand com = new SqlCommand("SP_AdminLogin", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Username", username);
                com.Parameters.AddWithValue("@Password",password);
                con.Open();
                int res = Convert.ToInt32(com.ExecuteScalar());
                return res > 0;
            }
            finally { con.Close(); }
        }
        /// <summary>
        /// Insert skills to the database
        /// </summary>
        /// <param name="obj">Skills model instance</param>
        /// <returns></returns>
        public bool AddSkill(Skills obj)
        {
            try
            {
                connection();
                SqlCommand com = new SqlCommand("SP_CreateSkill", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@SkillName", obj.SkillName);
                con.Open();
                int i = com.ExecuteNonQuery();
                return i > 0;
            }
            finally { con.Close(); }
        }
        /// <summary>
        /// Edit skill
        /// </summary>
        /// <param name="id">Skill id</param>
        /// <returns></returns>
        public bool EditSkill(Skills obj,int id)
        {
            try
            {
                connection();
                SqlCommand com = new SqlCommand("SP_UpdateSkill", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@SkillName", obj.SkillName);
                com.Parameters.AddWithValue("@SkillId", id);
                con.Open();
                int i = com.ExecuteNonQuery();
                return i > 0;
            }
            finally { con.Close(); }
        }
        /// <summary>
        /// Delete skill
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteSkill( int id)
        {
            try
            {
                connection();
                SqlCommand com = new SqlCommand("SP_DeleteSkill", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@SkillId", id);
                con.Open();
                int i = com.ExecuteNonQuery();
                return i > 0;
            }
            finally { con.Close(); }
        }
        /// <summary>
        /// Insert category to database
        /// </summary>
        /// <param name="cat">Category instance</param>
        /// <returns></returns>
        public bool AddCategory(Category cat)
        {
            try
            {
                connection();
                SqlCommand com = new SqlCommand("SP_CreateCategory", con);
                com.Parameters.AddWithValue("@CategoryName", cat.CategoryName);
                com.CommandType = CommandType.StoredProcedure;

                con.Open();
                int i = com.ExecuteNonQuery();
                return i > 0;
            }
            finally { con.Close(); }
        }
        /// <summary>
        /// Update category
        /// </summary>
        /// <param name="cat">Category instance</param>
        /// <returns></returns>
        public bool UpdateCategory(Category cat)
        {
            try
            {
                connection();
                SqlCommand com = new SqlCommand("SP_UpdateCategory", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@CategoryName", cat.CategoryName);
                com.Parameters.AddWithValue("@CategoryId", cat.CategoryId);
                con.Open();
                int i = com.ExecuteNonQuery();
                return i > 0;
            }
            finally { con.Close(); }
        }
        /// <summary>
        /// Delete category
        /// </summary>
        /// <param name="cat">category instance</param>
        /// <returns></returns>
        public bool DeleteCategory(int id)
        {
            try
            {
                connection();
                SqlCommand com = new SqlCommand("SP_DeleteCategory", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@CategoryId", id);
                con.Open();
                int i = com.ExecuteNonQuery();
                return i > 0;
            }
            finally { con.Close(); }
        }
        /// <summary>
        /// Employer approve 
        /// </summary>
        /// <param name="id"> Employer id</param>
        /// <returns></returns>
        public bool EmployerApprove(int id)
        {
            try
            {
                connection();
                SqlCommand com = new SqlCommand("SP_EmployerApprove", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@EmployerId", id);

                con.Open();
                int i = com.ExecuteNonQuery();
                return i >= 0;
            }
            finally { con.Close(); }
        }
        /// <summary>
        /// Employer reject
        /// </summary>
        /// <param name="id"> Employer id</param>
        /// <returns></returns>
        public bool EmployerReject(int id)
        {
            try
            {
                connection();
                SqlCommand com = new SqlCommand("SP_EmployerReject", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@EmployerId", id);
                con.Open();
                int i = com.ExecuteNonQuery();
                return i >= 0;
            }
            finally { con.Close(); }
        }
        /// <summary>
        /// Create new admin
        /// </summary>
        /// <param name="admin">Admin model</param>
        /// <returns></returns>
        public bool AddAdmin(Admin admin)
        {
            try
            {
                connection();
                admin.Password = BCrypt.Net.BCrypt.HashPassword(admin.Password);
                SqlCommand com = new SqlCommand("SP_CreateAdmin", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Name", admin.Name);
                com.Parameters.AddWithValue("@Username", admin.Username);
                com.Parameters.AddWithValue("@Password", admin.Password);
                com.Parameters.AddWithValue("@Email", admin.Email);
                com.Parameters.AddWithValue("@PhoneNumber", admin.PhoneNumber);
                con.Open();
                int i = com.ExecuteNonQuery();
                return i > 0;
            }
            finally { con.Close(); }    
        }
        /// <summary>
        /// Chnage Password
        /// </summary>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool ChangePassword (string oldPassword,string newPassword,string username)
        {
            try
            {
                connection();
                SqlCommand com = new SqlCommand("SP_ReadAdminPassword", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Username", username);
                con.Open();
                string password = Convert.ToString(com.ExecuteScalar());
                if (BCrypt.Net.BCrypt.Verify(oldPassword, password))
                {
                    com = new SqlCommand("SP_AdminChangePassword", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@Username", username);
                    newPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);
                    com.Parameters.AddWithValue("@NewPassword", newPassword);
                    int i = com.ExecuteNonQuery();
                    return i > 0;
                }
                else
                    return false;
            }
            finally
            {
                con.Close();
            }
        }

        /// <summary>
        /// Return s who viewed a particular job
        /// </summary>
        /// <param name="id">Job id</param>
        /// <returns></returns>
        public List<JobViewers> JobViewers(int id)
        {
            try
            {
                connection();
                List<JobViewers> jobViewers = new List<JobViewers>();
                SqlCommand com = new SqlCommand("SP_ReadJobView", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@JobId", id);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach(DataRow dr in dt.Rows)
                {
                    jobViewers.Add(new JobViewers
                    {
                        SeekerId = Convert.ToInt32(dr["SeekerID"]),
                        JobId   = Convert.ToInt32(dr["JobID"]),
                        ViewId = Convert.ToInt32(dr["ViewID"]),
                        Username = Convert.ToString(dr["username"]),
                        ViewDateTime = Convert.ToDateTime(dr["ViewDate"])
                    });
                }
                return jobViewers;
            }
            finally { con.Close(); }
        }
    }

}