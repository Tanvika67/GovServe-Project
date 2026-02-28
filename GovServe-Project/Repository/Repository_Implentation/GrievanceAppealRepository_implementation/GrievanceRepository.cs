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
			return await _context.Grievance.FindAsync(id);
		}
	}
}