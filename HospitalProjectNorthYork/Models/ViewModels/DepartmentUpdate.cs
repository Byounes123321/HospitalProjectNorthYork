using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProjectNorthYork.Models.ViewModels
{
    public class DepartmentUpdate
    {
        public DepartmentDto SelectedDepartment { get; set; }
        public IEnumerable<FAQDto> FAQ { get; set; }
        public IEnumerable<LocationDto> location { get; set; }
    }
}