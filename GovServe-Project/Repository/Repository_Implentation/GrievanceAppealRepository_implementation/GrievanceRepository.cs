using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GovServe.Models;
using GovServe_Project.Data;
using GovServe_Project.Enum;
using GovServe_Project.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace GovServe_Project.Repository.Repository_Implentation
{
	public class GrievanceRepository : IGrievanceRepository
	{
		private readonly GovServe_ProjectContext _context;

		// DbContext injected using Dependency Injection
		public GrievanceRepository(GovServe_ProjectContext context)
		{
			_context = context;
		}

		// Insert grievance into DB
		public async Task AddAsync(Grievance grievance)
		{
			await _context.Grievance.AddAsync(grievance);
			await _context.SaveChangesAsync();
		}

		// Get grievance by ID
		public async Task<Grievance> GetByIdAsync(int id)
		{
			return await _context.Grievance
				.FirstOrDefaultAsync(x => x.GrievanceId == id);
		}

		// Citizen dashboard - get only his grievances
		public async Task<List<Grievance>> GetByCitizenIdAsync(int citizenId)
		{
			return await _context.Grievance
				.Where(x => x.UserId == citizenId)
				.ToListAsync();
		}

		// Officer pending grievances (Open status)
		public async Task<List<Grievance>> GetPendingAsync()
		{
			return await _context.Grievance
				.Where(x => x.Status == GrievanceStatus.Open)
				.ToListAsync();
		}

		// Supervisor forwarded grievances
		public async Task<List<Grievance>> GetForwardedAsync()
		{
			return await _context.Grievance
				.Where(x => x.Status == GrievanceStatus.ForwardedToSupervisor)
				.ToListAsync();
		}

		// Admin dashboard list
		public async Task<List<Grievance>> GetAllAsync()
		{
			return await _context.Grievance.ToListAsync();
		}

		// Active = Open + Forwarded
		public async Task<int> GetActiveCountAsync()
		{
			return await _context.Grievance.CountAsync(x =>
				x.Status == GrievanceStatus.Open ||
				x.Status == GrievanceStatus.ForwardedToSupervisor);
		}

		// Inactive = Resolved + Rejected
		public async Task<int> GetInactiveCountAsync()
		{
			return await _context.Grievance.CountAsync(x =>
				x.Status == GrievanceStatus.Resolved ||
				x.Status == GrievanceStatus.Rejected);
		}

		// Update grievance record
		public async Task UpdateAsync(Grievance grievance)
		{
			_context.Grievance.Update(grievance);
			await _context.SaveChangesAsync();
		}
	}
}