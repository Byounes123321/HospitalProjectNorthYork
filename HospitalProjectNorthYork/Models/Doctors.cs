using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [ForeignKey("Department")]
        public int Department_ID { get; set; }
        public virtual Department Department { get; set; }
        /*
         * TODO: Add foreign keys for the following tables:
         * Department_ID
         */
    }
}