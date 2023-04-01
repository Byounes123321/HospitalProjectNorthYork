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
    public class VisitDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/VisitData/ListVisits
        [HttpGet]
        public IEnumerable<VisitDto> ListVisits()
        {
            List<Visit> Visits = db.Visits.ToList();
            List<VisitDto> VisitDtos = new List<VisitDto>();

            Visits.ForEach(a => VisitDtos.Add(new VisitDto()
            {
                Visit_ID = a.Visit_ID,
                VisitDate = a.VisitDate,
                Location_ID = a.Location_ID,
                Patient_ID = a.Patient_ID
            }));

            return VisitDtos;
        }

        // GET: api/VisitData/FindVisit/5
        [ResponseType(typeof(Visit))]
        [HttpGet]
        public IHttpActionResult FindVisit(int id)
        {
            Visit Visit = db.Visits.Find(id);
            VisitDto VisitDto = new VisitDto()
            {
                Visit_ID = Visit.Visit_ID,
                VisitDate = Visit.VisitDate,
                Location_ID = Visit.Location_ID,
                Patient_ID = Visit.Patient_ID

            };
            if (Visit == null)
            {
                return NotFound();
            }

            return Ok(VisitDto);
        }


        // POST: api/VisitData/UpdateVisit/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateVisit(int id, Visit visit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != visit.Visit_ID)
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
                if (!VisitExists(id))
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
        public IHttpActionResult AddVisit(Visit visit
            )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Visits.Add(visit);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = visit.Visit_ID }, visit);
        }

        // DELETE: api/FAQData/DeleteFAQ/5
        [ResponseType(typeof(FAQ))]
        [HttpPost]
        public IHttpActionResult DeleteFAQ(int id)
        {
            Visit visit = db.Visits.Find(id);
            if (visit == null)
            {
                return NotFound();
            }

            db.Visits.Remove(visit);
            db.SaveChanges();

            return Ok(visit);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VisitExists(int id)
        {
            return db.Visits.Count(e => e.Visit_ID == id) > 0;
        }
    }
}