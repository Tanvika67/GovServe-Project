using Microsoft.EntityFrameworkCore;
using GovServe_Project.Data;
using GovServe_Project.Models;
using GovServe_Project.Repository.Interface;
namespace GovServe_Project.Repository.Repository_Implentation
{
	public class ApplicationRepository : IApplicationRepository
	{
		private readonly GovServe_ProjectContext _context;

		public ApplicationRepository(GovServe_ProjectContext context)
		{
			_context = context;
		}

		// Create Application
		public async Task AddApplication(Application app)
		{
			await _context.Application.AddAsync(app);
			await _context.SaveChangesAsync();
		}

		// My Applications
		public async Task<List<Application>> GetByUser(int userId)
		{
			return await _context.Application
				.Where(x => x.UserId == userId)
				.ToListAsync();
		}

		// Application Details / Status
		public async Task<Application> GetById(int id)
		{
			return await _context.Application.FindAsync(id);
		}

		// Delete Application
		public async Task Delete(Application app)
		{
			_context.Application.Remove(app);
			await _context.SaveChangesAsync();
		}
	}
}
