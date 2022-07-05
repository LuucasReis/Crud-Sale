using AppVendas.Models;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppVendas.Models.Services.Exceptions;
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

        public async Task<List<Vendedor>> FindAllAsync()
        {
           return await _context.Vendedor.ToListAsync();
        }

        public async Task InsertAsync(Vendedor vendedor)
        {
            _context.Add(vendedor);
            await _context.SaveChangesAsync();
        }

        public async Task<Vendedor> FindByIDAsync(int id)
        {
            return await _context.Vendedor.Include(x => x.Department).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task RemoveAsync(int id)
        {

            try
            {
                var vendedor = await _context.Vendedor.FindAsync(id);
                _context.Vendedor.Remove(vendedor);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                
                throw new IntegrityException("Não posso deletar o vendedor, pois ele tem vendas.");
            }
        }

        public async Task UpdateAsync(Vendedor vendedor)
        {
            if (! await _context.Vendedor.AnyAsync(x => x.Id == vendedor.Id))
            {
                throw new NotFoundException("Id Não encontrada");
            }
            try
            {
                _context.Update(vendedor);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException e)
            {

                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}