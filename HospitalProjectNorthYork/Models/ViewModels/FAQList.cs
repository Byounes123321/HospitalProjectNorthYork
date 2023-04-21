using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProjectNorthYork.Models.ViewModels
{
    public class AppointmentList
    { 
        public IEnumerable<AppointmentDto> Appointments { get; set; }
    }
}