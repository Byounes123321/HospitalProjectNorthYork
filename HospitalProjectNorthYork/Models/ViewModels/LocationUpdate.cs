using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProjectNorthYork.Models.ViewModels
{
    public class LocationUpdate
    {
        //This viewmodel is a class which stores information that we need to present to /Location/Update/{}

        public LocationDto SelectedLocation { get; set; }

    }
}