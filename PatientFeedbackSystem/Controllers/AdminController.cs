using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Linq;
using PatientFeedbackSystem.Data;
using PatientFeedbackSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace PatientFeedbackSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Admin model)
        {
            var admin = _context.Admins.FirstOrDefault(x =>
    x.Email == model.Email &&
    x.Password == model.Password);

            if (admin != null)
            {
                HttpContext.Session.SetString("AdminEmail", admin.Email);
                return RedirectToAction("Dashboard");
            }

            ViewBag.Message = "Invalid Username or Password";

            return View();
        }


        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("AdminEmail") == null)
            {
                return RedirectToAction("Login");
            }

            ViewBag.TotalPatients = _context.Patients.Count();


            ViewBag.TotalSurveys = _context.Surveys.Count();


            ViewBag.AverageRating = _context.Surveys.Any()
                ? _context.Surveys.Average(x => x.OverallRate)
                : 0;


            ViewBag.RecentSurveys = _context.Surveys
                .Include(x => x.Patient)
                .OrderByDescending(x => x.DateSubmitted)
                .Take(5)
                .ToList();


            ViewBag.TodaySurveys = _context.Surveys
                .Count(x => x.DateSubmitted.Date == DateTime.Today);


            return View();
        }


        public IActionResult Reports()
        {
            if (HttpContext.Session.GetString("AdminEmail") == null)
            {
                return RedirectToAction("Login");
            }

            ViewBag.TotalPatients = _context.Patients.Count();

            ViewBag.TotalSurveys = _context.Surveys.Count();

            ViewBag.AverageRating = _context.Surveys.Any()
                ? _context.Surveys.Average(x => x.OverallRate)
                : 0;

            ViewBag.HighestRating = _context.Surveys.Any()
                ? _context.Surveys.Max(x => x.OverallRate)
                : 0;

            ViewBag.LowestRating = _context.Surveys.Any()
                ? _context.Surveys.Min(x => x.OverallRate)
                : 0;

            return View();
        }


        public IActionResult Statistics()
        {
            if (HttpContext.Session.GetString("AdminEmail") == null)
            {
                return RedirectToAction("Login");
            }

            ViewBag.TotalPatients = _context.Patients.Count();

            ViewBag.TotalSurveys = _context.Surveys.Count();


            ViewBag.AverageRating = _context.Surveys.Any()
                ? _context.Surveys.Average(x => x.OverallRate)
                : 0;


            ViewBag.HighestRating = _context.Surveys.Any()
                ? _context.Surveys.Max(x => x.OverallRate)
                : 0;


            ViewBag.LowestRating = _context.Surveys.Any()
                ? _context.Surveys.Min(x => x.OverallRate)
                : 0;


            return View();
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }

        public IActionResult AllSurveys
          (
               string search,
               DateTime? startDate,
               DateTime? endDate
          )
        {
            if (HttpContext.Session.GetString("AdminEmail") == null)
            {
                return RedirectToAction("Login");
            }

            var surveys = _context.Surveys
                .Include(x => x.Patient)
                .OrderByDescending(x => x.DateSubmitted)
                .AsQueryable();


            if (!string.IsNullOrEmpty(search))
            {
                surveys = surveys.Where
                (x => x.Patient.Name.Contains(search));
            }


            if (startDate.HasValue)
            {
                surveys = surveys.Where
                (x => x.DateSubmitted >= startDate.Value);
            }


            if (endDate.HasValue)
            {
                surveys = surveys.Where
                (x => x.DateSubmitted <= endDate.Value);
            }


            return View(surveys.ToList());

        }

        public IActionResult SurveyDetails(int id)
        {
            if (HttpContext.Session.GetString("AdminEmail") == null)
            {
                return RedirectToAction("Login");
            }

            var survey = _context.Surveys
                .Include(x => x.Patient)
                .FirstOrDefault(x => x.SurveyID == id);


            return View(survey);
        }

        public IActionResult DeleteSurvey(int id)
        {
            if (HttpContext.Session.GetString("AdminEmail") == null)
            {
                return RedirectToAction("Login");
            }

            var answers = _context.SurveyAnswers
                .Where(x => x.SurveyID == id)
                .ToList();

            _context.SurveyAnswers.RemoveRange(answers);

            var survey = _context.Surveys.FirstOrDefault(x => x.SurveyID == id);

            if (survey != null)
            {
                _context.Surveys.Remove(survey);
            }

            _context.SaveChanges();

            return RedirectToAction("AllSurveys");
        }

        public IActionResult PatientDetails(int id)
        {
            if (HttpContext.Session.GetString("AdminEmail") == null)
            {
                return RedirectToAction("Login");
            }

            var patient = _context.Patients
                .FirstOrDefault(x => x.PatientID == id);

            return View(patient);
        }

    }
}