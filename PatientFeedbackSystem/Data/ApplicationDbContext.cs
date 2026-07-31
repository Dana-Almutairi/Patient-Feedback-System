using Microsoft.EntityFrameworkCore;
using PatientFeedbackSystem.Models;

namespace PatientFeedbackSystem.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext
        (DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {

        }


        public DbSet<Patient> Patients { get; set; }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Survey> Surveys { get; set; }

        public DbSet<SurveyQuestion> SurveyQuestions { get; set; }

        public DbSet<SurveyAnswer> SurveyAnswers { get; set; }

        public DbSet<Report> Reports { get; set; }


    }
}