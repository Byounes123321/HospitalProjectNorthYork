using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace HospitalProjectNorthYork.Models
{
    public class Department
    {
        [Key]
        public int Department_ID { get; set; }
        //Primary Key
        public string DepartmentName { get; set; }
        //Name of the department
        public string DepartmentDesc { get; set; }
        //Description of the department
        // A department can be at multiple locations
        public ICollection<Location> Locations { get; set; }
        // A department can have multiple FAQs
        public ICollection<FAQ> FAQs { get; set; }
    }
    public class DepartmentDto
    {
        public int Department_ID { get; set; }
        public string DepartmentName { get; set;}
        public string DepartmentDesc { get; set; }

    }
}