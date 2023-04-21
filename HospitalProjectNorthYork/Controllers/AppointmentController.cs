using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using HospitalProjectNorthYork.Models;
using HospitalProjectNorthYork.Models.ViewModels;
using System.Web.Script.Serialization;
using Microsoft.Ajax.Utilities;

namespace HospitalProjectNorthYork.Controllers
{
    public class AppointmentController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static AppointmentController()
        {
            HttpClientHandler handler = new HttpClientHandler();
            client = new HttpClient(handler);
            client.BaseAddress = new Uri("https://localhost:44396/api/");
        }

        //GET: Appointment/List
        public ActionResult List()
        {
            AppointmentList list = new AppointmentList();
            string url = "AppointmentData/ListAppointments";
            HttpResponseMessage res = client.GetAsync(url).Result;

            IEnumerable<AppointmentDto> appointments = res.Content.ReadAsAsync<IEnumerable<AppointmentDto>>().Result;
            Debug.WriteLine("Number of appointments received : ");
            Debug.WriteLine(appointments.Count());

            list.Appointments = appointments;
            return View(list);
        }

        //GET: appointment/details/{appId}
        public ActionResult Details(int id)
        {
            AppointmentDetails viewModel = new AppointmentDetails();

            string url = "AppointmentData/FindAppointment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            Debug.WriteLine("response code: "+ response.StatusCode);

            AppointmentDto[] appointments = response.Content.ReadAsAsync<AppointmentDto[]>().Result;
            AppointmentDto appointment = appointments.FirstOrDefault();

            viewModel.SelectedAppointment = appointment;
            Debug.WriteLine("appointment Num: " + appointment.Appointment_ID);

            url = "PatientData/FindPatient/" + appointment.Patient_ID;
            response = client.GetAsync(url).Result;

            PatientDto patient = response.Content.ReadAsAsync<PatientDto>().Result;
            viewModel.Patient = patient;


            url = "DoctorsData/FindDoctor/" + appointment.Doctor_ID;
            response = client.GetAsync(url).Result;

            DoctorsDto doctor = response.Content.ReadAsAsync<DoctorsDto>().Result;
            viewModel.Docter = doctor;


            url = "LocationData/FindLocation/" + appointment.Location_ID;
            response = client.GetAsync(url).Result;

            LocationDto location = response.Content.ReadAsAsync<LocationDto>().Result;
            viewModel.Location = location;


            return View(viewModel);
        }
        public ActionResult New()
        {
          
            string url = "PatientData/ListPatients";

            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<PatientDto> patientInfo = response.Content.ReadAsAsync<IEnumerable<PatientDto>>().Result;


            string url2 = "DoctorsData/ListDoctors";
            HttpResponseMessage response2 = client.GetAsync(url2).Result;

            IEnumerable<DoctorsDto> doctorInfo = response2.Content.ReadAsAsync<IEnumerable<DoctorsDto>>().Result;

            string url3 = "LocationData/ListLocations";
            HttpResponseMessage response3 = client.GetAsync(url3).Result;

            IEnumerable<LocationDto> locationInfo = response3.Content.ReadAsAsync<IEnumerable<LocationDto>>().Result;

            //add the information to the view model
            NewAppointment viewModel = new NewAppointment();
            viewModel.Patient = patientInfo;
            viewModel.Doctor = doctorInfo;
            viewModel.Location = locationInfo;



            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Appointment appointment)
        {
            Debug.WriteLine("Enter Create method");
            string url = "AppointmentData/AddAppointment";
            string JsonPayload = jss.Serialize(appointment);
            Debug.WriteLine(JsonPayload);
            HttpContent content = new StringContent(JsonPayload);
            Debug.WriteLine(content.Headers);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url,content).Result;

            Debug.WriteLine(response);

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
            AppointmentUpdate ViewModel = new AppointmentUpdate();

            //the existing appointment information
            string url = "AppointmentData/FindAppointment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;


            AppointmentDto[] SelectedAppointments = response.Content.ReadAsAsync<AppointmentDto[]>().Result;
            AppointmentDto SelectedAppointment = SelectedAppointments.FirstOrDefault();

            ViewModel.SelectedAppointment = SelectedAppointment;


            url = "PatientData/listPatients/";
            response = client.GetAsync(url).Result;
            IEnumerable<PatientDto> patients = response.Content.ReadAsAsync<IEnumerable<PatientDto>>().Result;

            ViewModel.Patients = patients;

            url = "DoctorsData/listDoctors/";
            response = client.GetAsync(url).Result;
            IEnumerable<DoctorsDto> doctors = response.Content.ReadAsAsync<IEnumerable<DoctorsDto>>().Result;

            ViewModel.Doctors = doctors;


            url = "LocationData/listLocations/";
            response = client.GetAsync(url).Result;
            IEnumerable<LocationDto> locations= response.Content.ReadAsAsync<IEnumerable<LocationDto>>().Result;

            ViewModel.Locations = locations;


            return View(ViewModel);
        }

        // POST: Appointment/update/5
        [HttpPost]
        public ActionResult Update(int id, Appointment app)
        {
            Debug.WriteLine("the inputed Appointment desc is ");
            Debug.WriteLine(app.AppointmentDesc);

            string url = "AppointmentData/UpdateAppointment/" + id;
            string jsonpayload = jss.Serialize(app);
            Debug.WriteLine("Jason Data: ");
            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url,content).Result;
            Debug.WriteLine(content);
            Debug.WriteLine(response);


            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
        // GET: AppointmentData/Delete/5
        public ActionResult DeleteConfirm(int id)
        {

            string url = "AppointmentData/FindAppointment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            AppointmentDto[] SelectedAppointments = response.Content.ReadAsAsync<AppointmentDto[]>().Result;
            AppointmentDto SelectedAppointment = SelectedAppointments.FirstOrDefault();

            return View(SelectedAppointment);
        }

        // POST: appointment/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "AppointmentData/DeleteAppointment/" + id;
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