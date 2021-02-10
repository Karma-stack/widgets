using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using widgets.Data;
using widgets.Data.Entities;
using widgets.Models;
using widgets.Services;

namespace widgets.Controllers
{
    public class WidgetsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUrlGenerator _urlGenerator;
        private readonly EmailSender service;

        public WidgetsController(ApplicationDbContext dbContext, IUrlGenerator urlGenerator, EmailSender service)
        {
            _dbContext = dbContext;
            _urlGenerator = urlGenerator;
            this.service = service;
        }

        [Route("{query}")]
        public async Task<IActionResult> Rating(string query)
        {
            int linkId = _urlGenerator.Decode(query);
            var link = await _dbContext.Widgets.FindAsync(linkId);

            if (link == null)
                return NotFound(new { query });

            return View(new WidgetViewModel() { Id = link.Id ,Yandex = link.Yandex,Google = link.Google, TwoGIS = link.TwoGIS });
        }

        public async Task<IActionResult> Review()
        {
            var links = await _dbContext.Widgets.AsNoTracking().OrderByDescending(f => f.CreatedDate)
                .Select(f => new WidgetViewModel()
                {
                    Id = f.Id,
                    Name = f.Name,
                    Yandex = f.Yandex,
                    Google = f.Google,
                    TwoGIS = f.TwoGIS
                }).ToListAsync();

            return View(links);
        }
        public IActionResult Email()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(string review, string tel,int id)
        {
            try
            {

                var currentWidget = await _dbContext.Widgets.FindAsync(id);

                var reviews = new Review()
                {
                    review = review,
                    CreatedDate = DateTime.UtcNow,
                    TelNum = tel,
                    Widget = currentWidget
                };

                _dbContext.Reviews.Add(reviews);
                await _dbContext.SaveChangesAsync();

                service.SendEmail(review, tel, id);
                return NoContent();
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "На сервере произошла ошибка, попробуйте позже");
                return View();
            }
            //return RedirectToAction("Email");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
