using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using JobPortal.Models;
using JobPortal.Repository;
using Microsoft.Ajax.Utilities;

namespace JobPortal.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {  
        /// <summary>
        /// Admin index page
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            try
            {
                PublicRepository publicRepository = new PublicRepository();
                var jobs = publicRepository.GetJobDetails();
                return View(jobs);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        
        /// <summary>
        /// Add Skills
        /// </summary>
        /// <returns></returns>
        public ActionResult AddSkills()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSkills(Skills obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AdminRepository adminRepository = new AdminRepository();//Repository instance
                    if (adminRepository.AddSkill(obj))
                    {
                        TempData["Message"] = "Skill added ";
                        return RedirectToAction("AddSkills");
                    }
                }
                return View();
            }
            catch(Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return View("Error");
            }
        }
        /// <summary>
        /// Show skills
        /// </summary>
        /// <returns></returns>
        public ActionResult Skills()
        {
            PublicRepository publicRepository = new PublicRepository();
            var skills = publicRepository.DisplaySkills();
            return View(skills);
        }
        /// <summary>
        /// Display Categories
        /// </summary>
        /// <returns></returns>
        public ActionResult Categories()
        {
            PublicRepository publicRepository = new PublicRepository();
            var category = publicRepository.DisplayCategories();
            return View(category);
        }
        /// <summary>
        /// Edit skills
        /// </summary>
        /// <param name="id">Skill id</param>
        /// <returns></returns>
        public ActionResult EditSkill(int id)
        {
            PublicRepository publicRepository = new PublicRepository();
            var skill = publicRepository.DisplaySkills().Find(sk => sk.SkillId == id);
            return View(skill);
        }
        /// <summary>
        /// Edit skill
        /// </summary>
        /// <param name="id">Skill id</param>
        /// <param name="obj">Skill object</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditSkill(int id, Skills obj)
        {
            try
            {
                AdminRepository adminRepository = new AdminRepository();
                if (adminRepository.EditSkill(obj, id))
                {
                    TempData["Message"] = "Skill Updated";
                    return RedirectToAction("Skills", "Admin");
                   
                }
                return View();
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return View("Error");
            }
        }
        public ActionResult UpdateCategory(int id)
        {
            try
            {
                PublicRepository publicRepository = new PublicRepository();
                return View(publicRepository.DisplayCategories().Find(cat => cat.CategoryId == id));
             }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return View("Error");
            }

        }
        /// <summary>
        /// Edit category
        /// </summary>
        /// <param name="obj">Category object</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateCategory(Category obj)
        {
            try
            {
                AdminRepository adminRepository = new AdminRepository();
                if (adminRepository.UpdateCategory(obj))
                {
                    TempData["Message"] = "Category Updated";
                }
                return RedirectToAction("Categories", "Admin");
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return View("Error");
            }
        }
        /// <summary>
        /// Delete skill
        /// </summary>
        /// <param name="id">Skill id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteSkill(int id)
        {
            try
            {
                AdminRepository adminRepository = new AdminRepository();
                if (adminRepository.DeleteSkill(id))
                {
                    TempData["Message"] = "Skill deleted";
                }
                return RedirectToAction("Skills");
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return View("Error");
            }
        }
        /// <summary>
        /// Delete category
        /// </summary>
        /// <param name="id">Skill id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteCategory(int id)
        {
            try
            {
                AdminRepository adminRepository = new AdminRepository();
                if (adminRepository.DeleteCategory(id))
                {
                    TempData["Message"] = "Category deleted";
                }
                return RedirectToAction("Categories");
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return View("Error");
            }
        }
        /// <summary>
        /// Get details of the jobs
        /// </summary>
        /// <param name="id">Job id</param>
        /// <returns></returns>
        public ActionResult JobDetails(int id)
        {
            PublicRepository publicRepository = new PublicRepository();
            var jobDetails = publicRepository.GetJobDetails().Find(model => model.JobID == id);
            return View(jobDetails);

        }
        /// <summary>
        /// Filter the job details based on the search string
        /// </summary>
        /// <param name="search">Search string</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Jobs(string search)
        {
            try
            {
                PublicRepository publicRepository = new PublicRepository();
                var jobs = publicRepository.GetJobDetails();
                if (!string.IsNullOrEmpty(search))
                {
                    jobs = jobs.Where(job => job.JobTitle.Contains(search) || job.CategoryName.Contains(search) || job.Location.Contains(search) && job.ApplicationDeadline > DateTime.Now && job.IsPublished).ToList();
                }

                return View(jobs);
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return View("Error");
            }
        }

        /// <summary>
        /// Add category
        /// </summary>
        /// <returns></returns>
        public ActionResult AddCategory()
        {
            return View();  
        }
        /// <summary>
        /// Add Category
        /// </summary>
        /// <param name="cat">Category model instance </param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddCategory(Category cat)
        {
            try
            {
                AdminRepository adminRepository = new AdminRepository();
                if (adminRepository.AddCategory(cat))
                {
                    TempData["Message"] = "Category added ";
                    return RedirectToAction("AddCategory");
                   
                }
                return View();
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return View("Error");
            }
        }
        /// <summary>
        /// Edit category
        /// </summary>
        /// <param name="id">Category id</param>
        /// <returns></returns>
        public ActionResult EditCategory(int id)
        {
            try
            {
                PublicRepository publicRepository = new PublicRepository();
                var category = publicRepository.DisplayCategories().Find(cat => cat.CategoryId == id);
                return View(category);
            }catch(Exception ex)
            {
                return View(ex.Message);    
            }
        } 
        /// <summary>
        /// Pending verfication list of Employer
        /// </summary>
        /// <returns></returns>
        public ActionResult VerifyEmployer()
        {
            try
            {
                EmployerRepository employerRepository = new EmployerRepository();
                var employer = employerRepository.Employers().Where(emp => emp.Status == "Pending");
                return View(employer);
            }catch(Exception ex)
            {
                return View(ex.Message);
            }
        }
        /// <summary>
        /// Approve Employer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EmployerApprove(int id)
        {
            try
            {
                AdminRepository adminRepository = new AdminRepository();
                if (adminRepository.EmployerApprove(id))
                {
                    TempData["Message"] = "Employer approved..";
                    return Redirect(Request.UrlReferrer.ToString());
                }
                return View();
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return View("Error");
            }
        }
        /// <summary>
        /// Employer reject
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EmployerReject(int id)
        {
            try
            {
                AdminRepository adminRepository = new AdminRepository();
                if (adminRepository.EmployerReject(id))
                {
                    TempData["Message"] = "Employer rejected..";
                    return Redirect(Request.UrlReferrer.ToString());
                }
                return View();
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return View("Error");
            }
        }
        /// <summary>
        /// Display all the job seekers
        /// </summary>
        /// <returns></returns>
        public ActionResult JobSeekers()
        {
            JobSeekerRepository jobSeekerRepository = new JobSeekerRepository();
            return View(jobSeekerRepository.JobSeekers());
        }
        /// <summary>
        /// Display all the Employers 
        /// </summary>
        /// <returns></returns>
        public ActionResult Employers()
        {
            EmployerRepository employerRepository = new EmployerRepository();
            return View(employerRepository.Employers());
        }
        /// <summary>
        /// Display all the jobs
        /// </summary>
        /// <returns></returns>
        public ActionResult Jobs()
        {
            try
            {
                PublicRepository publicRepository = new PublicRepository();
                DateTime currentDate = DateTime.Now;
                var jobs = publicRepository.GetJobDetails().Where(job => job.ApplicationDeadline >= currentDate && job.IsPublished).ToList();
                return View(jobs);
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return View("Error");
            }
        }

        /// <summary>
        /// Add admin
        /// </summary>
        /// <returns></returns>
        public ActionResult AddAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAdmin(Admin admin)
        {
            AdminRepository adminRepository = new AdminRepository();
            try
            {
                if (adminRepository.AddAdmin(admin)) {
                    TempData["Message"] = "Admin added successfully"
;                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return View("Error");
            }
        }
        /// <summary>
        /// Admin chnage password 
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(string oldPassword,string newPassword)
        {
            try
            {
                AdminRepository adminRepository = new AdminRepository();
                if (adminRepository.ChangePassword(oldPassword, newPassword, Convert.ToString(Session["Admin"]))){
                    TempData["Message"] = "Password changed";
                }
                else
                {
                    TempData["Message"] = "Wrong password";
                    return View();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return View("Error");
            }
        }
        /// <summary>
        /// Check username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CheckUsername(string username)
        {
            try
            {
                PublicRepository publicRepository = new PublicRepository();

                if (!publicRepository.CheckUsername(username))
                {
                    return new HttpStatusCodeResult(200);
                }
                return new HttpStatusCodeResult(202);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        /// <summary>
        /// List the job viewers
        /// </summary>
        /// <param name="id">Job id</param>
        /// <returns></returns>
        public ActionResult JobViews(int id)
        {
            AdminRepository adminRepository = new AdminRepository();
            try
            {
                return View(adminRepository.JobViewers(id));
            }catch(Exception ex)
            {
                return View(ex.Message);
            }
        }
        /// <summary>
        /// View applicant details
        /// </summary>
        /// <param name="id">Job seeker id</param>
        /// <returns></returns>
        public ActionResult JobSeekerProfile(int id)
        {
            JobSeekerRepository jobSeekerRepository = new JobSeekerRepository();
            PublicRepository repo = new PublicRepository();
            var jobSeeker = jobSeekerRepository.JobSeekers().Find(model => model.SeekerId == id);
            var edu = jobSeekerRepository.GetEducationDetails(id);
            var userSkills = repo.JobSeekerSkills(id);
            var viewModel = new JobSeekerProfile
            {
                JobSeekerDetails = jobSeeker,
                EducationDetails = edu,
                Skills = userSkills
            };
            return View(viewModel);
        }
        public ActionResult ContactMessages()
        {
            PublicRepository publicRepository = new PublicRepository(); 
            return View(publicRepository.DisplayContactMessages());
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            TempData["Message"] = "Logouted";
            return RedirectToAction("Index", "Home");
        }
    }
}