using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProjectNorthYork.Models.ViewModels
{
    public class LocationList
    {
        public IEnumerable<LocationDto> Locations { get; set; }
    }
}