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
    public class DoctorController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static DoctorController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44396/api/");
        }
        // GET: Doctor
        public ActionResult List()
        {
            // communicate with doctor api to retrieve a list of doctors
            // curl https://localhost:44396/api/DoctorsData/ListDoctors

            string url = "DoctorsData/ListDoctors";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<DoctorsDto> doctors = response.Content.ReadAsAsync<IEnumerable<DoctorsDto>>().Result;

            return View(doctors);
        }

        // GET: Doctor/Details/5
        public ActionResult Details(int id)
        {
            // communicate with doctor api to retrieve details of the doctor
            // curl https://localhost:44396/api/DoctorsData/FindDoctor/{id}

            string url = "DoctorsData/FindDoctor/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            DoctorsDto doctor = response.Content.ReadAsAsync<DoctorsDto>().Result;

            return View(doctor);
        }

        // GET: Doctor/New
        public ActionResult New()
        {
            // information about all departments in the system
            // GET/DepartmentData/ListDepartments

            string url = "DepartmentData/ListDepartments";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<DepartmentDto> DepartmentOptions = response.Content.ReadAsAsync<IEnumerable<DepartmentDto>>().Result;

            return View(DepartmentOptions);
        }

        // POST: Doctor/Create
        [HttpPost]
        public ActionResult Create(Doctors doctor)
        {
            // communicate with doctor api to add details of the new doctor
            // curl -H "Content-Type:application/json" -d @doctor.json https://localhost:44396/api/DoctorsData/AddDoctor
            string url = "DoctorsData/AddDoctor";

            // converting payload to json to send to api

            string jsonpayload = jss.Serialize(doctor);

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

        // GET: Doctor/Edit/5
        public ActionResult Edit(int id)
        {
            DoctorUpdate ViewModel = new DoctorUpdate();

            // existing doctor information
            string url = "DoctorsData/FindDoctor/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            DoctorsDto doctor = response.Content.ReadAsAsync<DoctorsDto>().Result;
            ViewModel.SelectedDoctor = doctor;

            // all departments to choose from when updating this doctor
            url = "DepartmentData/ListDepartments";
            response = client.GetAsync(url).Result;
            IEnumerable<DepartmentDto> DepartmentOptions = response.Content.ReadAsAsync<IEnumerable<DepartmentDto>>().Result;
            ViewModel.DepartmentOptions = DepartmentOptions;

            return View(ViewModel);
        }

        // POST: Doctor/Update/5
        [HttpPost]
        public ActionResult Update(int id, Doctors doctor)
        {
            string url = "DoctorsData/UpdateDoctor/" + id;

            // converting payload to json to send to api

            string jsonpayload = jss.Serialize(doctor);

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

        // GET: Doctor/DeleteConfirm/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "DoctorsData/FindDoctor/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            DoctorsDto doctor = response.Content.ReadAsAsync<DoctorsDto>().Result;
            return View(doctor);
        }

        // POST: Doctor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "DoctorsData/DeleteDoctor/" + id;

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
