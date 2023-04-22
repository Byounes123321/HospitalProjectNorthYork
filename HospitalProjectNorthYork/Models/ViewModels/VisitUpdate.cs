using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProjectNorthYork.Models.ViewModels
{
    public class VisitUpdate
    { 
        public IEnumerable<AppointmentDto> Appointments { get; set; }
        public VisitDto SelectedVisit { get; set; }
        public IEnumerable<PatientDto> Patients { get; set; }
        public IEnumerable<LocationDto> Locations { get; set; }
    }
}