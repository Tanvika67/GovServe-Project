using GovServe_Project.Data;
using GovServe_Project.Models;
using GovServe_Project.Models.GrievanceAppealModel;
using GovServe_Project.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace GovServe_Project.Repository.Repository_Implentation.GrievanceAppealRepository_implementation
{
	// Repository class for handling database operations for Grievance
	public class GrievanceRepository : IGrievanceRepository
	{
		private readonly GovServe_ProjectContext _context;

		// Constructor injection of DbContext
		public GrievanceRepository(GovServe_ProjectContext context)
		{
			_context = context;
		}

		// Add grievance to database
		public async Task AddAsync(Grievance grievance)
		{
			await _context.Grievance.AddAsync(grievance);
			await _context.SaveChangesAsync();
		}

		// Get grievances for specific citizen
		public async Task<List<Grievance>> GetByCitizenAsync(int citizenId)
		{
			return await _context.Grievance
				.Where(x => x.UserId == citizenId)
				.ToListAsync();
		}

		// Get grievance by ID
		public async Task<Grievance?> GetByIdAsync(int id)
		{
			return await _context.Grievance.
				Include(g => g.Application)
				.FirstOrDefaultAsync(g => g.GrievanceId == id);

		}



		// Officer: Get All Grievances

		public async Task<List<Grievance>> GetAllAsync()
		{
			return await _context.Grievance.ToListAsync();
		}


		// Officer: Update Grievance

		public async Task UpdateAsync(Grievance grievance)
		{
			_context.Grievance.Update(grievance);
			await _context.SaveChangesAsync();
		}

		public async Task<int> GetPendingGrievanceCountAsync()
		{
			return await _context.Grievance
				.Where(g => g.Status == Enum.GrievanceStatus.Submitted || g.Status == Enum.GrievanceStatus.ForwardedToSupervisor)
				.CountAsync();
		}
		public async Task<int> GetResolvedGrievanceCountAsync()
		{
			return await _context.Grievance
				.Where(g => g.Status == Enum.GrievanceStatus.Resolved || g.Status == Enum.GrievanceStatus.Rejected)
				.CountAsync();

		}
	}
}