﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using widgets.Data;
using widgets.Models;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace widgets.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            var links = await _dbContext.Widgets.AsNoTracking().OrderByDescending(f => f.CreatedDate)
                .Select(f => new WidgetViewModel()
                {
                    Id = f.Id,
                    Name = f.Name,
                    CreatedDate = f.CreatedDate,
                    Link = $"{HttpContext.Request.Host}/{f.Link}",
                    CountConversion = f.CountConversion
                }).ToListAsync();

            return View(links);
        }

        public async Task<IActionResult> Reviews()
        {
            var links = await _dbContext.Reviews.AsNoTracking().OrderByDescending(f => f.CreatedDate)
                .Select(f => new ReviewViewModel()
                {
                    Id = f.Id,
                    review = f.review,
                    TelNum = f.TelNum,
                    CreatedDate = f.CreatedDate
                }).ToListAsync();

            return View(links);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
