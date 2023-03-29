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
        public int Feedback_ID { get; set; }
        //primary key for feedbacks table
        public string FeedbackDesc { get; set; }
        //Feedback description 
        [ForeignKey("Patient")]
        public int Patient_ID { get; set; }
        public virtual Patient Patient { get; set; }
        [ForeignKey("Appointment")]
        public int Appointment_ID { get; set; }
        public virtual Appointment Appointment { get; set; }
    }
    /*  
     * TODO: Add foreign keys for the following tables:
     * Appointment_ID
     * Paitent_ID
     * */
}