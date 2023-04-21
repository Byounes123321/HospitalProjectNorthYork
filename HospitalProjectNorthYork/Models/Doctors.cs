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
        //primary key for doctors table
        public int Doctor_ID { get; set; }
        //Doctor name
        public string DoctorName { get; set; }
        //Doctor bio
        public string DoctorBio { get; set; }
        //Department that a doctor belongs to
        [ForeignKey("Department")]
        public int Department_ID { get; set; }
        public virtual Department Department { get; set; }
        
    }

    public class DoctorsDto
    {
        public int Doctor_ID { get; set; }
        public string DoctorName { get; set; }
        public string DoctorBio { get; set;}
        public int Department_ID { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentDesc { get; set; }
    }
}