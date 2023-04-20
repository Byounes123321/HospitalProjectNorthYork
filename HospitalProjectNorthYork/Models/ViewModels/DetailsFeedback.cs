using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProjectNorthYork.Models.ViewModels
{
    public class DetailsFeedback
    {
        public FeedbacksDto SelectedFeedback { get; set; }
        public IEnumerable<PatientDto> PatientOptions { get; set; }

        public IEnumerable<AppointmentDto> AppointmentOptions { get; set; }
    }
}