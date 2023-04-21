﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProjectNorthYork.Models.ViewModels
{
    public class NewVisit
    {
        public IEnumerable<PatientDto> Patient { get; set; }
        public IEnumerable<LocationDto> Location { get; set; }

    }
}