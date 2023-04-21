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
using System.Diagnostics;

namespace HospitalProjectNorthYork.Controllers
{
    public class LocationDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
         /// <summary>
        /// Returns a list of all Locations in the system
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Locations in the database, including their associated data
        /// </returns>
        /// <example>
        /// GET:  GET: api/LocationData/ListLocations
        /// </example>
        [HttpGet]
        public IEnumerable<LocationDto> ListLocations()
        {
            List<Location> Locations = db.Locations.ToList();
            List<LocationDto> LocationDtos = new List<LocationDto>();

            Locations.ForEach(l => LocationDtos.Add(new LocationDto()
            {
                Location_ID = l.Location_ID,
                LocaitonName = l.LocaitonName,
                LocationDesc = l.LocationDesc
            }));
            return LocationDtos;
        }


        /// <summary>
        /// Returns a list of all Locations in the system by department id
        /// </summary>
        /// <param name="Department_ID">Primary key in the location table</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Departments in the database by specific Location id, including their associated data
        /// </returns>
        /// <example>
        /// GET: api/LocationData/ListLocationForDepartment/{Department_ID}
        /// </example>
        [HttpGet]
        [Route("api/LocationData/ListLocationForDepartment/{Department_ID}")]
        [ResponseType(typeof(LocationDto))]
        public IHttpActionResult ListLocationsForDepartment(int Department_ID)
        {
            List<Location> Locations = db.Locations.Where(
                l => l.Departments.Any(
                    d => d.Department_ID == Department_ID
                    )).ToList();

            List<LocationDto> LocationDtos = new List<LocationDto>();


            Locations.ForEach(l => LocationDtos.Add(new LocationDto()
            {
                Location_ID = l.Location_ID,
                LocaitonName = l.LocaitonName,
                LocationDesc = l.LocationDesc
            }));

            return Ok(LocationDtos);
        }

        /// <summary>
        /// Returns all locations in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: An location in the system matching up to the location ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the location</param>
        /// <example>
        /// GET: api/LocationData/FindLocation/5
        /// </example>
        [ResponseType(typeof(LocationDto))]
        [HttpGet]
        public IHttpActionResult FindLocation(int id)
        {
            Location Location = db.Locations.Find(id);
            LocationDto LocationDto = new LocationDto()
            {
                Location_ID = Location.Location_ID,
                LocaitonName = Location.LocaitonName,
                LocationDesc = Location.LocationDesc
            };
            if(Location == null)
            {
                return NotFound();
            }
            return Ok(LocationDto);
        }

         /// <summary>
        /// Updates a particular Location in the system with POST Data input
        /// </summary>
        /// <param name="id">Represents the Location ID primary key</param>
        /// <param name="Location">JSON FORM DATA of a Location</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/LocationData/UpdateLocation/5
        /// FORM DATA: Location JSON Object
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        [Authorize]
        public IHttpActionResult UpdateLocation(int id, Location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != location.Location_ID)
            {
                return BadRequest();
            }

            db.Entry(location).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(id))
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
        /// Adds a Location to the system
        /// </summary>
        /// <param name="Location">JSON FORM DATA of a Location</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Location ID, Location Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/LocationData/AddLocation
        /// FORM DATA: Location JSON Object
        /// </example>
        [ResponseType(typeof(Location))]
        [HttpPost]
        [Authorize]
        public IHttpActionResult AddLocation(Location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Locations.Add(location);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = location.Location_ID }, location);
        }

           /// <summary>
        /// Deletes a Location from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the Location</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/LocationData/DeleteLocation/5
        /// FORM DATA: (empty)
        /// </example>
        [ResponseType(typeof(Location))]
        [HttpPost]
        [Authorize]
        public IHttpActionResult DeleteLocation(int id)
        {
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return NotFound();
            }

            db.Locations.Remove(location);
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

        private bool LocationExists(int id)
        {
            return db.Locations.Count(e => e.Location_ID == id) > 0;
        }
    }
}