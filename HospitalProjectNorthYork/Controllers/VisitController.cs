using HospitalProjectNorthYork.Models.ViewModels;
using HospitalProjectNorthYork.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace HospitalProjectNorthYork.Controllers
{
    public class VisitController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static VisitController()
        {
            HttpClientHandler handler = new HttpClientHandler();
            client = new HttpClient(handler);
            client.BaseAddress = new Uri("https://localhost:44396/api/");
        }

        //GET: Visit/List
        public ActionResult List()
        {
            VisitList list = new VisitList();
            string url = "VisitData/ListVisits";
            HttpResponseMessage res = client.GetAsync(url).Result;

            IEnumerable<VisitDto> visits = res.Content.ReadAsAsync<IEnumerable<VisitDto>>().Result;
            

            list.Visits = visits;
            return View(list);
        }

        //GET: visit/details/{appId}
        public ActionResult Details(int id)
        {
            VisitDetails viewModel = new VisitDetails();

            string url = "VisitData/FindVisit/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            

            VisitDto[] visits = response.Content.ReadAsAsync<VisitDto[]>().Result;
            VisitDto visit = visits.FirstOrDefault();

            viewModel.SelectedVisits = visit;
           

            url = "LocationData/FindLocation/" + visit.Location_ID;
            response = client.GetAsync(url).Result;

            LocationDto location = response.Content.ReadAsAsync<LocationDto>().Result;
            viewModel.Location = location;


            url = "PatientData/FindPatient/" + visit.Patient_ID;
            response = client.GetAsync(url).Result;

            PatientDto patient = response.Content.ReadAsAsync<PatientDto>().Result;
            viewModel.Patient = patient;

            return View(viewModel);
        }
        public ActionResult New()
        {

            string url = "PatientData/ListPatients";

            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<PatientDto> patientInfo = response.Content.ReadAsAsync<IEnumerable<PatientDto>>().Result;

            string url2 = "LocationData/ListLocations";
            HttpResponseMessage response2 = client.GetAsync(url2).Result;

            IEnumerable<LocationDto> locationInfo = response2.Content.ReadAsAsync<IEnumerable<LocationDto>>().Result;

            //add the information to the view model
            NewVisit viewModel = new NewVisit();
            viewModel.Patient = patientInfo;
            viewModel.Location = locationInfo;



            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Visit visit)
        {
            
            string url = "VisitData/AddVisit";
            string JsonPayload = jss.Serialize(visit);
           
            HttpContent content = new StringContent(JsonPayload);
           
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
        public ActionResult Edit(int id)
        {
            VisitUpdate ViewModel = new VisitUpdate();

            //the existing appointment information
            string url = "VisitData/Findvisit/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;


             VisitDto[] SelectedVisits = response.Content.ReadAsAsync<VisitDto[]>().Result;
            VisitDto SelectedVisit = SelectedVisits.FirstOrDefault();

            ViewModel.SelectedVisit = SelectedVisit;


            url = "PatientData/listPatients/";
            response = client.GetAsync(url).Result;
            IEnumerable<PatientDto> patients = response.Content.ReadAsAsync<IEnumerable<PatientDto>>().Result;

            ViewModel.Patients = patients;

            url = "LocationData/listLocations/";
            response = client.GetAsync(url).Result;
            IEnumerable<LocationDto> locations = response.Content.ReadAsAsync<IEnumerable<LocationDto>>().Result;

            ViewModel.Locations = locations;


            return View(ViewModel);
        }

        // POST: Visit/update/5
        [HttpPost]
        public ActionResult Update(int id, Visit app)
        {
           

            string url = "VisitData/UpdateVisit/" + id;
            string jsonpayload = jss.Serialize(app);
           ;

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
        // GET: RecipientData/Delete/5
        public ActionResult DeleteConfirm(int id)
        {

            string url = "VisitData/FindVisit/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            VisitDto[] SelectedVisits = response.Content.ReadAsAsync<VisitDto[]>().Result;
            VisitDto SelectedVisit = SelectedVisits.FirstOrDefault();

            return View(SelectedVisit);
        }

        // POST: gift/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "VisitData/DeleteVisit/" + id;
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
    }
}
