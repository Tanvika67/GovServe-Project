using GovServe_Project.Data;
using GovServe_Project.Enum;
using GovServe_Project.Models;
using GovServe_Project.Models.GrievanceAppealModel;
using GovServe_Project.Repository.Interface;
using Microsoft.EntityFrameworkCore;
namespace GovServe_Project.Repository.Repository_Implentation.GrievanceAppealRepository_implementation
{
	public class AppealRepository : IAppealRepository
	{
		private readonly GovServe_ProjectContext _context;

		// Constructor injection of DbContext
		public AppealRepository(GovServe_ProjectContext context)
		{
			_context = context;
		}

		// Add appeal
		public async Task AddAsync(Appeal appeal)
		{
			await _context.Appeals.AddAsync(appeal);
			await _context.SaveChangesAsync();
		}

		// Get appeals by User ID
		public async Task<List<Appeal>> GetByUserAsync(int userId)
		{
			return await _context.Appeals
				.Where(x => x.UserId == userId)
				.ToListAsync();
		}

		// Get appeal by ID
		public async Task<Appeal?> GetByIdAsync(int id)
		{
			return await _context.Appeals.FindAsync(id);
		}


		// Get Appeals by Status (Officer Dashboard)

		public async Task<List<Appeal>> GetByStatusAsync(AppealStatus status)
		{
			return await _context.Appeals
				.Where(a => a.Status == status)
				.ToListAsync();
		}


		// Update Appeal (Approve/Reject)

		public async Task UpdateAsync(Appeal appeal)
		{
			_context.Appeals.Update(appeal);
			await _context.SaveChangesAsync();
		}

		public async Task<int> GetPendingAppealCountAsync()
		{
			return await _context.Appeals
				.Where(a => a.Status == AppealStatus.Submitted)
				.CountAsync();
		}
		public async Task<int> GetResolvedAppealCountAsync()
		{
			return await _context.Appeals
				.Where(a => a.Status == AppealStatus.Approved || a.Status == AppealStatus.Rejected)
				.CountAsync();
		}
	}
}
