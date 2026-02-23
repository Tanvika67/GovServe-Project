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
		public GovServe_ProjectContext(DbContextOptions<GovServe_ProjectContext> options)
			: base(options)
		{
		}


		public DbSet<GovServe_Project.Models.Case> Case { get; set; }
		public DbSet<GovServe_Project.Models.Department> Department { get; set; }
		public DbSet<GovServe_Project.Models.Escalation> Escalation { get; set; }
		public DbSet<GovServe_Project.Models.Notification> Notification { get; set; }
		public DbSet<GovServe_Project.Models.User> User { get; set; }
	

	}
}
