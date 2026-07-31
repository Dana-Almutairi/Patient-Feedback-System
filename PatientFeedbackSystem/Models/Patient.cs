using System;
using System.Collections.Generic;

namespace PatientFeedbackSystem.Models
{
    public class Patient
    {

        public int PatientID { get; set; }


        public string Name { get; set; }


        public string Email { get; set; }


        public string PhoneNumber { get; set; }


        public string Password { get; set; }


        public string Gender { get; set; }


        public int Age { get; set; }


        public DateTime CreatedAt { get; set; }

        public ICollection<Survey> Surveys { get; set; }

    }
}