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
                DepartmentDesc = a.DepartmentDesc
            }));

            return Ok(DepartmentDtos);

        }
        /*
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
                a => a.Location_ID == Location_ID
                ).ToList();
            // I don't know how to get the data from the bridge table
     

            List<DepartmentDto> DepartmentDtos = new List<DepartmentDto>();
        

            Departments.ForEach(a => DepartmentDtos.Add(new DepartmentDto()
            {
                Department_ID = a.Department_ID,
                DepartmentName = a.DepartmentName,
                DepartmentDesc = a.DepartmentDesc
            }));

            return Ok(DepartmentDtos);
        }
        */

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
    }
}