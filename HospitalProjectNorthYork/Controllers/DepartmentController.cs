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
    public class DepartmentController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static DepartmentController()
        {
            HttpClientHandler handler = new HttpClientHandler();
            client = new HttpClient(handler);
            client.BaseAddress = new Uri("https://localhost:44396/api/");
        }

        //GET: department/List
        public ActionResult List()
        {
            DepartmentList list = new DepartmentList();
            string url = "DepartmentData/ListDepartments";
            HttpResponseMessage res = client.GetAsync(url).Result;

            IEnumerable<DepartmentDto> departments = res.Content.ReadAsAsync<IEnumerable<DepartmentDto>>().Result;
            Debug.WriteLine("Number of Departments received : ");
            Debug.WriteLine(departments.Count());

            list.Departments = departments;
            return View(list);
        }
        //GET: department/details/{department_ID}
        public ActionResult Details(int id)
        {
            DepartmentDetails viewModel = new DepartmentDetails();

            string url = "DepartmentData/FindDepartment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            Debug.WriteLine("response code: " + response.StatusCode);

            DepartmentDto department = response.Content.ReadAsAsync<DepartmentDto>().Result;

            viewModel.SelectedDepartment = department;
            Debug.WriteLine("department Num: " + department.Department_ID);

            url = "faqData/ListFaqForDepartment" + id;
            response = client.GetAsync(url).Result;

            FAQDto fAQs = response.Content.ReadAsAsync<FAQDto>().Result;
            viewModel.FAQs = fAQs;

            url = "LocationData/ListLocationForDepartment" + id;
            response = client.GetAsync(url).Result;

            LocationDto location= response.Content.ReadAsAsync<LocationDto>().Result;
            viewModel.Location = location;


            return View(viewModel);
        }
        public ActionResult Error()
        {

            return View();
        }

        //POST: Department/Associate/{department_ID}
        [HttpPost]
        public ActionResult Associate(int id, int Location_ID)
        {

            //call our api to associate Department with Location
            string url = "DepartmentData/AssociateDepartmentWithLocation/" + id + "/" + Location_ID;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("Details/" + id);
        }

        //Get: Department/UnAssociate/{id}?Location_ID={Location_ID}
        [HttpGet]
        public ActionResult UnAssociate(int id, int Location_ID)
            
        {

            //call our api to unassociate Department with location
            string url = "departmentData/UnAssociateDepartmentWithLocaiton/" + id + "/" + Location_ID;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("Details/" + id);
        }

        //GET: department/new
        public ActionResult New()
        {

            string url = "FaqData/ListFaqs";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<FAQDto> fAQInfo = response.Content.ReadAsAsync<IEnumerable<FAQDto>>().Result;

            url = "LocationData/ListLocations";
            response = client.GetAsync(url).Result;

            IEnumerable<LocationDto> locationInfo = response.Content.ReadAsAsync<IEnumerable<LocationDto>>().Result;

            //add the information to the view model
            NewDepartment viewModel = new NewDepartment();
            viewModel.fAQ = fAQInfo;
            viewModel.Location = locationInfo;



            return View(viewModel);
        }

        // POST: department/Create
        [HttpPost]
        public ActionResult Create(Department department)
        {
            
            string url = "DepartmentData/AddDepartment";

            string jsonpayload = jss.Serialize(department.);
            Debug.WriteLine(jsonpayload);

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

        public ActionResult Edit(int id)
        {
            DepartmentUpdate ViewModel = new DepartmentUpdate();

            //the existing department information
            string url = "DepartmentData/FindDepartment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;


            DepartmentDto SelectedDepartment = response.Content.ReadAsAsync<DepartmentDto>().Result;

            ViewModel.SelectedDepartment = SelectedDepartment;


            url = "FaqData/listFaqs/";
            response = client.GetAsync(url).Result;
            IEnumerable<FAQDto> fAQs = response.Content.ReadAsAsync<IEnumerable<FAQDto>>().Result;

            ViewModel.FAQ = fAQs;

            url = "LocationData/listLocations/";
            response = client.GetAsync(url).Result;
            IEnumerable<LocationDto> locations = response.Content.ReadAsAsync<IEnumerable<LocationDto>>().Result;

            ViewModel.location = locations;


            return View(ViewModel);
        }

        // POST: department/update/5
        [HttpPost]
        public ActionResult Update(int id, Department department)
        {
            Debug.WriteLine("the inputed department name is ");
            Debug.WriteLine(department.DepartmentName);

            string url = "DepartmentData/UpdateDepartment/" + id;
            string jsonpayload = jss.Serialize(department);
            Debug.WriteLine("Jason Data: ");
            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
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
    }
}