using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GovServe.Models;
using GovServe_Project.Models.AdminModels;
using GovServe_Project.Models;

namespace GovServe_Project.Data
{
	public class GovServe_ProjectContext : DbContext
	{
		public GovServe_ProjectContext(DbContextOptions<GovServe_ProjectContext> options)
			: base(options)
		{
		}


		public DbSet<GovServe_Project.Models.Case> Case { get; set; }
		public DbSet<Department> Departments { get; set; }
		public DbSet<Escalation> Escalation { get; set; }
		public DbSet<Notification> Notification { get; set; }
		public DbSet<User> User { get; set; }

		public DbSet<Application>Application { get; set; }
		public DbSet<CitizenDocument> CitizenDocument {  get; set; }
		public DbSet<Service> Services { get; set; }
		public DbSet<EligibilityRule> EligibilityRules { get; set; }
		public DbSet<RequiredDocument> RequiredDocuments { get; set; }
		public DbSet<WorkflowStage> WorkflowStages { get; set; }
		public DbSet<SLARecord> SLARecords { get; set; }
		public DbSet<ServiceReport> ServiceReports { get; set; }

		internal async Task SaveChaangesAsync(Service service)
		{
			throw new NotImplementedException();
		}


	}
}
