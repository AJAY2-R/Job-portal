using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BCrypt.Net;
namespace JobPortal.Models
{
    /// <summary>
    /// Employer model
    /// </summary>
    public class EmployerModel
    {
        [Key]
        public int EmployerID { get; set; }

        [Required(ErrorMessage = "Company Name is required")]
        [StringLength(100, ErrorMessage = "Company Name cannot exceed 100 characters")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Official Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string OfficialEmail { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter the phone number")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        public string ContactPhone { get; set; }

        [Required(ErrorMessage = "Enter the url ")]
        [Url(ErrorMessage = "Invalid Website URL")]
        public string Website { get; set; }
        [Display(Name = "Full name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Designation is required")]
        [StringLength(50, ErrorMessage = "Designation cannot exceed 50 characters")]
        public string Designation { get; set; }
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]

        public string Password { get; set; }
        public byte[] CompanyLogo { get; set; }
        public string Status { get; set; }
        public void SetPassword(string password)
        {
            Password = BCrypt.Net.BCrypt.HashPassword(password);
        }
        public bool VerifyPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, Password);
        }
    }
    /// <summary>
    /// Login model  
    /// </summary>
    public class Login
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool VerifyPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(Password, password);
        }
    }
    /// <summary>
    /// Category model
    /// </summary>
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Enter the category ")]
        public string CategoryName { get; set; }
    }
    /// <summary>
    /// Job vacancy model 
    /// </summary>
    public class JobVacancy
    {
        [Key]
        public int VacancyID { get; set; }
        public int EmployerID { get; set; }
        public string JobTitle { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }    
        public string Location { get; set; }
        public decimal Salary { get; set; }
        public string EmploymentType { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        [DataType(DataType.Date)]
        public DateTime ApplicationDeadline { get; set; }
        public bool IsPublished { get; set; }
    }
    /// <summary>
    /// Admin model
    /// </summary>
    public class Admin{
        [Key]
        public int AdminID { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

    }
    /// <summary>
    /// Vacancy view model
    /// </summary>
    public class VacancyViewModel
    {
        public JobDetails JobVacancies { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public int Views { get; set; }
    }
    /// <summary>
    /// Job Viewers
    /// </summary>
    public class JobViewers
    {
        public int ViewId { get; set; }
        public string Username { get; set; }
        public DateTime ViewDateTime { get; set; }
        public int JobId { get; set; }
        public int SeekerId { get; set; }
    }
}