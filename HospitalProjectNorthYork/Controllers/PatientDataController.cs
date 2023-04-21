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
    public class PatientDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all Patients in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Patients in the database, including their associated Patient.
        /// </returns>
        /// <example>
        /// GET: api/PatientData/ListPatients
        /// </example>
        [HttpGet]
        public IEnumerable<PatientDto> ListPatients()
        {
            List<Patient> Patients = db.Patients.ToList();
            List<PatientDto> PatientDtos = new List<PatientDto>();

            Patients.ForEach(p => PatientDtos.Add(new PatientDto()
            {
                Patient_ID = p.Patient_ID,
                PatientName = p.PatientName,
                PatientAdmittanceDate = p.PatientAdmittanceDate,
                PatientDateOfBirth = p.PatientDateOfBirth
            }));
                return PatientDtos;
        }

        /// <summary>
        /// Returns a list of all patients in the system by appointment id
        /// </summary>
        /// <param name="Doctor_Id">Primary key in the appointments table</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all patients in the database by specific appointment id, including their associated data
        /// </returns>
        /// <example>
        /// GET: api/PatientData/ListPatientsForAppointment/{Appointment_ID}
        /// </example>
        [HttpGet]
        [Route("api/PatientData/ListPatientsForAppointment/{Appointment_ID")]
        [ResponseType(typeof(PatientDto))]
        public IHttpActionResult ListPatientsForAppointment(int Appointment_ID)
        {
            List<Patient> Patients = db.Patients.Where(
             p => p.Appointments.Any(
                 b => b.Appointment_ID == Appointment_ID
             )).ToList();

            List<PatientDto> PatientDtos = new List<PatientDto>();


            Patients.ForEach(p => PatientDtos.Add(new PatientDto()
            {
                Patient_ID = p.Patient_ID,
                PatientName = p.PatientName,
                PatientAdmittanceDate = p.PatientAdmittanceDate,
                PatientDateOfBirth = p.PatientDateOfBirth,

            }));

            return Ok(PatientDtos);
    }

        /// <summary>
        /// Returns a list of all patients in the system by visit id
        /// </summary>
        /// <param name="Doctor_Id">Primary key in the visits table</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all patients in the database by specific visit id, including their associated data
        /// </returns>
        /// <example>
        /// GET: api/PatientData/ListPatientsForVisit/{visit_Id}
        /// </example>

        public IHttpActionResult ListPatientsForVisit(int Visit_ID)
        {
            List<Patient> Patients = db.Patients.Where(
             p => p.Visits.Any(
                 b => b.Visit_ID == Visit_ID
             )).ToList();

            List<PatientDto> PatientDtos = new List<PatientDto>();


            Patients.ForEach(p => PatientDtos.Add(new PatientDto()
            {
                Patient_ID = p.Patient_ID,
                PatientName = p.PatientName,
                PatientAdmittanceDate = p.PatientAdmittanceDate,
                PatientDateOfBirth = p.PatientDateOfBirth,

            }));

            return Ok(PatientDtos);
        }

        /// <summary>
        /// Returns all patients in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: An patient in the system matching up to the patient ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the patient</param>
        /// <example>
        /// GET: api/PatientData/FindPatient/5
        /// </example>
        [ResponseType(typeof(PatientDto))]
        [HttpGet]
        public IHttpActionResult FindPatient(int id)
        {
            Patient Patient = db.Patients.Find(id);
            PatientDto PatientDto = new PatientDto()
            {
                Patient_ID = Patient.Patient_ID,
                PatientName = Patient.PatientName,
                PatientAdmittanceDate = Patient.PatientAdmittanceDate,
                PatientDateOfBirth = Patient.PatientDateOfBirth
            };
            if (Patient == null)
            {
                return NotFound();
            }

            return Ok(PatientDto);
        }

        /// <summary>
        /// Updates a particular Patient in the system with POST Data input
        /// </summary>
        /// <param name="id">Represents the Patient ID primary key</param>
        /// <param name="Patient">JSON FORM DATA of a Patient</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// Post: api/PatientData/UpdatePatient/5
        /// FORM DATA: Patient JSON Object
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        [Authorize]
        public IHttpActionResult UpdatePatient(int id, Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != patient.Patient_ID)
            {
                return BadRequest();
            }

            db.Entry(patient).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
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
        /// Adds a Patient to the system
        /// </summary>
        /// <param name="Patient">JSON FORM DATA of a Patient</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Patient ID, Patient Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/PatientData/AddPatient
        /// FORM DATA: Patient JSON Object
        /// </example>
        [ResponseType(typeof(Patient))]
        [HttpPost]
        [Authorize]
        public IHttpActionResult AddPatient(Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Patients.Add(patient);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = patient.Patient_ID }, patient);
        }

        /// <summary>
        /// Deletes a Patient from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the Patient</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/PatientData/DeletePatient/5
        /// FORM DATA: (empty)
        /// </example>
        [ResponseType(typeof(Patient))]
        [HttpPost]
        [Authorize]
        public IHttpActionResult DeletePatient(int id)
        {
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return NotFound();
            }

            db.Patients.Remove(patient);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PatientExists(int id)
        {
            return db.Patients.Count(e => e.Patient_ID == id) > 0;
        }
    }
}