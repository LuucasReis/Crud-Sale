using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AppVendas.Models;

    public class DepartmentsContext : DbContext
    {
        public DepartmentsContext (DbContextOptions<DepartmentsContext> options)
            : base(options)
        {
        }

        public DbSet<AppVendas.Models.Department>? Department { get; set; }
    }
