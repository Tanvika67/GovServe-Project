using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GovServe.Models;

namespace GovServe_Project.Data
{
    public class GovServe_ProjectContext : DbContext
    {
        public GovServe_ProjectContext (DbContextOptions<GovServe_ProjectContext> options)
            : base(options)
        {
        }

        public DbSet<GovServe.Models.Case> Case { get; set; } = default!;
    }
}
