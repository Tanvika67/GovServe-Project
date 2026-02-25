using Microsoft.EntityFrameworkCore;
using GovServe_Project.Data;
using GovServe_Project.Repository.Interface;
using GovServe_Project.Models;
using GovServe_Project.Services.Interfaces;
using GovServe_Project.DTOs;
using GovServe_Project.Services.Interfaces.CitizenService_Interface;
using GovServe_Project.Repository.Interface.CitizenRepository_Interface;
using GovServe_Project.DTOs.CitizenDTO;
using GovServe_Project.Models.CitizenModels;
using GovServe_Project.DTOs.CitizenDTO;

namespace GovServe_Project.Services.Service_Implementation.CitizenService_Implementation
{
	public class ApplicationService : IApplicationService
	{
		private readonly IApplicationRepository _applicationRepository;

		public ApplicationService(IApplicationRepository applicationRepository)
		{
			_applicationRepository = applicationRepository;
		}

		
		// Create Application
		public async Task<string> CreateApplicationAsync(CreateApplicationDTO dto)
		{
			// DTO → Entity Mapping
			var app = new Application()
			{
				ServiceID = dto.ServiceID,
				ServiceName = dto.ServiceName,
				Description = dto.Description,
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

			// Entity → DTO mapping
			var result = applications.Select(a => new ApplicationResponseDTO
			{
				UserId = a.UserId,
				ServiceID = a.ServiceID,
				ServiceName = a.ServiceName,
				ApplicationStatus = a.ApplicationStatus,
				SubmittedDate = a.SubmittedDate
			}).ToList();

			return result;
		}

		// Application Status
		public async Task<string> GetApplicationStatusAsync(int ApplicationId)
		{
			var application = await _applicationRepository.GetByIdAsync(ApplicationId);

			if (application == null)
				return null;

			return application.ApplicationStatus;
		}

		// Delete Application
		public async Task<bool> DeleteApplicationAsync(int ApplicationId)
		{
			var application = await _applicationRepository.GetByIdAsync(ApplicationId);

			if (application == null)
				return false;

			await _applicationRepository.DeleteAsync(application);

			return true;
		}
	}
}
