using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GovServe.Models;
using GovServe_Project.Models;

namespace GovServe_Project.Data
{
    public class GovServe_ProjectContext : DbContext
    {
        public GovServe_ProjectContext (DbContextOptions<GovServe_ProjectContext> options)
            : base(options)
        {
        }

        public DbSet<GovServe.Models.Case> Case { get; set; } = default!;
        public DbSet<GovServe_Project.Models.Department> Department { get; set; } = default!;
        public DbSet<GovServe_Project.Models.Service> Service { get; set; } = default!;
        public DbSet<GovServe_Project.Models.Application> Application { get; set; } = default!;
        public DbSet<GovServe_Project.Models.RequiredDocument> RequiredDocument { get; set; } = default!;
        public DbSet<GovServe_Project.Models.CitizenDocument> CitizenDocument { get; set; } = default!;
        public DbSet<GovServe_Project.Models.User> User { get; set; } = default!;

    }
}
