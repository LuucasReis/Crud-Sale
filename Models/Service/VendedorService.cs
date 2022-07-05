using AppVendas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppVendas.Models.Services
{
    public class VendedorService
    {
        private readonly DepartmentsContext _context;

        public VendedorService(DepartmentsContext context)
        {
            _context = context;
        }

        public List<Vendedor> FindAll()
        {
            return _context.Vendedor.ToList();
        }

        public void Insert(Vendedor vendedor)
        {
            _context.Add(vendedor);
            _context.SaveChanges();
        }
    }
}