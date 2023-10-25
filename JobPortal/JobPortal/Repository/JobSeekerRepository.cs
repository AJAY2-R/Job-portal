using JobPortal.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;

namespace JobPortal.Repository
{
    public class JobSeekerRepository
    {
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
        /// Register Job seeker 
        /// </summary>
        /// <param name="seeker">Job seeker model objct</param>
        /// <param name="imageUpload">Profile picture</param>
        /// <param name="resumeUpload">Resume</param>
        /// <returns></returns>
        public bool JobSeekerRegister(JobSeekerModel seeker, HttpPostedFileBase imageUpload, HttpPostedFileBase resumeUpload)
        {
            try
            {
                using (BinaryReader binaryReader = new BinaryReader(imageUpload.InputStream))
                {
                    seeker.Image = binaryReader.ReadBytes(imageUpload.ContentLength);
                }
                using (BinaryReader binaryReader = new BinaryReader(resumeUpload.InputStream))
                {
                    seeker.Resume = binaryReader.ReadBytes(resumeUpload.ContentLength);
                }
                seeker.SetPassword(seeker.Password);
                connection();
                SqlCommand com = new SqlCommand("SP_CreateJobSeeker", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@FirstName", seeker.FirstName);
                com.Parameters.AddWithValue("@LastName", seeker.LastName);
                com.Parameters.AddWithValue("@BirthDate", seeker.Birthdate);
                com.Parameters.AddWithValue("@Email", seeker.Email);
                com.Parameters.AddWithValue("@Gender", seeker.Gender);
                com.Parameters.AddWithValue("@PhoneNumber", seeker.PhoneNumber);
                com.Parameters.AddWithValue("Password", seeker.Password);
                com.Parameters.AddWithValue("@Experience", seeker.Experience);
                com.Parameters.AddWithValue("@Image", seeker.Image);
                com.Parameters.AddWithValue("@Resume", seeker.Resume);
                com.Parameters.AddWithValue("@State", seeker.State);
                com.Parameters.AddWithValue("@City", seeker.City);
                com.Parameters.AddWithValue("@Username", seeker.Username);
                com.Parameters.AddWithValue("@Address", seeker.Address);
                con.Open();
                int i = com.ExecuteNonQuery();
                return i > 0;
            }
            finally { con.Close(); }
        }
        /// <summary>
        /// Update job seeker
        /// </summary>
        /// <param name="seeker"></param>
        /// <param name="imageUpload"></param>
        /// <returns></returns>
        public bool JobSeekerUpdate(JobSeekerModel seeker, HttpPostedFileBase imageUpload,int seekerId)
        {
            try
            {
                if (imageUpload != null)
                {
                    using (BinaryReader binaryReader = new BinaryReader(imageUpload.InputStream))
                    {
                        seeker.Image = binaryReader.ReadBytes(imageUpload.ContentLength);
                    }
                }
                else
                    imageUpload = null;
                connection();
                SqlCommand com = new SqlCommand("SP_UpdateJobSeeker", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@SeekerId", seekerId);
                com.Parameters.AddWithValue("@FirstName", seeker.FirstName);
                com.Parameters.AddWithValue("@LastName", seeker.LastName);
                com.Parameters.AddWithValue("@BirthDate", seeker.Birthdate);
                com.Parameters.AddWithValue("@Email", seeker.Email);
                com.Parameters.AddWithValue("@Gender", seeker.Gender);
                com.Parameters.AddWithValue("@PhoneNumber", seeker.PhoneNumber);
                com.Parameters.AddWithValue("@Experience", seeker.Experience);
                com.Parameters.AddWithValue("@ProfilePicture", seeker.Image);
                com.Parameters.AddWithValue("@State", seeker.State);
                com.Parameters.AddWithValue("@City", seeker.City);
                com.Parameters.AddWithValue("@Address", seeker.Address);
                con.Open();
                int i = com.ExecuteNonQuery();
                return i > 0;
            }
            finally { con.Close(); }
        }
        /// <summary>
        /// Update resume
        /// </summary>
        /// <param name="resume">Resume file </param>
        /// <param name="seekerId">Job seeker id</param>
        /// <returns></returns>
        public bool UpdateResume(HttpPostedFileBase resume,int seekerId)
        {
            try
            {
                byte[] binaryResume;
                using (BinaryReader  binaryReader = new BinaryReader(resume.InputStream))
                {
                    binaryResume = binaryReader.ReadBytes(resume.ContentLength);
                }
                connection();
                SqlCommand com = new SqlCommand("SP_UpdateJobSeekerResume", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@SeekerId", seekerId);
                com.Parameters.AddWithValue("Resume", binaryResume);
                con.Open();
                int i = com.ExecuteNonQuery();
                return i > 0;
            }
            finally { con.Close(); }
        }
    
        /// <summary>
        /// Display the job seeker deatails
        /// </summary>
        /// <param name="id">Jobseeker id</param>
        /// <returns></returns>
        public List<JobSeekerModel> JobSeekers ()
        {
            try
            {
                connection();
                SqlCommand com = new SqlCommand("SP_ReadJobSeeker", con);
                List<JobSeekerModel> jobSeeker = new List<JobSeekerModel>();
                com.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                con.Open();
                da.Fill(dt);

                foreach(DataRow dr in dt.Rows)
                {
                    jobSeeker.Add(new JobSeekerModel()
                    {
                        SeekerId = Convert.ToInt32(dr["SeekerID"]),
                        FirstName = Convert.ToString(dr["FirstName"]),
                        LastName = Convert.ToString(dr["LastName"]),
                        Birthdate = Convert.ToDateTime(dr["Birthdate"]).Date,
                        State = Convert.ToString(dr["State"]),
                        City = Convert.ToString(dr["City"]),
                        Address = Convert.ToString(dr["Address"]),
                        Email = Convert.ToString(dr["Email"]),
                        Image = (byte[])dr["ProfilePicture"],
                        Gender = Convert.ToString(dr["Gender"]),
                        PhoneNumber = Convert.ToString(dr["PhoneNumber"]),
                        Experience = Convert.ToInt32(dr["Experience"]),
                        Username = Convert.ToString(dr["Username"]),
                        Resume = (byte[])dr["Resume"]
                    }) ; 
                }
                return jobSeeker;
            }finally { con.Close(); }
        }

        /// <summary>
        /// Add job seeker education details to database
        /// </summary>
        /// <param name="educationList"></param>
        /// <returns></returns>
        public bool AddEducationDetails(EducationDetails obj ,int id)
        {
            try
            {
                connection();
                con.Open();
                    obj.SeekerId = id;
                    SqlCommand com = new SqlCommand("SP_CreateEducationDetail", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@SeekerId", obj.SeekerId);
                    com.Parameters.AddWithValue("@Gpa", obj.Gpa);
                    com.Parameters.AddWithValue("@Major", obj.Major);
                    com.Parameters.AddWithValue("@Degree", obj.Degree);
                    com.Parameters.AddWithValue("@University", obj.University);
                    com.Parameters.AddWithValue("@GraduationYear", obj.GraduationYear);

                    int i = com.ExecuteNonQuery();
                return i > 0;            
            }
            finally
            {
                con.Close();
            }
        }
        /// <summary>
        /// Update eucational details
        /// </summary>
        /// <param name="obj">Education details instance</param>
        /// <returns></returns>
        public bool UpdateEducationDetails(EducationDetails  obj)
        {
            try
            {
                connection();
                con.Open();
                SqlCommand com = new SqlCommand("SP_UpdateEducationDetail", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@EducationID", obj.EducationId);
                com.Parameters.AddWithValue("@Gpa", obj.Gpa);
                com.Parameters.AddWithValue("@Major", obj.Major);
                com.Parameters.AddWithValue("@Degree", obj.Degree);
                com.Parameters.AddWithValue("@University", obj.University);
                com.Parameters.AddWithValue("@GraduationYear", obj.GraduationYear);

                int i = com.ExecuteNonQuery();
                return i > 0;
            }
            finally
            {
                con.Close();
            }
        }
        /// <summary>
        /// Delete education deatail
        /// </summary>
        /// <param name="id">Education id</param>
        /// <returns></returns>
        public bool DeleteEducationDetails(int id)
        {
            try
            {
                connection();
                con.Open();
                SqlCommand com = new SqlCommand("SP_DeleteEducationDetail", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@EducationID", id);
                int i = com.ExecuteNonQuery();
                return i > 0;
            }
            finally
            {
                con.Close();
            }
        }
        public bool DeleteJobSeekerSkill(int id)
        {
            try
            {
                connection();
                con.Open();
                SqlCommand com = new SqlCommand("SP_DeleteJobSeekerSkill", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@JobSeekerSkillID", id);
                int i = com.ExecuteNonQuery();
                return i > 0;
            }
            finally
            {
                con.Close();
            }
        }
        /// <summary>
        /// Dispaly education deatils
        /// </summary>
        /// <param name="seekerId">Job seeker id</param>
        /// <returns></returns>
        public List<EducationDetails> GetEducationDetails(int seekerId)
        {
            try
            {
                connection();
                SqlCommand com = new SqlCommand("SP_ReadEducationDetails", con);
                List<EducationDetails> educationDetails = new List<EducationDetails>();
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@SeekerId", seekerId);
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                con.Open();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    educationDetails.Add(new EducationDetails()
                    {
                        EducationId = Convert.ToInt32(dr["EducationID"]),
                        Gpa = Convert.ToDouble(dr["GPA"]),
                        Major = Convert.ToString(dr["Major"]),
                        Degree = Convert.ToString(dr["Degree"]),
                        University = Convert.ToString(dr["University"]),
                        GraduationYear = Convert.ToInt32(dr["GraduationYear"])
                    }) ;
                }
                
                return educationDetails;
            }
            finally { con.Close(); }
        }
        /// <summary>
        /// Apply for  job 
        /// </summary>
        /// <param name="application">Job application model </param>
        /// <returns></returns>
        public bool CreateJobApplication(JobApplication application)
        {
            try
            {
                connection();
                SqlCommand com = new SqlCommand("SP_CreateJobApplication", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@JobID", application.JobApplicationID);
                com.Parameters.AddWithValue("@SeekerID", application.SeekerId);
                com.Parameters.AddWithValue("@ApplicationDate", application.ApplicationDate);
                con.Open();
                int rowsAffected = com.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            finally { con.Close(); }
        }

        /// <summary>
        /// Display job application submited by the seeker
        /// </summary>
        /// <param name="SeekerId"></param>
        /// <returns></returns>
        public List<JobApplication> GetJobApplications(int SeekerId)
        {
            try
            {
                connection();
                SqlCommand com = new SqlCommand("SP_ReadJobApplicationSeeker", con);
                List<JobApplication> jobApplications = new List<JobApplication>();
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@SeekerId", SeekerId);
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                con.Open();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    jobApplications.Add(new JobApplication()
                    {
                        JobApplicationID = Convert.ToInt32(dr["ApplicationID"]),
                        JobId = Convert.ToInt32(dr["JobId"]),
                        SeekerId = Convert.ToInt32(dr["SeekerID"]),
                        ApplicationDate = Convert.ToDateTime(dr["ApplicationDate"]),
                        Status = Convert.ToString(dr["Status"]),
                        JobTitle = Convert.ToString(dr["JobTitle"])
                    });
                }
                return jobApplications;
            }
            finally { con.Close(); }
        }
        public List<Bookmark> GetBookmarks(int SeekerId)
        {
            try
            {
                connection();
                SqlCommand com = new SqlCommand("SP_ReadBookMarks", con);
                List<Bookmark> bookmarks = new List<Bookmark>();
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@SeekerId", SeekerId);
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                con.Open();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    bookmarks.Add(new Bookmark()
                    {
                        JobId = Convert.ToInt32(dr["JobId"]),
                        BookmarkId = Convert.ToInt32(dr["BookmarkId"]),
                        JobTitle = Convert.ToString(dr["JobTitle"])
                    });
                }
                return bookmarks;
            }
            finally { con.Close(); }
        }
        /// <summary>
        /// User visited the job
        /// </summary>
        public bool VisitJob(ViewJob obj)
        {
            try
            {
                connection();
                SqlCommand com = new SqlCommand("SP_CreateJobView", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@JobId", obj.JobId);
                com.Parameters.AddWithValue("@SeekerId", obj.SeekerId);
                com.Parameters.AddWithValue("@ViewDate", DateTime.Now);
                con.Open();
                int i = com.ExecuteNonQuery();
                return i > 0;
            }
            finally { con.Close(); }
        }
        /// <summary>
        /// Save and delete job bookmarks
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Bookmark(Bookmark obj)
        {
            try
            {
                connection();
                SqlCommand com = new SqlCommand("SP_Bookmark", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@JobId", obj.JobId);
                com.Parameters.AddWithValue("@SeekerId", obj.SeekerId);
                con.Open();
                int i = com.ExecuteNonQuery();
                return i > 0;
            }
            finally { con.Close(); }
        }

        /// <summary>
        /// Change password job seeker
        /// </summary>
        /// <param name="oldPassword">Old password</param>
        /// <param name="newPassword">New Password</param>
        /// <param name="employerId">job seeker id</param>
        /// <returns></returns>
        public bool ChangePassword(string oldPassword, string newPassword, int seekerId)
        {
            try
            {
                connection();
                SqlCommand com = new SqlCommand("SP_ReadJobSeekerPassword", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@SeekerId", seekerId);
                con.Open();
                string password = Convert.ToString(com.ExecuteScalar());
                if (BCrypt.Net.BCrypt.Verify(oldPassword, password))
                {
                    com = new SqlCommand("SP_ChangeJobSeekerPassword", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@SeekerId", seekerId);
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
        /// Add skills to the job seeker
        /// </summary>
        /// <param name="skillId">Skill id</param>
        /// <param name="seekerId">Seeker id</param>
        /// <returns></returns>
        public bool AddSkill(int skillId,int seekerId)
        {
            try
            {
                connection();
                SqlCommand com = new SqlCommand("SP_CreateJobSeekerSkills", con);
                com.CommandType = CommandType.StoredProcedure;
                con.Open();
                com.Parameters.Clear();
                com.Parameters.AddWithValue("@SeekerId", seekerId);
                com.Parameters.AddWithValue("@SkillId", skillId);
                int i= com.ExecuteNonQuery();
                return i > 0;
            }
            finally { con.Close(); }
        }
        /// <summary>
        /// Chat list of the job seeker
        /// </summary>
        /// <param name="seekerId">Seeker id</param>
        /// <returns></returns>
        public List<ChatList> ChatList(int seekerId)
        {
            try
            {
                connection();
                List<ChatList> chats = new List<ChatList>();
                SqlCommand com = new SqlCommand("SP_ChatListSeeker", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@SeekerID", seekerId);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    chats.Add(new ChatList
                    {
                        SeekerID = Convert.ToInt32(dr["SeekerID"]),
                        EmployerID = Convert.ToInt32(dr["EmployerID"]),
                        ChatID = Convert.ToInt32(dr["ChatID"]),
                        SeekerName = dr["SeekerName"].ToString(),
                        CompanyName = dr["CompanyName"].ToString(),
                    });
                }
                return chats;
            }
            finally { con.Close(); }
        }
    }
}