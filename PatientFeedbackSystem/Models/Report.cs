using System;

namespace PatientFeedbackSystem.Models
{
    public class Report
    {

        public int ReportID { get; set; }


        public int DepartmentID { get; set; }


        public double AverageRate { get; set; }


        public int NumberOfSurveys { get; set; }


        public DateTime CreatedAt { get; set; }

    }
}