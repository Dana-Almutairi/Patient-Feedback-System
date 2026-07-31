using System.ComponentModel.DataAnnotations;
namespace PatientFeedbackSystem.Models
{
    public class SurveyAnswer
    {
        [Key]
        public int AnswerID { get; set; }


        public int SurveyID { get; set; }


        public int QuestionID { get; set; }


        public int Rating { get; set; }


        public string AnswerText { get; set; }

    }
}