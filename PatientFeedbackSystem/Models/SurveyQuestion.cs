using System.ComponentModel.DataAnnotations;
namespace PatientFeedbackSystem.Models
{
    public class SurveyQuestion
    {
        [Key]
        public int QuestionID { get; set; }


        public int DepartmentID { get; set; }


        public string QuestionText { get; set; }


        public bool IsActive { get; set; }

    }
}