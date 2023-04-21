using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProjectNorthYork.Models.ViewModels
{
    public class AppointmentUpdate
    {   
        public AppointmentDto SelectedAppointment { get; set; }
        public IEnumerable<PatientDto> Patients { get; set; }   
        public IEnumerable<DoctorsDto> Doctors { get; set; }
        public IEnumerable<LocationDto> Locations { get; set; }

    }
}