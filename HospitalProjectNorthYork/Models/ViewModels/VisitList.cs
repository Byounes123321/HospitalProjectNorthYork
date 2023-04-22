using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProjectNorthYork.Models.ViewModels
{
    public class VisitList
    { 
        public IEnumerable<VisitDto> Visits { get; set; }
    }
}