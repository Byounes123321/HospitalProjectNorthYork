using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProjectNorthYork.Models.ViewModels
{
    public class DepartmentDetails
    {
        public DepartmentDto SelectedDepartment { get; set; }
        public IEnumerable<FAQDto> FAQs { get;set; }
        public IEnumerable<LocationDto> Location { get; set; }       

    }
}