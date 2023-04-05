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
    public class FeedbacksDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Gathers list of all feedbacks
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all feedbacks from DB including the related information of patient and appointment
        /// </returns>
        /// <example>
        /// GET: api/FeedbacksData/ListFeedbacks
        /// </example>

        // GET: api/FeedbacksData/ListFeedbacks
        [HttpGet]
        public IEnumerable<FeedbacksDto> ListFeedbacks()
        {
            List<Feedbacks> Feedbacks = db.Feedbacks.ToList();
            List<FeedbacksDto> FeedbacksDto = new List<FeedbacksDto>();

            Feedbacks.ForEach(f => FeedbacksDto.Add(new FeedbacksDto()
            {
                Feedback_ID = f.Feedback_ID,
                FeedbackDesc = f.FeedbackDesc,
                Patient_ID = f.Patient_ID,
                PatientName = f.Patient.PatientName,
                Appointment_ID = f.Appointment_ID,
                AppointmentDesc = f.Appointment.AppointmentDesc,
                AppointmentDate = f.Appointment.AppointmentDate
            }));

            return FeedbacksDto;
        }

        /// <summary>
        /// Gathers information about feedback with the particular ID
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all information of the feedback including patient and appointment
        /// </returns>
        /// <param name="id">Feedback ID.</param>
        /// <example>
        /// GET: api/FeedbacksData/FindFeedback/5
        /// </example>

        // GET: api/FeedbacksData/FindFeedback/5
        [ResponseType(typeof(Feedbacks))]
        [HttpGet]
        public IHttpActionResult FindFeedback(int id)
        {
            Feedbacks feedback = db.Feedbacks.Find(id);
            FeedbacksDto feedbackdto = new FeedbacksDto()
            {
                Feedback_ID = feedback.Feedback_ID,
                FeedbackDesc = feedback.FeedbackDesc,
                Patient_ID = feedback.Patient_ID,
                PatientName = feedback.Patient.PatientName,
                Appointment_ID = feedback.Appointment_ID,
                AppointmentDesc = feedback.Appointment.AppointmentDesc,
                AppointmentDate = feedback.Appointment.AppointmentDate
            };
            if (feedback == null)
            {
                return NotFound();
            }

            return Ok(feedbackdto);
        }

        /// <summary>
        /// Update information about feedback with the particular ID
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: updated information of the feedback
        /// </returns>
        /// <param name="id">Feedback ID.</param>
        /// <param name="feedbacks">Feedback information.</param>
        /// <example>
        /// GET: api/FeedbacksData/UpdateFeedback/5
        /// </example>

        // POST: api/FeedbacksData/UpdateFeedback/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateFeedback(int id, Feedbacks feedbacks)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != feedbacks.Feedback_ID)
            {
                return BadRequest();
            }

            db.Entry(feedbacks).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedbacksExists(id))
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
        /// Add information of a new feedback
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: added feedback
        /// </returns>
        /// <param name="feedbacks">Feedback information.</param>
        /// <example>
        /// GET: api/FeedbacksData/AddFeedback
        /// </example>

        // POST: api/FeedbacksData/AddFeedback
        [ResponseType(typeof(Feedbacks))]
        [HttpPost]
        public IHttpActionResult AddFeedback(Feedbacks feedbacks)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Feedbacks.Add(feedbacks);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = feedbacks.Feedback_ID }, feedbacks);
        }
        /// <summary>
        /// Delete information about an existing feedback
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: deleted feedback
        /// </returns>
        /// <param name="id">Feedback ID.</param>
        /// <example>
        /// GET: api/FeedbacksData/DeleteFeedback/5
        /// </example>

        // DELETE: api/FeedbacksData/DeleteFeedback/5
        [ResponseType(typeof(Feedbacks))]
        [HttpPost]
        public IHttpActionResult DeleteFeedback(int id)
        {
            Feedbacks feedbacks = db.Feedbacks.Find(id);
            if (feedbacks == null)
            {
                return NotFound();
            }

            db.Feedbacks.Remove(feedbacks);
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

        private bool FeedbacksExists(int id)
        {
            return db.Feedbacks.Count(e => e.Feedback_ID == id) > 0;
        }
    }
}