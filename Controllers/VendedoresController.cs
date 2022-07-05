using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppVendas.Models;
using AppVendas.Models.Services;

namespace AppVendas.Controllers
{
    public class VendedoresController : Controller
    {
        private readonly VendedorService _VendedorService;

        public VendedoresController(VendedorService VendedorService)
        {
            _VendedorService = VendedorService;
        }

        public IActionResult Index()
        {
            var list = _VendedorService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Vendedor Vendedor)
        {
            _VendedorService.Insert(Vendedor);
            return RedirectToAction(nameof(Index));
        }
    }
}