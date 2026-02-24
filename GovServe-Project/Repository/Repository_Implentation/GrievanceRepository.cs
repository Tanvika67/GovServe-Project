using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GovServe.Models;
using GovServe_Project.Data;
using GovServe_Project.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace GovServe_Project.Repository.Repository_Implentation
{
	// Repository implementation for Grievance
	public class GrievanceRepository : IGrievanceRepository
	{
		private readonly GovServe_ProjectContext _context;

		public GrievanceRepository(GovServe_ProjectContext context) => _context = context;

		// Add new grievance to database
		public async Task AddAsync(Grievance grievance)
		{
			await _context.Grievance.AddAsync(grievance);
			await _context.SaveChangesAsync();
		}

		// Fetch all grievances with related appeals
		public async Task<List<Grievance>> GetAllAsync()
		{
			return await _context.Grievance.Include(g => g.Appeals).ToListAsync();
		}

		// Fetch single grievance by ID
		public async Task<Grievance> GetByIdAsync(int id)
		{
			return await _context.Grievance.Include(g => g.Appeals)
											.FirstOrDefaultAsync(g => g.GrievanceID == id);
		}

		// Update grievance in database
		public async Task UpdateAsync(Grievance grievance)
		{
			_context.Grievance.Update(grievance);
			await _context.SaveChangesAsync();
		}
	}
}
