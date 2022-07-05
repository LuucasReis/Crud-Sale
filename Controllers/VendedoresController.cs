using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppVendas.Models;
using AppVendas.Models.ViewModels;
using AppVendas.Models.Services;
using AppVendas.Models.Services.Exceptions;

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
            var ViewModel = new VendedorFormViewModel() { Departments = departments };
            return View(ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Vendedor Vendedor)
        {
            _VendedorService.Insert(Vendedor);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new{message="Id não especificado."});
            }

            var vendedor = _VendedorService.FindByID(id.Value);
            if (vendedor == null)
            {
                return RedirectToAction(nameof(Error), new{message ="Id não encontrado." });
            }

            return View(vendedor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _VendedorService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new{message="Id não especificado"});
            }

            var vendedor = _VendedorService.FindByID(id.Value);
            if (vendedor == null)
            {
                return RedirectToAction(nameof(Error), new{message="Id não encontrado"});
            }

            return View(vendedor);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new{message= "Id não especificado"});
            }

            var obj = _VendedorService.FindByID(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new{message= "Id não encontrado"});
            }

            List<Department> departments = _DepartmentService.FindAllDepartment();
            VendedorFormViewModel viewModel = new VendedorFormViewModel { Vendedor = obj, Departments = departments };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(int id, Vendedor vendedor)
        {
            if (id != vendedor.Id)
            {
                return RedirectToAction(nameof(Error), new{message="Id não correspondente."});
            }

            try
            {
                _VendedorService.Update(vendedor);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {

                return RedirectToAction(nameof(Error), new{message = e.Message});
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
               RequestId = Activity.Current?.Id?? HttpContext.TraceIdentifier,
               Message = message
            };
            
            return View(viewModel);
        }
    }
}