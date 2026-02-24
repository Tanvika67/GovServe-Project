using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GovServe.Models;
using GovServe_Project.Data;
using GovServe_Project.Models;
using GovServe_Project.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace GovServe_Project.Repository.Repository_Implentation
{
	// Repository implementation for Appeal
	public class AppealRepository : IAppealRepository
	{
		private readonly GovServe_ProjectContext _context;

		public AppealRepository(GovServe_ProjectContext context) => _context = context;

		// Add new appeal
		public async Task AddAsync(Appeal appeal)
		{
			await _context.Appeal.AddAsync(appeal);
			await _context.SaveChangesAsync();
		}

		// Fetch all appeals with linked grievance
		public async Task<List<Appeal>> GetAllAsync()
		{
			return await _context.Appeal.Include(a => a.Grievance).ToListAsync();
		}

		// Fetch appeal by ID
		public async Task<Appeal> GetByIdAsync(int id)
		{
			return await _context.Appeal.Include(a => a.Grievance)
										 .FirstOrDefaultAsync(a => a.AppealID == id);
		}

		// Update appeal in database
		public async Task UpdateAsync(Appeal appeal)
		{
			_context.Appeal.Update(appeal);
			await _context.SaveChangesAsync();
		}
	}
}
