using HospitalProjectNorthYork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using HospitalProjectNorthYork.Models.ViewModels;

namespace HospitalProjectNorthYork.Controllers
{
    public class PatientController : Controller
    {
        private static readonly HttpClient client;
        JavaScriptSerializer jss = new JavaScriptSerializer();

        static PatientController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44396/api/patientdata/");
        }

        // GET: Patient/List
        public ActionResult List()
        {
            //objective communicate with our patient data api to retrieve a list of patients
            //curl https://localhost:44396/api/patientdata/listpatients

            string url = "listpatients";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<Patient> patients = response.Content.ReadAsAsync<IEnumerable<Patient>>().Result;

            return View(patients);
        }

        // GET: Patient/Details/5
        public ActionResult Details(int id)
        {
            //objective communicate with our patient data api to retrieve one patient
            //curl https://localhost:44396/api/patientdata/findpatient/{id}

            PatientDetails ViewModel = new PatientDetails();

            string url = "findPatient/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            PatientDto[] patients = response.Content.ReadAsAsync<PatientDto[]>().Result;
            PatientDto patient = patients.FirstOrDefault();

            ViewModel.SelectedPatient = patient;

            return View(ViewModel);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Patient/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Patient/Create
        [HttpPost]
        public ActionResult Create(Patient patient)
        {
            //objective: add a new patient into our system using the API
            //curl -H "Content-type:application/json" -d @patient.json https://localhost:44396/api/patientdata/addpatient
            string url = "addpatients";

            string jsonpayload = jss.Serialize(patient);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
            return RedirectToAction("List");
            } else
            {
                return RedirectToAction("Errors");
            }

        }

        // GET: Patient/Edit/5
        public ActionResult Edit(int id)
        {
            PatientUpdate ViewModel = new PatientUpdate();

            string url = "PatientData/FindPatient/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            PatientDto[] SelectedPatients = response.Content.ReadAsAsync<PatientDto[]>().Result;
            PatientDto SelectedPatient = SelectedPatients.FirstOrDefault();

            ViewModel.SelectedPatient = SelectedPatient;

            return View(ViewModel);
        }

        // POST: Patient/Update/5
        [HttpPost]
        public ActionResult Edit(int id, Patient patient)
        {
            string url = "PatientData/UpdatePatient/" + id;
            string jsonpayload = jss.Serialize(patient);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Errors");
            }
        }

        // GET: Patient/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "PatientData/FindPatient/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PatientDto[] SelectedPatients = response.Content.ReadAsAsync<PatientDto[]>().Result;
            PatientDto SelectedPatient = SelectedPatients.FirstOrDefault();

            return View(SelectedPatient);
        }

        // POST: Patient/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "PatientData/UpdatePatient/" + id;

            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Errors");
            }
        }
    }
}
