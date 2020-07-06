using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DataGov_API_Intro_ej.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DataGov_API_Intro_ej.Controllers
{
    public class HomeController : Controller
    {
        HttpClient httpClient;

        // Obtaining the API key is easy. The same key should be usable across the entire
        // data.gov developer network, i.e. all data sources on data.gov.
        // https://www.nps.gov/subjects/developer/get-started.htm
        //Your API key for eapenjohn@hotmail.com is:BMY3pb3vdn5TKtuyxdaXuMoUXYbJN3ix20DBW3fU

        static string BASE_URL = "https://developer.nps.gov/api/v1/";
        static string API_KEY = "BMY3pb3vdn5TKtuyxdaXuMoUXYbJN3ix20DBW3fU"; //Add your API key here inside ""
        public IActionResult Index()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);
            httpClient.DefaultRequestHeaders.Accept.Add(
                 new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            string NATIONAL_PARK_API_PATH = BASE_URL + "/parks?limit=20";
            string parksData = "";
            Parks parks = null;
            try
            {
                HttpResponseMessage response = httpClient.GetAsync(NATIONAL_PARK_API_PATH).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    parksData = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }

                if (!parksData.Equals(""))
                {
                    // JsonConvert is part of the NewtonSoft.Json Nuget package
                    parks = JsonConvert.DeserializeObject<Parks>(parksData);
                }
            }
            catch (Exception e)
            {
                // This is a useful place to insert a breakpoint and observe the error message
                Console.WriteLine(e.Message);
            }
            return View(parks);
        }

        public IActionResult Link()
        {
            return View();
        }
    }
}




