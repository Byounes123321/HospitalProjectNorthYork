using HospitalProjectNorthYork.Models;
using HospitalProjectNorthYork.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace HospitalProjectNorthYork.Controllers
{
    public class FeedbackController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static FeedbackController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44396/api/");
        }
        // GET: Feedback
        public ActionResult List()
        {
            // communicate with feedback api to retrieve a list of feedbacks
            // curl https://localhost:44396/api/FeedbacksData/ListFeedbacks

            string url = "FeedbacksData/ListFeedbacks";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<FeedbacksDto> feedbacks = response.Content.ReadAsAsync<IEnumerable<FeedbacksDto>>().Result;

            return View(feedbacks);
        }

        // GET: Feedback/Details/5
        public ActionResult Details(int id)
        {
            // communicate with feedback api to retrieve details of the feedback
            // curl https://localhost:44396/api/FeedbacksData/FindFeedback/{id}

            string url = "FeedbacksData/FindFeedback/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            FeedbacksDto feedback = response.Content.ReadAsAsync<FeedbacksDto>().Result;

            return View(feedback);
        }

        // GET: Feedback/New
        public ActionResult New()
        {
            // information about all patients in the system
            // GET/PatientData/ListPatients

            string url = "PatientData/ListPatients";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<PatientDto> PatientOptions = response.Content.ReadAsAsync<IEnumerable<PatientDto>>().Result;
            
            return View(PatientOptions);
        }

        // GET: Feedback/AppointmentsList
        public ActionResult AppointmentsList(Patient patient)
        {
            // information about all appointments for particular patient ID in the system
            // GET/AppointmentData/ListAppointmentsForPatient/{patientId}

            string url = "AppointmentData/ListAppointmentsForPatient/" + patient.Patient_ID;
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<AppointmentDto> AppointmentOptions = response.Content.ReadAsAsync<IEnumerable<AppointmentDto>>().Result;

            return View(AppointmentOptions);
        }

        // POST: Feedback/Create
        [HttpPost]
        public ActionResult Create(Feedbacks feedback)
        {
            // communicate with feedback api to add details of the new feedback
            // curl -H "Content-Type:application/json" -d @doctor.json https://localhost:44396/api/FeedbacksData/AddFeedback
            string url = "FeedbacksData/AddFeedback";

            // converting payload to json to send to api

            string jsonpayload = jss.Serialize(feedback);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Feedback/Edit/5
        public ActionResult Edit(int id)
        {
            // existing feedback information
            string url = "FeedbacksData/FindFeedback/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            FeedbacksDto feedback = response.Content.ReadAsAsync<FeedbacksDto>().Result;

            return View(feedback);
        }

        // POST: Feedback/Update/5
        [HttpPost]
        public ActionResult Update(int id, Feedbacks feedback)
        {
            string url = "FeedbacksData/UpdateFeedback/" + id;

            // converting payload to json to send to api

            string jsonpayload = jss.Serialize(feedback);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Feedback/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "FeedbacksData/FindFeedback/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            FeedbacksDto feedback = response.Content.ReadAsAsync<FeedbacksDto>().Result;
            return View(feedback);
        }

        // POST: Feedback/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "FeedbacksData/DeleteFeedback/" + id;

            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}
