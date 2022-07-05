using Microsoft.EntityFrameworkCore;
namespace AppVendas.Models.Services
{
    public class RegistroVendasService
    {
        private readonly DepartmentsContext _context;

        public RegistroVendasService(DepartmentsContext context)
        {
            _context = context;
        }

        public async Task<List<RegistroVendas>> FindByDate(DateTime? inicio, DateTime? fim)
        {
            var result = from x in _context.RegistroVendas select x;

            if (inicio.HasValue)
            {
                result = result.Where(x => x.Data >= inicio.Value);
            }

            if (fim.HasValue)
            {
                result = result.Where(x => x.Data <= fim.Value);
            }

            return await result
                   .Include(x => x.Vendedor)
                   .Include(x => x.Vendedor.Department)
                   .OrderByDescending(x => x.Data)
                   .ToListAsync();
        }
    }
}