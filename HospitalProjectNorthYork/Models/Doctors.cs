using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HospitalProjectNorthYork.Models
{
    public class Doctors
    {
        [Key]
        public int Doctor_ID { get; set; }
        //primary key for doctors table
        public string DoctorName { get; set; }
        //Doctor name
        public string DoctorBio { get; set; }
        //Doctor bio
/*
 * TODO: Add foreign keys for the following tables:
 * Department_ID
 */
}
}