
using GovServe_Project.Models;
using GovServe_Project.Models.AdminModels;
using GovServe_Project.Models.SuperModels;
using Microsoft.EntityFrameworkCore;
using GovServe_Project.Models.CitizenModels;


namespace GovServe_Project.Data
{
    public class GovServe_ProjectContext : DbContext
    {
        public GovServe_ProjectContext (DbContextOptions<GovServe_ProjectContext> options)
            : base(options)
        {
        }

        public DbSet<Case> Case { get; set; } = default!;
        public DbSet<Department> Departments { get; set; } = default!;
        public DbSet<Service> Services { get; set; } = default!;
        public DbSet<EligibilityRule> EligibilityRules { get; set; } = default!;
        public DbSet<RequiredDocument> RequiredDocuments { get; set; } = default!;
        public DbSet<WorkflowStage> WorkflowStages { get; set; } = default!;
      
        public DbSet<SLADays> SLADays { get; set; } = default!;
        public DbSet<SLARecord> SLARecords { get; set; } = default!;
        public DbSet<Role> Roles { get; set; } = default!;
        public DbSet<ServiceReport> ServiceReports { get; set; } = default!;
        public DbSet<Application> Application { get; set; } = default!;
        public DbSet<CitizenDocument> CitizenDocument { get; set; } = default!;
		
        public DbSet<Users> User { get; set; } = default!;
        public DbSet<Escalation> Escalation { get; set; } = default!;
        public DbSet<Notification> Notification { get; set; } = default!;
       // public DbSet<Grievance> Grievance { get; set; } = default!;

      

		//public DbSet<Appeal> Appeal { get; set; } = default!;
		



		//public DbSet<Appeal> Appeal { get; set; } = default!;
		

        internal async Task SaveChaangesAsync(Service service)
        {
            throw new NotImplementedException();
        }



    }
}

