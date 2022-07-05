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

        public async Task<List<RegistroVendas>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from x in _context.RegistroVendas select x;

            if (minDate.HasValue)
            {
                result = result.Where(x => x.Data >= minDate.Value);
            }

            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Data <= maxDate.Value);
            }

            return await result
                   .Include(x => x.Vendedor)
                   .Include(x => x.Vendedor.Department)
                   .OrderByDescending(x => x.Data)
                   .ToListAsync();
        }
    }
}