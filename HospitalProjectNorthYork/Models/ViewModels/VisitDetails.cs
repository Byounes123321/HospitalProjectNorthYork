﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProjectNorthYork.Models.ViewModels
{
    public class AppointmentDetails
    {
        public AppointmentDto SelectedAppointment { get; set; }
        public PatientDto Patient { get; set; }
        public DoctorsDto Docter { get; set; }
        public LocationDto Location { get; set; }
    }
}