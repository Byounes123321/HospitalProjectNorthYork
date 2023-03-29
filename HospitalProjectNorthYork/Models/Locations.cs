using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HospitalProjectNorthYork.Models
{
    public class Location
    {
        [Key]
        public int Location_ID { get; set; }
        //Primary Key
        public string LocaitonName { get; set; }
        //Name of the location
        public string LocationDesc { get; set; }
        //Description of the location
        // A location can have multiple departments
        public ICollection<Department> Departments { get; set; }

    }
}