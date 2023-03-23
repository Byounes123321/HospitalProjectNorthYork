using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HospitalProjectNorthYork.Models
{
    public class Feedbacks
    {
        [Key]
        public int Feedback_ID { get; set; }
        //primary key for feedbacks table
        public string FeedbackDesc { get; set; }
        //Feedback description 
    }
    /*  
     * TODO: Add foreign keys for the following tables:
     * Appointment_ID
     * Paitent_ID
     * */
}