using System;

namespace PatientFeedbackSystem.Models
{
    public class Survey
    {
        public int SurveyID { get; set; }

        public int PatientID { get; set; }

        public Patient Patient { get; set; }

        public DateTime DateSubmitted { get; set; }

        public int OverallRate { get; set; }

        public string Comments { get; set; }

        public string Status { get; set; }
    }
}