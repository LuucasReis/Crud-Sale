using System.Collections.Generic;
namespace AppVendas.Models.ViewModels
{
    public class VendedorFormViewModel
    {
        public Vendedor Vendedor { get; set; }
        public ICollection<Department> Departments { get; set; }
    }
}