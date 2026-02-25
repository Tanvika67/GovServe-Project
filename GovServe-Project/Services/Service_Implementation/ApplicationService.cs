using GovServe_Project.Data;
using GovServe_Project.DTOs;
using GovServe_Project.Models;
using GovServe_Project.Repository.Interface;
using GovServe_Project.Repository.Repository_Implentation;
using GovServe_Project.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GovServe_Project.Services.Service_Implementation
{
	public class ApplicationService : IApplicationService
	{
		private readonly GovServe_ProjectContext _context;
		private readonly IApplicationRepository _applicationRepository;

		public ApplicationService(IApplicationRepository applicationRepository,GovServe_ProjectContext context)
		{
			_applicationRepository = applicationRepository;
			_context = context;
		}

		// Create Application
		public async Task<string> CreateApplicationAsync(CreateApplicationDTO dto)
		{
			//  Step 1: Get service from DB using name
			var service = await _context.Services
				.FirstOrDefaultAsync(s => s.ServiceName == dto.ServiceName);

			if (service == null)
				throw new Exception("Service not found");

			//  Step 2: Create Application
			var app = new Application()
			{
				ServiceID = service.ServiceID,   // ✅ now works
				Description = dto.Description,
				UserId = dto.UserId,
				ApplicationStatus = "Submitted",
				SubmittedDate = DateTime.Now
			};

			await _applicationRepository.CreateAsync(app);

			return "Application Submitted Successfully";
		}

		// My Applications
		public async Task<List<ApplicationResponseDTO>> GetMyApplicationsAsync(int userId)
		{
			
			var applications = await _applicationRepository.GetByUserIdAsync(userId);

			
			var result = applications.Select(a => new ApplicationResponseDTO
			{
				UserId = a.UserId,
				ServiceName = a.Service.ServiceName,
				ApplicationStatus = a.ApplicationStatus,
			    SubmittedDate = a.SubmittedDate
			}).ToList();

			return result;
		}

		
		// Application Status
		public async Task<string> GetApplicationStatusAsync(int applicationId)
		{
			var application = await _applicationRepository.GetByIdAsync(applicationId);

			if (application == null)
				return null;

			return application.ApplicationStatus;
		}

		
		// Delete Application
		public async Task<bool> DeleteApplicationAsync(int applicationId)
		{
			var application = await _applicationRepository.GetByIdAsync(applicationId);

			if (application == null)
				return false;

			await _applicationRepository.DeleteAsync(application);

			return true;
		}
	}
}
