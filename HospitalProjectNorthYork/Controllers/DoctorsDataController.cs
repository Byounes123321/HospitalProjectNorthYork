using System;
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

namespace HospitalProjectNorthYork.Controllers
{
    public class DoctorsDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Gathers list of all doctos
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all doctors from DB including the department they belong to
        /// </returns>
        /// <example>
        /// GET: api/DoctorsData/ListDoctors
        /// </example>

        // GET: api/DoctorsData/ListDoctors
        [HttpGet]
        public IEnumerable<DoctorsDto> ListDoctors()
        {
            List<Doctors> Doctors = db.Doctors.ToList();
            List<DoctorsDto> DoctorsDto = new List<DoctorsDto>();

            Doctors.ForEach(f => DoctorsDto.Add(new DoctorsDto()
            {
                Doctor_ID = f.Doctor_ID,
                DoctorName = f.DoctorName,
                DoctorBio = f.DoctorBio,
                Department_ID = f.Department_ID,
                DepartmentName = f.Department.DepartmentName,
                DepartmentDesc = f.Department.DepartmentDesc
            }));

            return DoctorsDto;
        }

        /// <summary>
        /// Gathers information about doctor with the particular ID
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all information of the doctor including the department they belong to
        /// </returns>
        /// <param name="id">Doctor ID.</param>
        /// <example>
        /// GET: api/DoctorsData/FindDoctor/5
        /// </example>

        // GET: api/DoctorsData/FindDoctor/5
        [ResponseType(typeof(Doctors))]
        [HttpGet]
        public IHttpActionResult FindDoctor(int id)
        {
            Doctors doctor = db.Doctors.Find(id);
            DoctorsDto doctordto = new DoctorsDto()
            {
                Doctor_ID = doctor.Doctor_ID,
                DoctorName = doctor.DoctorName,
                DoctorBio = doctor.DoctorBio,
                Department_ID = doctor.Department_ID,
                DepartmentName = doctor.Department.DepartmentName,
                DepartmentDesc = doctor.Department.DepartmentDesc
            };
            if (doctor == null)
            {
                return NotFound();
            }

            return Ok(doctordto);
        }

        /// <summary>
        /// Update information about doctor with the particular ID
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: updated information of the doctor
        /// </returns>
        /// <param name="id">Doctor ID.</param>
        /// <param name="doctors">Doctor information.</param>
        /// <example>
        /// GET: api/DoctorsData/UpdateDoctor/5
        /// </example>
        
        // POST: api/DoctorsData/UpdateDoctor/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateDoctor(int id, Doctors doctors)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != doctors.Doctor_ID)
            {
                return BadRequest();
            }

            db.Entry(doctors).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorsExists(id))
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
        /// Add information of a new doctor
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: added doctor
        /// </returns>
        /// <param name="doctors">Doctor information.</param>
        /// <example>
        /// GET: api/DoctorsData/AddDoctor
        /// </example>

        // POST: api/DoctorsData/AddDoctor
        [ResponseType(typeof(Doctors))]
        [HttpPost]  
        public IHttpActionResult AddDoctor(Doctors doctors)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Doctors.Add(doctors);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = doctors.Doctor_ID }, doctors);
        }

        /// <summary>
        /// Delete information about an existing doctor
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: deleted doctor
        /// </returns>
        /// <param name="id">Doctor ID.</param>
        /// <example>
        /// GET: api/DoctorsData/DeleteDoctor/5
        /// </example>

        // DELETE: api/DoctorsData/DeleteDoctor/5
        [ResponseType(typeof(Doctors))]
        [HttpPost]  
        public IHttpActionResult DeleteDoctor(int id)
        {
            Doctors doctors = db.Doctors.Find(id);
            if (doctors == null)
            {
                return NotFound();
            }

            db.Doctors.Remove(doctors);
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

        private bool DoctorsExists(int id)
        {
            return db.Doctors.Count(e => e.Doctor_ID == id) > 0;
        }
    }
}