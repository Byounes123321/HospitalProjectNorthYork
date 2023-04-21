using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProjectNorthYork.Models.ViewModels
{
    public class DepartmentDetails
    {
        public DepartmentDto SelectedDepartment { get; set; }
        public IEnumerable<FAQDto> RelatedFAQs { get;set; }
        public IEnumerable<FAQDto> UnrelatedFAQs { get; set; }
        //public IEnumerable<LocationDto> RelatedLocations { get; set; }
        //public IEnumerable<LocationDto> unrelatedLocations { get; set; }

    }
}