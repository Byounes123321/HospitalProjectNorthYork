using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProjectNorthYork.Models.ViewModels
{
    public class FAQDetails
    {
        public FAQDto SelectedFAQ { get; set; }
        public PatientDto Patient { get; set; }
        public DoctorsDto Docter { get; set; }
        public LocationDto Location { get; set; }
        public IEnumerable<DepartmentDto> RelatedDepartments { get; set; }
        public IEnumerable<DepartmentDto> UnrelatedDepartments { get; set; }
    }
}