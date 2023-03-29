using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HospitalProjectNorthYork.Models
{
    public class Visit
    {
        [Key]
        public int Visit_ID { get; set; }
        // primary key 

        public DateTime VisitDate { get; set; }
        // date time of the appointment
        [ForeignKey("Patient")]
        public int Patient_ID { get; set; }
        public virtual Patient Patient { get; set; }
        [ForeignKey("Location")]
        public int Location_ID { get; set; }
        public virtual Location Location { get; set; }
    }
}