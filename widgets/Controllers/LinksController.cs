using Microsoft.AspNetCore.Mvc;
using widgets.Data;
using widgets.Data.Entities;
using widgets.Extensions;
using widgets.Models;
using widgets.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace widgets.Controllers
{
    public class LinksController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUrlGenerator _urlGenerator;

        public LinksController(ApplicationDbContext dbContext, IUrlGenerator urlGenerator)
        {
            _dbContext = dbContext;
            _urlGenerator = urlGenerator;
        }

        //[Route("{shortLink}")]
        public async Task<IActionResult> Link(string shortLink)
        {
            int linkId = _urlGenerator.Decode(shortLink);
            var link = await _dbContext.Widgets.FindAsync(linkId);

            if (link == null)
                return NotFound(new { shortLink });

            try
            {
                link.CountConversion += 1;
                _dbContext.Widgets.Update(link);
                await _dbContext.SaveChangesAsync();

                return RedirectToAction("Rating","Widgets", new { link.Link });
            }
            catch
            {
                return RedirectToAction(nameof(HomeController.Index), this.UrlName<HomeController>());
            }
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm]WidgetViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var lastWidget = _dbContext.Widgets.OrderBy(f => f.Id).LastOrDefault();
                var widget = new Widget()
                {
                    //LongUrl = model.LongUrl,
                    Name = model.Name,
                    CreatedDate = DateTime.UtcNow,
                    Yandex = model.Yandex,
                    Google = model.Google,
                    TwoGIS = model.TwoGIS
                };

                if (lastWidget != null)
                    widget.Id = lastWidget.Id + 1;




                _dbContext.Widgets.Add(widget);
                await _dbContext.SaveChangesAsync();

                widget.Link = _urlGenerator.Encode(widget.Id);
                _dbContext.Update(widget);
                await _dbContext.SaveChangesAsync();

                return RedirectToAction(nameof(HomeController.Index), this.UrlName<HomeController>());
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "На сервере произошла ошибка, попробуйте позже");
                return View(model);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var link = await _dbContext.Links.FindAsync(id);

            if (link == null)
                return NotFound(new { id });

            return View(new LinkViewModel()
            {
                Id = link.Id,
                CountConversion = link.CountConversion,
                CreatedDate = link.CreatedDate,
                LongUrl = link.LongUrl,
                ShortUrl = $"{HttpContext.Request.Host}/{link.ShortUrl}"
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [FromForm] LinkViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var link = await _dbContext.Links.FindAsync(id);

            if (link == null)
                return NotFound(new { id });

            try
            {
                link.LongUrl = model.LongUrl;
                _dbContext.Links.Update(link);
                await _dbContext.SaveChangesAsync();

                TempData["Message"] = "Изменения сохранены";

                return View(new LinkViewModel()
                {
                    Id = link.Id,
                    CountConversion = link.CountConversion,
                    CreatedDate = link.CreatedDate,
                    LongUrl = link.LongUrl,
                    ShortUrl = $"{HttpContext.Request.Host}/{link.ShortUrl}"
                });
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "На сервере произошла ошибка, попробуйте позже");
                return View(model);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var link = await _dbContext.Links.FindAsync(id);
            if (link == null)
                return NotFound();

            try
            {
                _dbContext.Links.Remove(link);
                await _dbContext.SaveChangesAsync();

                return Ok(new { });
            }
            catch
            {
                return BadRequest("На сервере произошла ошибка, попробуйте позже");
            }
        }
    }
}
