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
        private readonly IHttpCalls _HttpCalls;
        public HomeController(IHttpCalls httpcalls)
        {
            _HttpCalls = httpcalls;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _HttpCalls.BuyOnline();
            return View(result.data.ad_list);
        }        
        //public async Task<IActionResult> Index()
        //{
        //    var result = await _HttpCalls.BuyOnline();
        //    return View(result.d);
        //}
        public IActionResult Pagination(int page)
        {            
            HttpClient client = new HttpClient();
            string url = $"{URL}/api/payment_methods/";
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
