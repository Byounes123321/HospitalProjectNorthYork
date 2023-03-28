using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    }
}