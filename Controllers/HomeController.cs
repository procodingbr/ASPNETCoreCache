using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using ProCoding.Demos.ASPNetCore.Cache.Models;

namespace ProCoding.Demos.ASPNetCore.Cache
{
    public class HomeController : Controller
    {
        private readonly IDistributedCache _cache;

        public HomeController(IDistributedCache cache)
        {
            _cache = cache;
        }
        
        public IActionResult Index()
        {
            ViewData["Title"] = _cache.GetString("Title") ?? "[No Title Set]";
            return View();
        }

        public IActionResult SetTitle(string id)
        {
            _cache.SetString("Title", id);
            return RedirectToAction("Index");
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
