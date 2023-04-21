using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProjectNorthYork.Models.ViewModels
{
    public class NewDepartment
    {
        public IEnumerable<FAQDto> fAQ { get; set; }
        public IEnumerable<LocationDto> Location { get; set; }

    }
}