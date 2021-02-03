using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using widgets.Models;

namespace widgets.Controllers
{
    public class WidgetsController : Controller
    {
        private readonly ILogger<WidgetsController> _logger;
        private readonly EmailSender service;

        public WidgetsController(ILogger<WidgetsController> logger, EmailSender service)
        {
            _logger = logger;
            this.service = service;
        }

        public IActionResult Rating()
        {
            return View();
        }

        public IActionResult Review()
        {
            return View();
        }
        public IActionResult Email()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SendEmailDefault(string review,string tel)
        {
            service.SendEmailDefault(review,tel);
            return RedirectToAction("Email");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
