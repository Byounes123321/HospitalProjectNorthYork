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
using HospitalProjectNorthYork.Migrations;
using HospitalProjectNorthYork.Models;

namespace HospitalProjectNorthYork.Controllers
{
    public class VisitDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/VisitData/ListVisits
        [HttpGet]
        [ResponseType(typeof(VisitDto))]

        public IHttpActionResult ListVisits()
        {
            List<Visit> Visits = db.Visits.ToList();
            List<VisitDto> VisitDtos = new List<VisitDto>();

            Visits.ForEach(a => VisitDtos.Add(new VisitDto()
            {
                Visit_ID = a.Visit_ID,
                VisitDate = a.VisitDate,
                Location_ID = a.Location_ID,
                
                Patient_ID = a.Patient_ID,
                
            }));

            return Ok(VisitDtos);
        }

        [HttpGet]
        [Route("api/VisitData/ListVisitsForLocation/{Location_Id}")]
        [ResponseType(typeof(VisitDto))]
        public IHttpActionResult ListVisitsForLocation(int Location_Id)
        {
            List<Visit> Visits = db.Visits.Where(a => a.Location_ID == Location_Id).ToList();
            List<VisitDto> VisitDtos = new List<VisitDto>();

            Visits.ForEach(a => VisitDtos.Add(new VisitDto()
            {
                Visit_ID = a.Visit_ID,
                VisitDate = a.VisitDate,
                Location_ID = a.Location_ID,
               
                Patient_ID = a.Patient_ID,
               
            }));

            return Ok(VisitDtos);
        }


        [HttpGet]
        [Route("api/VisitData/ListVisitsForPatient/{Patient_Id}")]
        [ResponseType(typeof(VisitDto))]
        public IHttpActionResult ListVisitsForPatient(int Patient_Id)
        {
            List<Visit> Visits = db.Visits.Where(a => a.Location_ID == Patient_Id).ToList();
            List<VisitDto> VisitDtos = new List<VisitDto>();

            Visits.ForEach(a => VisitDtos.Add(new VisitDto()
            {
                Visit_ID = a.Visit_ID,
                VisitDate = a.VisitDate,
                Location_ID = a.Location_ID,
                Patient_ID = a.Patient_ID,
                
            }));

            return Ok(VisitDtos);
        }

        // GET: api/VisitData/FindVisit/5
        [ResponseType(typeof(Visit))]
        [Route("api/VisitData/FindVisit/{Visit_ID}")]
        [HttpGet]
        public IHttpActionResult FindVisit(int Visit_ID)
        {
            List<Visit> Visits = db.Visits.Where(a => a.Visit_ID == Visit_ID).ToList();
            List<VisitDto> VisitDtos = new List<VisitDto>();

            Visits.ForEach(a => VisitDtos.Add(new VisitDto()
            {
                Visit_ID = a.Visit_ID,
                VisitDate = a.VisitDate,
                Location_ID = a.Location_ID,
                Patient_ID = a.Patient_ID,
               

            }));
           

            return Ok(VisitDtos);
        }


        // POST: api/VisitData/UpdateVisit/5
        [ResponseType(typeof(void))]
        [Route("api/VisitData/Updatevisit/{Visit_ID}")]
        [HttpPost]
        public IHttpActionResult UpdateVisit(int Visit_ID, Visit visit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (Visit_ID != visit.Visit_ID)
            {
                return BadRequest();
            }

            db.Entry(visit).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitExists(Visit_ID))
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



        // POST: api/FAQData/AddFAQ
        [ResponseType(typeof(Visit))]
        [HttpPost]
        public IHttpActionResult AddVisit(Visit Visit )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Visits.Add(Visit);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Visit.Visit_ID }, Visit);
        }

        // DELETE: api/FAQData/DeleteVisit/5
        [ResponseType(typeof(Visit))]
        [Route("api/VisitData/DeleteVisit/{Visit_ID}")]
        [HttpPost]
        public IHttpActionResult DeleteVisit(int Visit_ID)
        {
            Visit Visit = db.Visits.Find(Visit_ID);
            if (Visit == null)
            {
                return NotFound();
            }

            db.Visits.Remove(Visit);
            db.SaveChanges();

            return Ok();
        }

      

        private bool VisitExists(int Visit_ID)
        {
            return db.Visits.Count(e => e.Visit_ID == Visit_ID) > 0;
        }
    }
}