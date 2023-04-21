using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProjectNorthYork.Models.ViewModels
{
    public class DepartmentList
    {
        public IEnumerable<DepartmentDto> Departments { get; set; }
    }
}