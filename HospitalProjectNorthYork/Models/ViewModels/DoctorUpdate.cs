using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProjectNorthYork.Models.ViewModels
{
    public class DoctorUpdate
    {
        // this view model is a class that stores information that we need to present to /Doctor/Update/{}

        // existing doctor information
        public DoctorsDto SelectedDoctor { get; set; }
        // all departments to choose from
        public IEnumerable<DepartmentDto> DepartmentOptions { get; set; }
    }
}