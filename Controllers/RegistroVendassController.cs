using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppVendas.Models.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppVendas.Controllers
{
    public class RegistroVendassController : Controller
    {
        private readonly RegistroVendasService _RegistroVendasService;

        public RegistroVendassController(RegistroVendasService service)
        {
            _RegistroVendasService = service;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value;
            ViewData["maxDate"] = maxDate.Value;
            var result = await _RegistroVendasService.FindByDateAsync(minDate, maxDate);
            return View(result);
        }

        public async Task<IActionResult> GroupingSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            var result = await _RegistroVendasService.FindByDateGroupingAsync(minDate, maxDate);
            return View(result);
        }
    }
}