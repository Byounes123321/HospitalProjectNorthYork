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
    public class LocationController : Controller
    {
         private static readonly HttpClient client;
        JavaScriptSerializer jss = new JavaScriptSerializer();

        static LocationController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44396/api/");
        }

        // GET: Location/List
        public ActionResult List()
        {
            //objective: communicate with our location data api to retrieve a list of locations
            //curl https://localhost:44396/api/locationdata/listlocations

            string url = "locationdata/listLocations";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<Location> locations = response.Content.ReadAsAsync<IEnumerable<Location>>().Result;

            return View(locations);
        }

        // GET: Location/Details/5
        public ActionResult Details(int id)
        {

            //objective: communicate with our location data api to retrieve one location
            // curl https://localhost:44396/api/locationdata/findlocation/{id}

            LocationDetails ViewModel = new LocationDetails();

            string url = "locationdata/findLocation/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            LocationDto selectedLocation = response.Content.ReadAsAsync<LocationDto>().Result;

            return View(ViewModel);
        }

        public ActionResult Error()
        {
            return View();
        }


        // GET: Location/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Location/Create
        [HttpPost]
        public ActionResult Create(Location location)
        {
            //objective: add a new location into our system using the API
            //curl -H "Content-type:application/json" -d @location.json https://localhost:44396/api/locationdata/addlocation
            string url = "locationdata/addlocation";

            string jsonpayload = jss.Serialize(location);

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

        // GET: Location/Edit/5
        public ActionResult Edit(int id)
        {
            LocationUpdate ViewModel = new LocationUpdate();

            string url = "LocationData/FindLocation/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            LocationDto[] SelectedLocations = response.Content.ReadAsAsync<LocationDto[]>().Result;
            LocationDto SelectedLocation = SelectedLocations.FirstOrDefault();

            ViewModel.SelectedLocation = SelectedLocation;

            return View(ViewModel);
        }

        // POST: Location/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Location location)
        {
            string url = "LocationData/UpdateLocation/" + id;
            string jsonpayload = jss.Serialize(location);

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

        // GET: Location/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "LocationData/FindLocation/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            LocationDto[] SelectedLocations = response.Content.ReadAsAsync<LocationDto[]>().Result;
            LocationDto SelectedLocation = SelectedLocations.FirstOrDefault();

            return View(SelectedLocation);
        }

        // POST: Location/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "locationdata/updateLocation/" + id;

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
