using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace AppVendas.Models.Services
{
    public class DepartmentService
    {
        private readonly DepartmentsContext _context;

        public DepartmentService(DepartmentsContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> FindAllDepartmentAsync()
        {
            return await _context.Department.OrderBy(x => x.Nome).ToListAsync();
        }
    }
}