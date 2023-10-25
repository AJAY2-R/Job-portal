using JobPortal.Models;
using JobPortal.Repository;
using Microsoft.Ajax.Utilities;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobPortal.Controllers
{
    public class EmployerController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                PublicRepository publicRespository = new PublicRepository();
                var vacency = publicRespository.GetJobDetails().Where(emp => emp.EmployerID == Convert.ToInt32(Session["EmployerId"]));
                return View(vacency);
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return View("Error");
            }
        }
        /// <summary>
        /// Add job vacancy
        /// </summary>
        /// <returns></returns>
        public ActionResult AddJobVacancy()
        {
            PublicRepository publicRespository = new PublicRepository();
            var categories = publicRespository.DisplayCategories();
            return View(categories);
        }
        [HttpPost]
        public ActionResult AddJobVacancy(JobVacancy obj)
        {
            try
            {
                int employerId = Convert.ToInt32(Session["EmployerId"]);
                EmployerRepository employerRepository = new EmployerRepository();
                if (employerRepository.AddJobVacancy(obj, employerId))
                {
                    TempData["Message"] = "Job vaccency published....";
                    return RedirectToAction("Index");
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
        /// View profile
        /// </summary>
        /// <returns></returns>
        public ActionResult ViewProfile()
        {
            EmployerRepository employerRepository = new EmployerRepository();
            return View(employerRepository.Employers().Find(model => model.EmployerID == Convert.ToInt32(Session["EmployerId"])));
        }
        public ActionResult UpdateProfile()
        {
            EmployerRepository employerRepository = new EmployerRepository();
            return View(employerRepository.Employers().Find(model => model.EmployerID == Convert.ToInt32(Session["EmployerId"])));
        }

        [HttpPost]
        public ActionResult UpdateProfile(EmployerModel employer, HttpPostedFileBase uploadedLogo )
        {
            try
            {
                EmployerRepository employerRepository = new EmployerRepository();
                if(employerRepository.UpdateEmployer(employer, uploadedLogo, Convert.ToInt32(Session["EmployerId"])))
                {
                    TempData["Message"] = "Updated";
                }
                return RedirectToAction("ViewProfile");
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return View("Error");
            }
        }
        /// <summary>
        /// Diplay vacancy created by the employer
        /// </summary>
        /// <returns></returns>
        public ActionResult Vacancies()
        {
            try
            {
                PublicRepository publicRepository = new PublicRepository();
                var vacency = publicRepository.GetJobDetails().Where(emp => emp.EmployerID == Convert.ToInt32(Session["EmployerId"]));
                return View(vacency);
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
        /// Update vacancy details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UpdateVacancy(int id)
        {
            PublicRepository publicRepository = new PublicRepository();
            var jobDetails= publicRepository.GetJobDetails().Find(job=>job.JobID == id);
            var categories = publicRepository.DisplayCategories();

            var viewModel = new VacancyViewModel
            {
                JobVacancies = jobDetails,
                Categories = categories
            };

            return View(viewModel);
        }
        [HttpPost]
        public ActionResult UpdateVacancy(JobVacancy jobVacancy)
        {
            try
            {
                EmployerRepository employerRepository = new EmployerRepository();
                if (employerRepository.UpdateJobVacancy(jobVacancy))
                {
                    TempData["Message"] = "Updated";
                }
                return RedirectToAction("Vacancies");
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return View("Error");
            }
        }
        /// <summary>
        /// Retrive applications 
        /// </summary>
        /// <param name="id">Job id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Applications(int id)
        {
            try
            {
                EmployerRepository employerRepository = new EmployerRepository();
                return View(employerRepository.GetJobApplications(id));
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return View("Error");
            }
        }
        /// <summary>
        /// Approve application
        /// </summary>
        /// <param name="id">Aplication id</param>
        /// <returns></returns>
        [HttpGet] 
        public ActionResult ApplicationApprove(int id,int aid)
        {
            try
            {
                EmployerRepository employerRepository = new EmployerRepository();
                if (employerRepository.JobApplicationApprove(id))
                {
                    TempData["Message"] = "Application Approved";
                }
                return RedirectToAction("Applications",new {id = aid});
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return View("Error");
            }
        }
        /// <summary>
        /// Reject application
        /// </summary>
        /// <param name="id">Application id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ApplicationReject(int id,int aid)
        {
            try
            {
                EmployerRepository employerRepository = new EmployerRepository();
                if (employerRepository.JobApplicationReject(id))
                {
                    TempData["Message"] = "Application Rejected";
                }
                return RedirectToAction("Applications",new {id=aid});
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return View("Error");
            }
        }
        /// <summary>
        /// Update status to read 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ApplicationRead(int id)
        {
            try
            {
                EmployerRepository employerRepository = new EmployerRepository();
                if (employerRepository.JobApplicationRead(id))
                {
                    return new HttpStatusCodeResult(200);
                }
                return new HttpStatusCodeResult(400);
            }
            catch(Exception) {
                return new HttpStatusCodeResult(400);
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
            PublicRepository publicRepository = new PublicRepository();
            var jobSeeker = jobSeekerRepository.JobSeekers().Find(model => model.SeekerId == id);
            var edu = jobSeekerRepository.GetEducationDetails(id);
            var userSkills = publicRepository.JobSeekerSkills(id);
            var viewModel = new JobSeekerProfile
            {
                JobSeekerDetails = jobSeeker,
                EducationDetails = edu,
                Skills = userSkills
            };
            return View(viewModel);
        }
        /// <summary>
        /// Employer chnage password 
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(string oldPassword, string newPassword)
        {
            try
            {
                EmployerRepository employerRepository = new EmployerRepository();
                if (employerRepository.ChangePassword(oldPassword, newPassword, Convert.ToInt32(Session["EmployerId"])))
                {
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
        /// Send message
        /// </summary>
        /// <param name="id">Seeker id</param>
        /// <returns></returns>
        public ActionResult SendMessage(int id)
        {
            PublicRepository publicRepository = new PublicRepository();
            int employerId =(int)Session["EmployerId"];
            var chats = publicRepository.ReadMessage(id,employerId);
            if (chats.Count == 0)
            {
                JobSeekerRepository jobSeekerRepository = new JobSeekerRepository();
                var jobSeeker = jobSeekerRepository.JobSeekers().Find(model => model.SeekerId == id);
                return View("Send-Message", jobSeeker);
            }
            return View(chats);
        }
        /// <summary>
        /// Send message
        /// </summary>
        /// <param name="id">Seeker id</param>
        /// <param name="message">Message</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SendMessage(int id, string message)
        {
            try
            {
                PublicRepository publicRepository = new PublicRepository();
                int  employerId= (int)Session["EmployerId"];
                char sender = 'E';
                if (publicRepository.SendMessage( id,employerId, message, sender))
                {
                    return new HttpStatusCodeResult(200);
                }
                return new HttpStatusCodeResult(400);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(500);
            }
        }
        /// <summary>
        /// Chat list of the Employer
        /// </summary>
        /// <returns></returns>
        public ActionResult ChatList()
        {
            try
            {
                EmployerRepository employerRepository = new EmployerRepository();
                int employerId = (int)Session["EmployerId"];
                return View(employerRepository.ChatList(employerId));
            }
            catch (Exception ex)
            {
                ExceptionLogging.SendErrorToText(ex);
                return View("Error");
            }
        }
        /// <summary>
        /// Logout employer
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session["EmployrId"] = null;
            Session["CompanyLogo"] = null; 
            Session["EmployerUsername"] = null;
            TempData["Message"] = "Logouted";
            return RedirectToAction("Index", "Home");
        }
    }
}