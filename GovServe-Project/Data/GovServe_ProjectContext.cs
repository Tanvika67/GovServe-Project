using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GovServe.Models;
using GovServe_Project.Models.AdminModels;

namespace GovServe_Project.Data
{
    public class GovServe_ProjectContext : DbContext
    {
        public GovServe_ProjectContext (DbContextOptions<GovServe_ProjectContext> options)
            : base(options)
        {
        }

        public DbSet<GovServe.Models.Case> Case { get; set; } = default!;
        public DbSet<Department> Departments { get; set; } = default!;
        public DbSet<Service> Services { get; set; } = default!;
        public DbSet<EligibilityRule> EligibilityRules { get; set; } = default!;
        public DbSet<RequiredDocument> RequiredDocuments { get; set; } = default!;
        public DbSet<WorkflowStage> WorkflowStages { get; set; } = default!;
        public DbSet<SLARecord> SLARecords{ get; set; } = default!;

        internal async Task SaveChaangesAsync(Service service)
        {
            throw new NotImplementedException();
        }
    }
}
