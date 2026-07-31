using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PatientFeedbackSystem.Data;
using PatientFeedbackSystem.Models;

namespace PatientFeedbackSystem.Controllers
{
    public class SurveyController : Controller
    {

        private readonly ApplicationDbContext _context;


        public SurveyController(ApplicationDbContext context)
        {
            _context = context;
        }



        // GET

        public IActionResult TakeSurvey()
        {

            if (HttpContext.Session.GetInt32("PatientID") == null)
            {
                return RedirectToAction("Login", "Patient");
            }


            return View();
        }



        // POST

        [HttpPost]
        public IActionResult TakeSurvey(SurveyViewModel model)
        {

            if (ModelState.IsValid)
            {

                int patientID =
                HttpContext.Session.GetInt32("PatientID").Value;



                double overallRate =

                (model.Doctors +
                model.Nursing +
                model.Reception +
                model.Appointments +
                model.Emergency +
                model.Laboratory +
                model.Radiology +
                model.Pharmacy +
                model.Cleanliness) / 9.0;



                Survey survey = new Survey()
                {

                    PatientID = patientID,

                    DateSubmitted = DateTime.Now,

                    OverallRate = (int)Math.Round(overallRate),

                    Comments = model.Comments,

                    Status = "Completed"

                };


                _context.Surveys.Add(survey);

                _context.SaveChanges();
                int surveyID = survey.SurveyID;

                _context.SurveyAnswers.Add(new SurveyAnswer
                {
                    SurveyID = surveyID,
                    QuestionID = 1,
                    Rating = model.Doctors
                });

                _context.SurveyAnswers.Add(new SurveyAnswer
                {
                    SurveyID = surveyID,
                    QuestionID = 2,
                    Rating = model.Nursing
                });

                _context.SurveyAnswers.Add(new SurveyAnswer
                {
                    SurveyID = surveyID,
                    QuestionID = 3,
                    Rating = model.Reception
                });

                _context.SurveyAnswers.Add(new SurveyAnswer
                {
                    SurveyID = surveyID,
                    QuestionID = 4,
                    Rating = model.Appointments
                });

                _context.SurveyAnswers.Add(new SurveyAnswer
                {
                    SurveyID = surveyID,
                    QuestionID = 5,
                    Rating = model.Emergency
                });

                _context.SurveyAnswers.Add(new SurveyAnswer
                {
                    SurveyID = surveyID,
                    QuestionID = 6,
                    Rating = model.Laboratory
                });

                _context.SurveyAnswers.Add(new SurveyAnswer
                {
                    SurveyID = surveyID,
                    QuestionID = 7,
                    Rating = model.Radiology
                });

                _context.SurveyAnswers.Add(new SurveyAnswer
                {
                    SurveyID = surveyID,
                    QuestionID = 8,
                    Rating = model.Pharmacy
                });

                _context.SurveyAnswers.Add(new SurveyAnswer
                {
                    SurveyID = surveyID,
                    QuestionID = 9,
                    Rating = model.Cleanliness,
                    AnswerText = model.Comments
                });

                _context.SaveChanges();




                return RedirectToAction("Dashboard", "Patient");

            }


            return View(model);

        }


    }
}