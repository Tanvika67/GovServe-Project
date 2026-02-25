

using GovServe_Project.Models;
using GovServe_Project.Models.AdminModels;
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

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Application → User
			modelBuilder.Entity<Application>()
				.HasOne(a => a.User)
				.WithMany()
				.HasForeignKey(a => a.UserId)
				.OnDelete(DeleteBehavior.Restrict);

			// Application → Service
			modelBuilder.Entity<Application>()
				.HasOne(a => a.Service)
				.WithMany()
				.HasForeignKey(a => a.ServiceID)
				.OnDelete(DeleteBehavior.Restrict);

			// Application → Department
			modelBuilder.Entity<Application>()
				.HasOne(a => a.Department)
				.WithMany()
				.HasForeignKey(a => a.DepartmentID)
				.OnDelete(DeleteBehavior.Restrict);

			// RequiredDocument → Service
			modelBuilder.Entity<RequiredDocument>()
				.HasOne(r => r.Service)
				.WithMany()
				.HasForeignKey(r => r.ServiceID)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Case>()
	             .HasOne(c => c.Application)
	             .WithMany()
	            .HasForeignKey(c => c.ApplicationId)
	            .OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Case>()
				.HasOne(c => c.AssignedOfficer)
				.WithMany()
				.HasForeignKey(c => c.AssignedOfficerId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Escalation>()
                 .HasOne(e => e.Case)
	             .WithMany()
	             .HasForeignKey(e => e.CaseId)
	             .OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Escalation>()
				.HasOne(e => e.EscalatedByUser)
				.WithMany()
				.HasForeignKey(e => e.EscalatedByUserId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Notification>()
	            .HasOne(n => n.User)
	            .WithMany()
	            .HasForeignKey(n => n.UserId)
	            .OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Notification>()
				.HasOne(n => n.Case)
				.WithMany()
				.HasForeignKey(n => n.CaseId)
				.OnDelete(DeleteBehavior.Restrict);
		}

		public DbSet<Case> Case { get; set; } = default!;
		public DbSet<Notification> Notification { get; set; } = default!;
		public DbSet<Department> Departments { get; set; } = default!;
        public DbSet<Service> Services { get; set; } = default!;
        public DbSet<EligibilityRule> EligibilityRules { get; set; } = default!;
        public DbSet<RequiredDocument> RequiredDocuments { get; set; } = default!;
        public DbSet<WorkflowStage> WorkflowStages { get; set; } = default!;
        //public DbSet<SLARecord> SLARecords { get; set; } = default!;
        public DbSet<ServiceReport> ServiceReports { get; set; } = default!;
        public DbSet<Application> Application { get; set; } = default!;
        public DbSet<CitizenDocument> CitizenDocument { get; set; } = default!;

        public DbSet<Users> User { get; set; } = default!;
     

        internal async Task SaveChaangesAsync(Service service)
        {
            throw new NotImplementedException();
        }
    }
}

