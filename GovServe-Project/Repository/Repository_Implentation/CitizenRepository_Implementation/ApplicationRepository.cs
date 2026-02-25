using System;
using GovServe_Project.Data;
using GovServe_Project.Models;
using GovServe_Project.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using GovServe_Project.Models.CitizenModels;
using GovServe_Project.Repository.Interface.CitizenRepository_Interface;

namespace GovServe_Project.Repository.Repository_Implentation.CitizenRepository_Implementation
{
	public class ApplicationRepository : IApplicationRepository
	{
		private readonly GovServe_ProjectContext _context;

		public ApplicationRepository(GovServe_ProjectContext context)
		{
			_context = context;
		}

	
		// Create Application
		public async Task CreateAsync(Application application)
		{
			await _context.Application.AddAsync(application);
			await _context.SaveChangesAsync();
		}


		// Get By Id
		public async Task<Application> GetByIdAsync(int ApplicationId)
		{
			return await _context.Application.FindAsync(ApplicationId);
		}

		// Get By UserId
		public async Task<List<Application>> GetByUserIdAsync(int userId)
		{
			return await _context.Application
								 .Where(a => a.UserId == userId)
								 .ToListAsync();
		}

		// Delete Application
		public async Task DeleteAsync(Application ApplicationId)
		{
			_context.Application.Remove(ApplicationId);
			await _context.SaveChangesAsync();
		}
	}
}
