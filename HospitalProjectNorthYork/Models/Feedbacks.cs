using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProjectNorthYork.Models
{
    public class Feedbacks
    {
        [Key]
        //primary key for feedbacks table
        public int Feedback_ID { get; set; }
        //Feedback description 
        public string FeedbackDesc { get; set; }
        //associated patient
        [ForeignKey("Patient")]
        public int Patient_ID { get; set; }
        public virtual Patient Patient { get; set; }
        //associated appointment
        [ForeignKey("Appointment")]
        public int Appointment_ID { get; set; }
        public virtual Appointment Appointment { get; set; }
    }
    public class FeedbacksDto
    {
        public int Feedback_ID { get; set; }
        public string FeedbackDesc { get; set; }
        public int Patient_ID { get; set; }
        public string PatientName { get; set; }
        public int Appointment_ID { get;set; }
        public string AppointmentDesc { get; set; }
        public DateTime AppointmentDate { get; set; }

    }

}