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

        /// <summary>
        /// Returns a list of all Appointments in the system by doctor id
        /// </summary>
        /// <param name="Doctor_Id">Primary key in the doctors table</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Appointments in the database by specific doctor id, including their associated data
        /// </returns>
        /// <example>
        /// GET: api/AppointmentData/ListAppointmentsForDoctor/{Doctor_Id}
        /// </example>
        [HttpGet]
        [Route("api/AppointmentData/ListAppointmentsForDoctor/{Doctor_Id}")]
        [ResponseType(typeof(AppointmentDto))]
        public IHttpActionResult ListAppointmentsForDoctor(int Doctor_Id)
        {
            List<Appointment> Appointments = db.Appointments.Where(a => a.Doctor_ID == Doctor_Id).ToList();
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
        /// <summary>
        /// Returns a list of all Appointments in the system by patient id
        /// </summary>
        /// <param name="Patient_ID">Primary key in the patient table</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Appointments in the database by specific patient id, including their associated data
        /// </returns>
        /// <example>
        /// GET: api/AppointmentData/ListAppointmentsForPatient/{Patient_ID}
        /// </example>
        
        [HttpGet]
        [Route("api/AppointmentData/ListAppointmentsForPatient/{Patient_ID}")]
        [ResponseType(typeof(AppointmentDto))]
        public IHttpActionResult ListAppointmentsForPatient(int Patient_ID)
        {
            List<Appointment> Appointments = db.Appointments.Where(a => a.Patient_ID == Patient_ID).ToList();
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
        ///<summary>
        ///Returns a specific Appointment called by its id
        /// </summary>
        /// <param name="Appointment_ID"> Primary key in the appointments table</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: Appointment in the database that matching the Appointment_ID key
        /// </returns>
        /// <example>
        /// GET: api/AppointmentData/FindAppointment/{Appointment_ID}
        /// </example>
        [HttpGet]
        [Route("api/AppointmentData/FindAppointment/{Appointment_ID}")]
        [ResponseType(typeof(AppointmentDto))]
        public IHttpActionResult FindAppointment(int Appointment_ID)
        {
            List<Appointment> Appointments = db.Appointments.Where(a => a.Appointment_ID == Appointment_ID).ToList();
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
        /// <summary>
        /// Updates a particular appointment in the system with POST Data input
        /// </summary>
        /// <param name="Appointment_ID">Represents the Appointment ID primary key</param>
        /// <param name="Appointment">JSON FORM DATA of an appointment</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/AppointmentData/UpdateAppointment/5
        /// FORM DATA: Appointment JSON Object
        [ResponseType(typeof(void))]
        [Route("api/AppointmentData/UpdateAppointment/{Appointment_ID}")]
        [HttpPost]
        public IHttpActionResult UpdateAppointment(int Appointment_ID, Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (Appointment_ID != appointment.Appointment_ID)
            {

                return BadRequest();
            }

            db.Entry(appointment).State = EntityState.Modified;
 

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(Appointment_ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }
        /// <summary>
        /// Adds an Appointment to the system
        /// </summary>
        /// <param name="Appointment">JSON FORM DATA of an Appointment</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Appointment ID, Appointment Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/AppointmentData/AddAppointment
        /// FORM DATA: Appointment JSON Object
        /// </example>
        [ResponseType(typeof(Appointment))]
        [HttpPost]
        public IHttpActionResult AddAppointment(Appointment Appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Appointments.Add(Appointment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Appointment.Appointment_ID}, Appointment);
        }
        /// <summary>
        /// Deletes an Appointment from the system by it's ID.
        /// </summary>
        /// <param name="Appointment_ID">The primary key of the Appointment</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/AppointmentData/DeleteAppointment/5
        /// FORM DATA: (empty)
        /// </example>
        [ResponseType(typeof(Appointment))]
        [Route("api/AppointmentData/DeleteAppointment/{Appointment_ID}")]
        [HttpPost]
        public IHttpActionResult DeleteAppointment(int Appointment_ID)
        {
            Appointment Appointment = db.Appointments.Find(Appointment_ID);
            if (Appointment == null)
            {
                return NotFound();
            }

            db.Appointments.Remove(Appointment);
            db.SaveChanges();

            return Ok();
        }


        /// <summary>
        /// Check if there is an appointment in the Database With the respective id
        /// </summary>
        /// <param name="Appointment_ID"></param>
        /// <returns>
        /// True or False
        /// </returns>
        private bool AppointmentExists(int Appointment_ID)
        {
            return db.Appointments.Count(e => e.Appointment_ID == Appointment_ID) > 0;
        }
    }
}