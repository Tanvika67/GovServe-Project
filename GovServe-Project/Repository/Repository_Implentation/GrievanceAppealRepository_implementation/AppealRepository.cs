using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GovServe.Models;
using GovServe_Project.Data;
using GovServe_Project.Enum;
using GovServe_Project.Models;
using GovServe_Project.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace GovServe_Project.Repository.Repository_Implentation
{
	public class AppealRepository : IAppealRepository
	{
		private readonly GovServe_ProjectContext _context;

		public AppealRepository(GovServe_ProjectContext context)
		{
			_context = context;
		}

		public async Task AddAsync(Appeal appeal)
		{
			await _context.Appeal.AddAsync(appeal);
			await _context.SaveChangesAsync();
		}

		public async Task<Appeal> GetByIdAsync(int id)
		{
			return await _context.Appeal
				.FirstOrDefaultAsync(x => x.AppealID == id);
		}

		public async Task<List<Appeal>> GetByCitizenIdAsync(int citizenId)
		{
			return await _context.Appeal
				.Where(x => x.UserId == citizenId)
				.ToListAsync();
		}

		public async Task<List<Appeal>> GetAllAsync()
		{
			return await _context.Appeal.ToListAsync();
		}

		// Active = Open
		public async Task<int> GetActiveCountAsync()
		{
			return await _context.Appeal.CountAsync(x =>
				x.Status == AppealStatus.Open);
		}

		// Inactive = Approved + Rejected
		public async Task<int> GetInactiveCountAsync()
		{
			return await _context.Appeal.CountAsync(x =>
				x.Status == AppealStatus.Approved ||
				x.Status == AppealStatus.Rejected);
		}

		public async Task UpdateAsync(Appeal appeal)
		{
			_context.Appeal.Update(appeal);
			await _context.SaveChangesAsync();
		}
	}

}
