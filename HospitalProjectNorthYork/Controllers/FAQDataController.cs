using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using HospitalProjectNorthYork.Migrations;
using HospitalProjectNorthYork.Models;

namespace HospitalProjectNorthYork.Controllers
{
    public class FAQDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/FAQData/ListFAQS
        [HttpGet]
        [ResponseType(typeof(FAQDto))]

        public IHttpActionResult ListFAQS()
        {
            List<FAQ> FAQS= db.FAQS.ToList();
            List<FAQDto> FAQDtos = new List<FAQDto>();

            FAQS.ForEach(a => FAQDtos.Add(new FAQDto()
            {
                Faq_ID = a.Faq_ID,
                Ques = a.Ques,
                Answer = a.Answer
            }));

            return Ok(FAQDtos); ;

        }

        [HttpGet]
        [Route("api/DepartmentData/ListFAQSForDepartment/{Department_ID}")]
        [ResponseType(typeof(FAQDto))]
        public IHttpActionResult ListFAQSForDepartment(int Department_ID)
        {

            List<FAQ> FAQS = db.FAQS.Where(
             a => a.Departments.Any(
                 k => k.Department_ID == Department_ID
             )).ToList();

            List<FAQDto> FAQDtos = new List<FAQDto>();


            FAQS.ForEach(a => FAQDtos.Add(new FAQDto()
            {
                Faq_ID = a.Faq_ID,
               Ques = a.Ques,
                Answer = a.Answer
            }));

            return Ok(FAQDtos);
        }

        [HttpGet]
        [Route("api/DepartmentData/ListFAQSNotForDepartment/{Department_ID}")]
        [ResponseType(typeof(FAQDto))]
        public IHttpActionResult ListFAQSNotForDepartment(int Department_ID)
        {

            List<FAQ> FAQS = db.FAQS.Where(
             a => ! a.Departments.Any(
                 k => k.Department_ID == Department_ID
             )).ToList();

            List<FAQDto> FAQDtos = new List<FAQDto>();


            FAQS.ForEach(a => FAQDtos.Add(new FAQDto()
            {
                Faq_ID = a.Faq_ID,
                Ques = a.Ques,
                Answer = a.Answer
            }));

            return Ok(FAQDtos);
        }

        // GET: api/FAQData/FindFAQ/5
        [ResponseType(typeof(FAQ))]
        [HttpGet]
        public IHttpActionResult FindFAQ(int id)
        {
            FAQ FAQ = db.FAQS.Find(id);
            FAQDto FAQDto = new FAQDto()
            {
                Faq_ID = FAQ.Faq_ID,
                Ques = FAQ.Ques,
                Answer = FAQ.Answer

            };
            if (FAQ == null)
            {
                return NotFound();
            }

            return Ok(FAQDto);
        }


        // POST: api/FAQData/UpdateFAQ/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateFAQ(int Faq_ID, FAQ fAQ)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (Faq_ID != fAQ.Faq_ID)
            {
                return BadRequest();
            }

            db.Entry(fAQ).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FAQExists(Faq_ID))
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
        [ResponseType(typeof(FAQ))]
        [HttpPost]
        public IHttpActionResult AddFAQ(FAQ FAQ)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FAQS.Add(FAQ);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = FAQ.Faq_ID }, FAQ);
        }

        // DELETE: api/FAQData/DeleteFAQ/5
        [ResponseType(typeof(FAQ))]
        [Route("api/FAQData/DeleteFAQ/{Faq_ID}")]
        [HttpPost]
        public IHttpActionResult DeleteFAQ(int Faq_ID)
        {
            FAQ fAQ = db.FAQS.Find(Faq_ID);
            if (fAQ == null)
            {
                return NotFound();
            }

            db.FAQS.Remove(fAQ);
            db.SaveChanges();

            return Ok();
        }

    
        private bool FAQExists(int Faq_ID)
        {
            return db.FAQS.Count(e => e.Faq_ID == Faq_ID) > 0;
        }
    }
}