using GovServe_Project.Data;
using GovServe_Project.DTOs.CitizenDTO;
using GovServe_Project.DTOs.CitizenDTO;
using GovServe_Project.DTOs.OfficerDTO;
using GovServe_Project.Models.SuperModels;
using GovServe_Project.Models.CitizenModels;
using GovServe_Project.Models.SuperModels;
using GovServe_Project.Repository.Interface.CitizenRepository_Interface;
using GovServe_Project.Services.Interfaces.CitizenService_Interface;
using Microsoft.EntityFrameworkCore;

namespace GovServe_Project.Services.Service_Implementation.CitizenService_Implementation
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
			var service = await _context.Services
				.FirstOrDefaultAsync(s => s.ServiceName == dto.ServiceName);

			if (service == null)
				throw new Exception("Service not found");

			//  Step 2: Create Application
			var app = new Application()
			{
				ServiceID = service.ServiceID,   
				UserId = dto.UserId,
				ServiceName = dto.ServiceName,
				DepartmentID = dto.DepartmentID,	
				ApplicationStatus = "Submitted",
				SubmittedDate = DateTime.Now,
				CompletedDate =null
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
				ApplicationId = a.ApplicationID,	
				UserId = a.UserId,
				ServiceName = a.Service.ServiceName,
				ServiceID = a.ServiceID,
				ApplicationStatus = a.ApplicationStatus,
				SubmittedDate = a.SubmittedDate
			}).ToList();

			return result;
		}
		//get all application
        public async Task<List<ApplicationResponseDTO>> GetAllApplicationsAsync()
        {
            var applications = await _applicationRepository.GetAllAsync();

            var result = applications.Select(a => new ApplicationResponseDTO
            {
                ApplicationId = a.ApplicationID,
                UserId = a.UserId,
                ServiceID = a.ServiceID,
                ServiceName = a.Service.ServiceName,
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


		//Update Application
		public async Task<bool> UpdateApplicationAsync(int id, Application application)
		{
			var existingApplication = await _applicationRepository.GetByIdAsync(id);

			if (existingApplication == null)
			{
				return false;
			}


			existingApplication.ApplicationStatus = application.ApplicationStatus;

			await _applicationRepository.UpdateAsync(existingApplication);
			return true;
		}



		public Task DeleteApplication(int id)
		{
			throw new NotImplementedException();
		}
		
		//for officer to view application details

		public async Task<ApplicationDetails> GetApplicationDetails(int applicationId)
		{
			return await
				_applicationRepository.GetApplicationDetails(applicationId);


		}
	}
}

