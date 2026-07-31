using System.ComponentModel.DataAnnotations;

namespace PatientFeedbackSystem.Models
{
    public class SurveyViewModel
    {

        [Required]
        public int Doctors { get; set; }


        [Required]
        public int Nursing { get; set; }


        [Required]
        public int Reception { get; set; }


        [Required]
        public int Appointments { get; set; }


        [Required]
        public int Emergency { get; set; }


        [Required]
        public int Laboratory { get; set; }


        [Required]
        public int Radiology { get; set; }


        [Required]
        public int Pharmacy { get; set; }


        [Required]
        public int Cleanliness { get; set; }


        public string Comments { get; set; }


    }
}