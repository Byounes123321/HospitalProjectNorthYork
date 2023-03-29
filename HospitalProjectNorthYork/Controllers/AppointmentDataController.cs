using System;
using System.IO;
using System.Web;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using HospitalProjectNorthYork.Models;
using System.Diagnostics;

namespace HospitalProjectNorthYork.Controllers
{
    public class AppointmentDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Returns a list of all Appointments in the system
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Appointments in the database, including their associated data
        /// </returns>
        /// <example>
        /// GET: api/AppointmentData/ListAppointments
        /// </example>
        [HttpGet]
        [ResponseType(typeof(AppointmentDto))]
        public IHttpActionResult ListAppointments()
        {
            List<Appointment> Appointments = db.Appointments.ToList();
            List<AppointmentDto> AppointmentDtos = new List<AppointmentDto>();

            Appointments.ForEach(a => AppointmentDtos.Add(new AppointmentDto()
            {
                Appointment_ID = a.Appointment_ID,
                AppointmentDesc = a.AppointmentDesc,
                AppointmentDate = a.AppointmentDate,
                Patient_ID = a.Patient_ID,
                Doctor_ID = a.Doctor_ID,
                Location_ID = a.Location_ID
            }));

            return Ok(AppointmentDtos);

        }



    }
}
