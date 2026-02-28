using GovServe_Project.Data;
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

		// Get appeals by Application ID
		public async Task<List<Appeal>> GetByApplicationAsync(int applicationId)
		{
			return await _context.Appeals
				.Where(x => x.ApplicationID == applicationId)
				.ToListAsync();
		}

		// Get appeal by ID
		public async Task<Appeal?> GetByIdAsync(int id)
		{
			return await _context.Appeals.FindAsync(id);
		}
	}
}
