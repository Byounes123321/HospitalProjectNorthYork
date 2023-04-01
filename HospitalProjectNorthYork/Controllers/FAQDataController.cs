﻿using System;
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

        // GET: api/FAQData/ListFAQs
        [HttpGet]
        public IEnumerable<FAQDto> ListFAQS()
        {
            List<FAQ> FAQS= db.FAQS.ToList();
            List<FAQDto> FAQDtos = new List<FAQDto>();

            FAQS.ForEach(a => FAQDtos.Add(new FAQDto()
            {
                Faq_ID = a.Faq_ID,
                Ques = a.Ques,
                Answer = a.Answer
            }));

            return FAQDtos;

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
        public IHttpActionResult UpdateFAQ(int id, FAQ fAQ)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fAQ.Faq_ID)
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
                if (!FAQExists(id))
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
        public IHttpActionResult AddFAQ(FAQ fAQ)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FAQS.Add(fAQ);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = fAQ.Faq_ID }, fAQ);
        }

        // DELETE: api/FAQData/DeleteFAQ/5
        [ResponseType(typeof(FAQ))]
        [HttpPost]
        public IHttpActionResult DeleteFAQ(int id)
        {
            FAQ fAQ = db.FAQS.Find(id);
            if (fAQ == null)
            {
                return NotFound();
            }

            db.FAQS.Remove(fAQ);
            db.SaveChanges();

            return Ok(fAQ);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FAQExists(int id)
        {
            return db.FAQS.Count(e => e.Faq_ID == id) > 0;
        }
    }
}