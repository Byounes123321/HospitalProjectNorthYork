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
    public class DepartmentDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Returns a list of all Departments in the system
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Departments in the database, including their associated data
        /// </returns>
        /// <example>
        /// GET: api/DepartmentData/ListDepartments
        /// </example>
        [HttpGet]
        [ResponseType(typeof(DepartmentDto))]
        public IHttpActionResult ListDepartments()
        {
            List<Department> Departments = db.Departments.ToList();
            List<DepartmentDto> DepartmentDtos = new List<DepartmentDto>();

            Departments.ForEach(a => DepartmentDtos.Add(new DepartmentDto()
            {
                Department_ID = a.Department_ID,
                DepartmentName = a.DepartmentName,
                DepartmentDesc = a.DepartmentDesc,
            }));

            return Ok(DepartmentDtos);

        }
        
        /// <summary>
        /// Returns a list of all Departments in the system by location id
        /// </summary>
        /// <param name="Location_ID">Primary key in the location table</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Departments in the database by specific Location id, including their associated data
        /// </returns>
        /// <example>
        /// GET: api/DepartmentData/ListDepartmentsForLocation/{Location_ID}
        /// </example>
        [HttpGet]
        [Route("api/DepartmentData/ListDepartmentsForLocation/{Location_ID}")]
        [ResponseType(typeof(DepartmentDto))]
        public IHttpActionResult ListDepartmentsForLocation(int Location_ID)
        {

            List<Department> Departments = db.Departments.Where(
             a => a.Locations.Any(
                 k => k.Location_ID == Location_ID
             )).ToList();

            List<DepartmentDto> DepartmentDtos = new List<DepartmentDto>();


            Departments.ForEach(a => DepartmentDtos.Add(new DepartmentDto()
            {
                Department_ID = a.Department_ID,
                DepartmentName = a.DepartmentName,
                DepartmentDesc = a.DepartmentDesc
            }));

            return Ok(DepartmentDtos);
        }
        /// <summary>
        /// Returns a list of all Departments in the system by FAQ id
        /// </summary>
        /// <param name="faq_id">Primary key in the FAQ table</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Departments in the database by specific FAQ id, including their associated data
        /// </returns>
        /// <example>
        /// GET: api/DepartmentData/ListDepartmentsForFAQ/{FAQ_ID}
        /// </example>
        [HttpGet]
        [Route("api/DepartmentData/ListDepartmentsForFAQ/{FAQ_ID}")]
        [ResponseType(typeof(DepartmentDto))]
        public IHttpActionResult ListDepartmentsForFAQ(int Faq_ID)
        {

            List<Department> Departments = db.Departments.Where(
             a => a.FAQs.Any(
                 k => k.Faq_ID == Faq_ID
             )).ToList();

            List<DepartmentDto> DepartmentDtos = new List<DepartmentDto>();


            Departments.ForEach(a => DepartmentDtos.Add(new DepartmentDto()
            {
                Department_ID = a.Department_ID,
                DepartmentName = a.DepartmentName,
                DepartmentDesc = a.DepartmentDesc
            }));

            return Ok(DepartmentDtos);
        }

        ///<summary>
        ///Returns a specific Department called by its id
        /// </summary>
        /// <param name="Department_ID"> Primary key in the departments table</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: Department in the database that matching the Department_ID key
        /// </returns>
        /// <example>
        /// GET: api/DepartmentData/FindDepartment/{Appointment_ID}
        /// </example>
        [HttpGet]
        [Route("api/DepartmentData/FindDepartment/{Department_ID}")]
        [ResponseType(typeof(DepartmentDto))]
        public IHttpActionResult FindDepartment(int Department_ID)
        {
            Department Department = db.Departments.Find(Department_ID);
            DepartmentDto departmentDto = new DepartmentDto()
            {
                Department_ID = Department.Department_ID,
                DepartmentName =Department.DepartmentName,    
                DepartmentDesc= Department.DepartmentDesc,
            };

            return Ok(departmentDto);
        }
        /// <summary>
        /// Adds an Department to the system
        /// </summary>
        /// <param name="Department">JSON FORM DATA of an Department</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Department ID, Department Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/DepartmentData/AddDepartment
        /// FORM DATA: Department JSON Object
        /// </example>
        [ResponseType(typeof(Department))]
        [HttpPost]
        public IHttpActionResult AddDepartment(Department Department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Departments.Add(Department);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Department.Department_ID }, Department);
        }

        /// <summary>
        /// Updates a particular Department in the system with POST Data input
        /// </summary>
        /// <param name="Department_ID">Represents the Department ID primary key</param>
        /// <param name="Department">JSON FORM DATA of an Department</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/DepartmentData/UpdateDepartment/2
        /// FORM DATA: Department JSON Object
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateDepartment(int Department_ID, Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (Department_ID != department.Department_ID)
            {

                return BadRequest();
            }

            db.Entry(department).State = EntityState.Modified;


            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(Department_ID))
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
        /// Deletes an Department from the system by it's ID.
        /// </summary>
        /// <param name="Department_ID">The primary key of the Department</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/DepartmentData/DeleteDepartment/5
        /// FORM DATA: (empty)
        /// </example>
        [ResponseType(typeof(Department))]
        [Route("api/DepartmentData/DeleteDepartment/{Department_ID}")]
        [HttpPost]
        public IHttpActionResult DeleteDepartment(int Department_ID)
        {
            Department department = db.Departments.Find(Department_ID);
            if (department == null)
            {
                return NotFound();
            }
            
            db.Departments.Remove(department);
            db.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// returns if there is a department with the given id
        /// </summary>
        /// <returns>
        /// True or False
        /// </returns>
        private bool DepartmentExists(int Department_ID)
        {
            return db.Departments.Count(e => e.Department_ID == Department_ID) > 0;
        }

        /// <summary>
        /// Associates a particular department with a location animal
        /// </summary>
        /// <param name="Department_ID">The animal ID department key</param>
        /// <param name="Location_ID">The keeper ID location key</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST api/DepartmentData/AssociateDepartmentWithLocation/2/1
        /// </example>
        [HttpPost]
        [Route("api/DepartmentData/AssociateDepartmentWithLocation/{Department_ID}/{Location_ID}")]
        public IHttpActionResult AssociateDepartmentWithLocation(int Department_ID, int Location_ID)
        {

            Department SelectedDepartment = db.Departments.Include(a => a.Locations).Where(a => a.Department_ID == Department_ID).FirstOrDefault();
            Location SelectedLocation = db.Locations.Find(Location_ID);

            if (SelectedDepartment == null || SelectedLocation == null)
            {
                return NotFound();
            }

            SelectedDepartment.Locations.Add(SelectedLocation);
            db.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Associates a particular department with a particular FAQ association
        /// </summary>
        /// <param name="Department_ID"></param>
        /// <param name="FAQ_ID"></param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST api/DepartmentData/AssociateDepartmentWithFAQ/2/1
        /// </example>
        [HttpPost]
        [Route("api/DepartmentData/AssociateDepartmentWithFAQ/{Department_ID}/{FAQ_ID}")]
        public IHttpActionResult AssociateDepartmentWithFAQ(int Department_ID, int FAQ_ID)
        {

            Department SelectedDepartment = db.Departments.Include(a => a.FAQs).Where(a => a.Department_ID == Department_ID).FirstOrDefault();
            FAQ SelectedFAQ = db.FAQS.Find(FAQ_ID);

            if (SelectedDepartment == null || SelectedFAQ == null)
            {
                return NotFound();
            }

            SelectedDepartment.FAQs.Add(SelectedFAQ);
            db.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Removes an association between a particular department and a location animal
        /// </summary>
        /// <param name="department_ID">The department ID primary key</param>
        /// <param name="Location_ID">The location ID primary key</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST api/departmentData/UnAssociateDepartmentWithLocaiton/9/1
        /// </example>
        [HttpPost]
        [Route("api/departmentData/UnAssociateDepartmentWithLocaiton/{Department_Id}/{Location_ID}")]
        public IHttpActionResult UnAssociateDepartmentWithLocaiton(int Department_ID, int Location_ID)
        {

            Department SelectedDepartment = db.Departments.Include(a => a.Locations).Where(a => a.Department_ID == Department_ID).FirstOrDefault();
            Location SelectedLocation = db.Locations.Find(Location_ID);

            if (SelectedDepartment == null || SelectedLocation == null)
            {
                return NotFound();
            }


            SelectedDepartment.Locations.Remove(SelectedLocation);
            db.SaveChanges();

            return Ok();
        }
        /// <summary>
        /// Removes an association between a particular department and a FAQ 
        /// </summary>
        /// <param name="department_ID">The department ID primary key</param>
        /// <param name="FAQ_ID">The FAQ ID primary key</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST api/departmentData/UnAssociateDepartmentWithFAQ/9/1
        /// </example>
        [HttpPost]
        [Route("api/departmentData/UnAssociateDepartmentWithFAQ/{Department_Id}/{FAQ_ID}")]
        public IHttpActionResult UnAssociateDepartmentWithFAQ(int Department_ID, int FAQ_ID)
        {

            Department SelectedDepartment = db.Departments.Include(a => a.FAQs).Where(a => a.Department_ID == Department_ID).FirstOrDefault();
            FAQ SelectedFAQ = db.FAQS.Find(FAQ_ID);

            if (SelectedDepartment == null || SelectedFAQ == null)
            {
                return NotFound();
            }


            SelectedDepartment.FAQs.Remove(SelectedFAQ);
            db.SaveChanges();

            return Ok();
        }
    }
}