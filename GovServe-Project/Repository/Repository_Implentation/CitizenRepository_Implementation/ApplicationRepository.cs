using System;
using GovServe_Project.Data;
using GovServe_Project.DTOs.OfficerDTO;
using GovServe_Project.Models;
using GovServe_Project.Models.CitizenModels;
using GovServe_Project.Models.SuperModels;
using GovServe_Project.Repository.Interface;
using GovServe_Project.Repository.Interface.CitizenRepository_Interface;
using Microsoft.EntityFrameworkCore;

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

								  .Include(a => a.Service)
								  .Include(a=>a.Department)
								 .Where(a => a.UserId == userId)
								 .ToListAsync();

		}

		// Delete Application
		public async Task DeleteAsync(Application ApplicationId)
		{
			_context.Application.Remove(ApplicationId);
			await _context.SaveChangesAsync();
		}

		// for citizen to view application details with all related data (service, department, documents, etc.)
		public async Task<Application> GetApplicationWithAllDetailsAsync(int applicationId)
		{
			return await _context.Application
				.Include(a => a.Service)           
				.Include(a => a.Department)      
				.Include(a => a.CitizenDetails)    
				.Include(a => a.CitizenDocuments)  
				.ThenInclude(d => d.RequiredDocument)
				.FirstOrDefaultAsync(a => a.ApplicationID == applicationId);
		}


		//for officer to view application details

		public async Task<ApplicationDetails> GetApplicationDetails(int applicationId)

		{

			var data = await _context.Application


			.Where(a => a.ApplicationID == applicationId)

			.Select(a => new ApplicationDetails

			{
				ApplicationID = a.ApplicationID,

				ApplicationStatus = a.ApplicationStatus.ToString(),

				SubmittedDate = a.SubmittedDate
			})

			.FirstOrDefaultAsync();

			return data;

		}

		//---------------Supervisor methods-----------------
		//supervisor needs this
		public async Task<Application> GetApplicationWithDocuments(int applicationId)

		{

			return await _context.Application

			.Include(a => a.CitizenDocuments)

			.FirstOrDefaultAsync(a => a.ApplicationID == applicationId);
		}

		public async Task<List<Application>> GetAllAsync()

		{

			return await _context.Application

				.Include(a => a.Service)

				.Include(a => a.Department)

				.Include(a => a.User)

				.ToListAsync();

		}
	}
}