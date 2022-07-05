using System.Linq;
namespace AppVendas.Models.Services
{
    public class DepartmentService
    {
        private readonly DepartmentsContext _context;

        public DepartmentService(DepartmentsContext context)
        {
            _context = context;
        }

        public List<Department> FindAllDepartment()
        {
            return _context.Department.OrderBy(x => x.Nome).ToList();
        }
    }
}