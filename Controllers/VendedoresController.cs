using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppVendas.Models;
using AppVendas.Models.ViewModels;
using AppVendas.Models.Services;

namespace AppVendas.Controllers
{
    public class VendedoresController : Controller
    {
        private readonly VendedorService _VendedorService;
        private readonly DepartmentService _DepartmentService;

        public VendedoresController(VendedorService VendedorService, DepartmentService departmentService)
        {
            _VendedorService = VendedorService;
            _DepartmentService = departmentService;
        }

        public IActionResult Index()
        {
            var list = _VendedorService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            var departments = _DepartmentService.FindAllDepartment();
            var ViewModel= new VendedorFormViewModel(){Departments = departments};
            return View(ViewModel);
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