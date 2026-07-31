using Microsoft.AspNetCore.Http;
using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc;
using PatientFeedbackSystem.Data;
using PatientFeedbackSystem.Models;

namespace PatientFeedbackSystem.Controllers
{
    public class PatientController : Controller
    {

        private readonly ApplicationDbContext _context;


        public PatientController(ApplicationDbContext context)
        {
            _context = context;
        }


        // Register (GET)

        public IActionResult Register()
        {
            return View();
        }


        // Register (POST)

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {

                Patient patient = new Patient()
                {
                    Name = model.Name,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Gender = model.Gender,
                    Age = model.Age,
                    Password = model.Password,
                    CreatedAt = DateTime.Now
                };


                try
                {
                    _context.Patients.Add(patient);

                    _context.SaveChanges();

                    return RedirectToAction("Login");
                }
                catch (Exception ex)
                {
                    return Content(ex.ToString());
                }

            }


            return View(model);

        }

        // Login (GET)

        public IActionResult Login()
        {
            return View();
        }



        // Login (POST)

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {

                var patient = _context.Patients.FirstOrDefault
                (x => x.Email == model.Email &&
                x.Password == model.Password);


                if (patient != null)
                {
                    HttpContext.Session.SetInt32
                    ("PatientID", patient.PatientID);

                    HttpContext.Session.SetString
                    ("PatientName", patient.Name);


                    return RedirectToAction("Dashboard");
                }

            }

            ViewBag.Message = "Invalid email or password.";
            return View(model);

        }





        public IActionResult Dashboard()
        {

            if (HttpContext.Session.GetInt32("PatientID") == null)
            {
                return RedirectToAction("Login");
            }


            return View();

        }



        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }

        public IActionResult PreviousSurveys()
        {

            int? patientID =
            HttpContext.Session.GetInt32("PatientID");


            if (patientID == null)
            {
                return RedirectToAction("Login");
            }


            var surveys = _context.Surveys
            .Where(x => x.PatientID == patientID)
            .OrderByDescending(x => x.DateSubmitted)
            .ToList();


            return View(surveys);

        }
    }
}