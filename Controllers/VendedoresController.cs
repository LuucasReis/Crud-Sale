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

        public async Task<IActionResult> Index()
        {
            var list = await _VendedorService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _DepartmentService.FindAllDepartmentAsync();
            var ViewModel = new VendedorFormViewModel() { Departments = departments };
            return View(ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vendedor vendedor)
        {

            await _VendedorService.InsertAsync(vendedor);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não especificado." });
            }

            var vendedor = await _VendedorService.FindByIDAsync(id.Value);
            if (vendedor == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado." });
            }

            return View(vendedor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _VendedorService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
               return RedirectToAction(nameof(Error), new { message = e.Message});
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não especificado" });
            }

            var vendedor = await _VendedorService.FindByIDAsync(id.Value);
            if (vendedor == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }

            return View(vendedor);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não especificado" });
            }

            var obj = await _VendedorService.FindByIDAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }

            List<Department> departments = await _DepartmentService.FindAllDepartmentAsync();
            VendedorFormViewModel viewModel = new VendedorFormViewModel { Vendedor = obj, Departments = departments };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, Vendedor vendedor)
        {


            if (id != vendedor.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não correspondente." });
            }

            try
            {
                await _VendedorService.UpdateAsync(vendedor);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {

                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Message = message
            };

            return View(viewModel);
        }
    }
}