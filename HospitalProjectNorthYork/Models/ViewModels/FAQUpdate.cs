using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProjectNorthYork.Models.ViewModels
{
    public class FAQUpdate
    { 
        public FAQDto FAQs { get; set; }
        public IEnumerable<DepartmentDto> department { get; set; }
    }
}