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

            url = "faqData/ListFaqsForDepartment/" + id;
            response = client.GetAsync(url).Result;

            IEnumerable<FAQDto> RelatedFAQs = response.Content.ReadAsAsync<IEnumerable<FAQDto>>().Result;
            viewModel.RelatedFAQs = RelatedFAQs;


            url = "faqData/ListFaqsNotForDepartment/" + id;

            response = client.GetAsync(url).Result;

            IEnumerable<FAQDto> UnrelatedFAQs = response.Content.ReadAsAsync<IEnumerable<FAQDto>>().Result;
            viewModel.UnrelatedFAQs = UnrelatedFAQs;

            //Location methods need to be finished

            //url = "LocationData/ListLocationsForDepartment/" + id;
            //response = client.GetAsync(url).Result;

            //IEnumerable<LocationDto> RelatedLocations = response.Content.ReadAsAsync<IEnumerable<LocationDto>>().Result;
            //viewModel.RelatedLocations = RelatedLocations;

            //url = "LocationData/ListLocationsNotForDepartment/" + id;
            //response = client.GetAsync(url).Result;

            //IEnumerable<LocationDto> unrelatedLocations = response.Content.ReadAsAsync<IEnumerable<LocationDto>>().Result;
            //viewModel.unrelatedLocations = unrelatedLocations;

            return View(viewModel);
        }
        public ActionResult Error()
        {

            return View();
        }

        //POST: Department/AssociateLocation/{department_ID, Location_ID}
        [HttpPost]
        public ActionResult AssociateLocation(int id, int Location_ID)
        {

            //call our api to associate Department with Location
            string url = "DepartmentData/AssociateDepartmentWithLocation/" + id + "/" + Location_ID;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("Details/" + id);
        }

        //Get: Department/UnAssociateLocation/{id}?Location_ID={Location_ID}
        [HttpGet]
        public ActionResult UnAssociateLocation(int id, int Location_ID)
            
        {

            //call our api to unassociate Department with location
            string url = "departmentData/UnAssociateDepartmentWithLocaiton/" + id + "/" + Location_ID;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("Details/" + id);
        }


        //POST: Department/AssociateFAQ/{department_ID, FAQ_ID}
        [HttpPost]
        public ActionResult AssociateFAQ(int id, int FAQ_ID)
        {

            //call our api to associate Department with FAQ
            string url = "DepartmentData/AssociateDepartmentWithFAQ/" + id + "/" + FAQ_ID;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("Details/" + id);
        }

        //Get: Department/UnAssociateFAQ/{id}?FAQ_ID={FAQ_ID}
        [HttpGet]
        public ActionResult UnAssociateFAQ(int id, int FAQ_ID)

        {
            //call our api to unassociate Department with location
            string url = "departmentData/UnAssociateDepartmentWithFAQ/" + id + "/" + FAQ_ID;
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

            string jsonpayload = jss.Serialize(department);
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
            Debug.WriteLine(content.Headers);
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

        // GET: DepartmentData/Delete/5
        public ActionResult DeleteConfirm(int id)
        {

            string url = "DepartmentData/FindDepartment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            DepartmentDto SelectedDepartment= response.Content.ReadAsAsync<DepartmentDto>().Result;
     

            return View(SelectedDepartment);
        }

        // POST: Department/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "DepartmentData/DeleteDepartment/" + id;
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