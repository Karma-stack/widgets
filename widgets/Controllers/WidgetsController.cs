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

        public IActionResult Rating()
        {
            return View();
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
        public async Task<IActionResult> SendEmail(string review, string tel, string link)
        {
            try
            {
                var lastReview = _dbContext.Reviews.OrderBy(f => f.Id).LastOrDefault();
                var reviews = new Review()
                {
                    review = review,
                    CreatedDate = DateTime.UtcNow,
                    TelNum = tel
                };

                if (lastReview != null)
                    reviews.Id = lastReview.Id + 1;

                _dbContext.Reviews.Add(reviews);
                await _dbContext.SaveChangesAsync();

                service.SendEmail(review, tel, link);
                return RedirectToAction("Email");
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
