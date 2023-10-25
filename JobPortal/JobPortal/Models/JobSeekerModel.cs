using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobPortal.Models
{
    /// <summary>
    /// Job seeker model
    /// </summary>
    public class JobSeekerModel
    {
        [Key]
        public int SeekerId { get; set; }
        [Required(ErrorMessage = "Enter the first name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Enter the last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Enter the email address")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please select a gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Select the date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime Birthdate { get; set; }

        [Required(ErrorMessage = "Invalid phone number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Enter a password")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public byte[] Resume { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Experience must be non-negative")]
        public double Experience { get; set; }

        public byte[] Image { get; set; }
        [Required(ErrorMessage = "Select a city")]
        public String City { get;set; }
        [Required(ErrorMessage = "Enter the address")]
        public string Address { get;set; }
        [Required(ErrorMessage = "Select a state")]
        public string State { get;set; }
        [Required(ErrorMessage = "Enter the username")]
        public string Username { get;set; }

        /// <summary>
        /// Encrypt the password
        /// </summary>
        /// <param name="password">Password</param>
        public void SetPassword(string password)
        {
            Password = BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
    /// <summary>
    /// Skills Model
    /// </summary>
    public class Skills
    {
        [Key]
        public int SkillId {get; set;}
        [Required(ErrorMessage ="Enter the skill ")]
        public string SkillName { get; set; }
    }
    /// <summary>
    /// Education details model of the job seeker
    /// </summary>
    public class EducationDetails
    {
        [Key]
        public int EducationId { get;set; }
        public int SeekerId { get; set; }
        public double Gpa { get; set; }
        public string Major { get; set; }
        public string Degree { get; set; }
        public string University {  get; set; }
        public int GraduationYear { get; set; }

    }
    /// <summary>
    /// Job seeker skills model
    /// </summary>
    public class JobSeekerSkills
    {
        [Key]
        public int JobSeekerSkillId { get; set;}
        public int SeekerId { get; set;}
        public string SkillName { get; set;}
        public int SkillId { get; set; }

    }
    /// <summary>
    /// Job Application Model - Job applications applied by the user
    /// </summary>
    public class JobApplication
    {
        [Key]
        public int JobApplicationID { get; set; }
        public int SeekerId { get; set; }
        public int JobId { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string Status { get; set; }
        public string JobTitle { get; set; }
        public string SeekerName { get; set; }
    }
    /// <summary>
    /// Show job seeker profile
    /// </summary>
    public class JobSeekerProfile
    {
        public JobSeekerModel JobSeekerDetails { get; set; }
        public List<EducationDetails> EducationDetails { get; set; }
        public List <JobSeekerSkills> Skills { get; set; }
        public List<Skills> AllSkills { get; set; }
    }
    /// <summary>
    /// Job details model
    /// </summary>
    public class JobDetails
    {
        public int JobID { get; set; }
        public int EmployerID { get; set; }
        public string JobTitle { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Location { get; set; }
        public decimal Salary { get; set; }
        public string EmploymentType { get; set; }
        public DateTime ApplicationDeadline { get; set; }
        public bool IsPublished { get; set; }
        public string CompanyName { get; set; }
        public string OfficialEmail { get; set; }
        public string Email { get; set; }
        public string ContactPhone { get; set; }
        public string Website { get; set; }
        public string EmployerName { get; set; }
        public string Designation { get; set; }
        public byte[] CompanyLogo { get; set; }
        public int NumberOfApplications { get; set; }
        public int NumberOfViews { get; set; }
    }

    /// <summary>
    /// View status of the job
    /// </summary>
    public class ViewJob
    {
        [Key]
        public int JobId { get; set; }
        public int SeekerId { get; set; }
        public DateTime ViewDate { get; set; }
    }
    /// <summary>
    /// Bookmark model
    /// </summary>
    public class Bookmark
    {
        [Key]
        public int BookmarkId { get; set; }
        public int JobId { get; set; }
        public int SeekerId { get; set; }
        public string JobTitle { get; set; }
    }
    /// <summary>
    /// View model for front page
    /// </summary>
    public class Index
    {
        public List<JobDetails> JobDetails { get; set; }
        public List<JobApplication> JobApplications { get; set; }
    }
    /// <summary>
    /// Chat message model
    /// </summary>
    public class ChatMessage
    {
        public int SeekerID { get; set; }
        public int EmployerID { get; set; }
        public int ChatID { get; set; }
        public int MessageId { get; set; }
        public string Message { get; set; }
        public string SeekerName { get; set; }
        public string CompanyName { get; set; }
        public DateTime DateAndTime { get; set; }
        public char Sender { get; set; }
    }
    /// <summary>
    /// Chat list
    /// </summary>
    public class ChatList
    {
        public int ChatID { get; set; }
        public int SeekerID { get; set; }
        public int EmployerID { get; set; }
        public string SeekerName { get; set; }
        public string CompanyName { get; set; }
    }
    public class ContactMessage
    {
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }
    }

}
