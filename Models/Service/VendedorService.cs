using AppVendas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

        public Vendedor FindByID(int id)
        {
            return _context.Vendedor.Include(x=> x.Department).FirstOrDefault(x=> x.Id == id);
        }

        public void Remove(int id)
        {
           var vendedor = _context.Vendedor.Find(id);
           _context.Vendedor.Remove(vendedor);
           _context.SaveChanges();
        }
    }
}