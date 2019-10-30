using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using localb.Models;
using Microsoft.AspNetCore.Authorization;

namespace localb.Controllers
{
    
    public class HomeController : Controller
    {
        private string URL { get; set; }
        private string URLENDPOINT { get; set; }
        private readonly IHttpCalls _HttpCalls;
        public HomeController(IHttpCalls httpcalls)
        {
            _HttpCalls = httpcalls;
        }        
        public async Task<IActionResult> Index(int? page)
        {            
            IEnumerable<Ad_List> result = (page.HasValue) 
                ? await _HttpCalls.BuyOnline(max: page.Value)
                : await _HttpCalls.BuyOnline();           
            return View("Index", result);
        }        
        //public async Task<IActionResult> Index()
        //{
        //    var result = await _HttpCalls.BuyOnline();
        //    return View(result.d);
        //}
        public async Task<IActionResult> Pagination(int page)
        {
            var result = await _HttpCalls.BuyOnline(max:page);
            return View("Index", result);            
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
