using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using localb.Models;
using System.Security.Cryptography;
using System.Net.Http;
using Newtonsoft.Json;

namespace localb.Controllers
{
    public class HomeController : Controller
    {       
        const string URL = "https://localbitcoins.com";
        const string URLENDPOINT = "/buy-bitcoins-online";
        public IActionResult Index()
        {
            HMACSHA256 hash = new HMACSHA256();
            HttpClient client = new HttpClient();
            string url = $"{URL}{URLENDPOINT}/VE/Venezuela/.json";
            var response = client.GetStringAsync(url).Result;
            Rootobject result = JsonConvert.DeserializeObject<Rootobject>(response);
            return View(result.data.ad_list);
        }
        public IActionResult Pagination(int page)
        {
            HMACSHA256 hash = new HMACSHA256();
            HttpClient client = new HttpClient();
            string url = $"{URL}{URLENDPOINT}/VE/Venezuela/.json?page={page}";
            var response = client.GetStringAsync(url).Result;
            return View(response);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
