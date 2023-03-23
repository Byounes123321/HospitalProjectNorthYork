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
    }
}