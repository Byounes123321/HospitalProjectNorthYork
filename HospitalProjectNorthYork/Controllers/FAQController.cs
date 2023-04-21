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
    public class FAQController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static FAQController()
        {
            HttpClientHandler handler = new HttpClientHandler();
            client = new HttpClient(handler);
            client.BaseAddress = new Uri("https://localhost:44396/api/");
        }

        //GET: Visit/List
        public ActionResult List()
        {
            FAQList list = new FAQList();
            string url = "FAQData/ListFAQs";
            HttpResponseMessage res = client.GetAsync(url).Result;

            IEnumerable<FAQDto> FAQS = res.Content.ReadAsAsync<IEnumerable<FAQDto>>().Result;


            list.FAQS = FAQS;
            return View(list);
        }

        //GET: visit/details/{FAQ_ID}
        public ActionResult Details(int id)
        {
            FAQDetails viewModel = new FAQDetails();

            string url = "FAQData/FindFAQ/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;


            FAQDto FAQ = response.Content.ReadAsAsync<FAQDto>().Result;
           

            viewModel.SelectedFAQ = FAQ;


            url = "departmentData/ListDepartmentsForFAQ/" + id;
            response = client.GetAsync(url).Result;

            IEnumerable<DepartmentDto> RelatedDepartments= response.Content.ReadAsAsync<IEnumerable<DepartmentDto>>().Result;
            viewModel.RelatedDepartments = RelatedDepartments;

            url = "departmentData/ListDepartmentsNotForFAQ/" + id;
            response = client.GetAsync(url).Result;

            IEnumerable<DepartmentDto> UnrelatedDepartments = response.Content.ReadAsAsync<IEnumerable<DepartmentDto>>().Result;
            viewModel.UnrelatedDepartments = UnrelatedDepartments;



            return View(viewModel);
        }

        public ActionResult Error()
        {
            return View();
        }


        //GET: FAQ/new
        public ActionResult New()
        {

            string url = "DepartmentData/ListDepartments";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<DepartmentDto> departmentInfo = response.Content.ReadAsAsync<IEnumerable<DepartmentDto>>().Result;

          
            //add the information to the view model
            NewFAQ viewModel = new NewFAQ();
            viewModel.Department = departmentInfo;
           



            return View(viewModel);
        }


        // POST: FAQ/Create

        [HttpPost]
        public ActionResult Create(FAQ fAQ)
        {

            string url = "FAQData/AddFAQ";
            string JsonPayload = jss.Serialize(fAQ);

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
            FAQUpdate ViewModel = new FAQUpdate();

            //the existing faq information
            string url = "FAQData/FindFAQ/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;


            FAQDto SelectedFAQ = response.Content.ReadAsAsync<FAQDto>().Result;
          
            ViewModel.SelectedFAQ = SelectedFAQ;


            url = "DepartmentData/listDepartments/";
            response = client.GetAsync(url).Result;
            IEnumerable<DepartmentDto> department = response.Content.ReadAsAsync<IEnumerable<DepartmentDto>>().Result;

            ViewModel.department = departments;

          
            return View(ViewModel);
        }

        // POST: FAQ/update/5
        [HttpPost]
        public ActionResult Update(int id, FAQ faq)
        {


            string url = "FAQData/UpdateFAQ/" + id;
            string jsonpayload = jss.Serialize(faq);
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
        // GET: FAQData/Delete/5
        public ActionResult DeleteConfirm(int id)
        {

            string url = "FAQData/FindFAQ/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            FAQDto SelectedFAQ = response.Content.ReadAsAsync<FAQDto>().Result;
           

            return View(SelectedFAQ);
        }

        // POST: FAQ/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "FAQData/DeleteFAQ/" + id;
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
