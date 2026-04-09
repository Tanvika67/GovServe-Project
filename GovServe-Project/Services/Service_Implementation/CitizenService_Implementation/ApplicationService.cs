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
using GovServe_Project.Controllers.CitizenController;
using GovServe_Project.DTOs;
namespace GovServe_Project.Services.Service_Implementation.CitizenService_Implementation
{
	public class ApplicationService : IApplicationService
	{
		private readonly GovServe_ProjectContext _context;
		private readonly IApplicationRepository _applicationRepository;

		public ApplicationService(IApplicationRepository applicationRepository, GovServe_ProjectContext context)
		{
			_applicationRepository = applicationRepository;
			_context = context;
		}

		// Create Application
		public async Task<Application> CreateApplicationAsync(CreateApplicationDTO dto)
		{
			var service = await _context.Services
				.FirstOrDefaultAsync(s => s.ServiceID == dto.ServiceID);

			if (service == null)
				throw new Exception("Service not found");

			var departmentExists = await _context.Departments
	            .AnyAsync(d => d.DepartmentID == dto.DepartmentID);

			if (!departmentExists)
				throw new Exception($"Department with ID {dto.DepartmentID} not found. Please check if the department exists in the database.");

			// Step 2: Create Application Object
			var app = new Application()
			{
				ServiceID = service.ServiceID,
				UserId = dto.UserId,
				DepartmentID = dto.DepartmentID,
				ApplicationStatus = "Submitted",
				SubmittedDate = DateTime.Now,
				CompletedDate = null
			};

			// Step 3: Save to DB (EF Core will automatically populate the app.ApplicationID)
			await _applicationRepository.CreateAsync(app);

			// Step 4: Return the object itself
			// This allows the Frontend to read 'app.applicationID'
			return app;
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
				ServiceID = a.ServiceID,
				DepartmentID=a.DepartmentID,
				ServiceName = a.Service?.ServiceName,
				DepartmentName = a.Department?.DepartmentName,
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


		public async Task<ApplicationDetailsDTO> GetApplicationDetailsAsync(int id)
		{
			var app = await _applicationRepository.GetApplicationWithAllDetailsAsync(id);
			if (app == null) return null;

			return new ApplicationDetailsDTO
			{
				ApplicationId = app.ApplicationID,
				ServiceName = app.Service?.ServiceName,
				DepartmentName = app.Department?.DepartmentName,
				ApplicationStatus = app.ApplicationStatus,
				SubmittedDate = app.SubmittedDate,

				// Section 2: Mapping Citizen Info
				CitizenInfo = app.CitizenDetails != null ? new CreateCitizenDetailsDTO
				{
					FullName = app.CitizenDetails.FullName,
					Gender = app.CitizenDetails.Gender,
					DateOfBirth = app.CitizenDetails.DateOfBirth,
					FatherName = app.CitizenDetails.FatherName,
					MotherName = app.CitizenDetails.MotherName,
					Email = app.CitizenDetails.Email,
					Phone = app.CitizenDetails.Phone,
					AadhaarNumber = app.CitizenDetails.AadhaarNumber,
					AddressLine1 = app.CitizenDetails.AddressLine1,
					AddressLine2 = app.CitizenDetails.AddressLine2,
					City = app.CitizenDetails.City,
					State = app.CitizenDetails.State,
					Pincode = app.CitizenDetails.Pincode
				} : null,

				// Section 3: Mapping Documents List
				Documents = app.CitizenDocuments?.Select(d => new CitizenDocumentViewDTO
				{
					DocumentName = d.RequiredDocument?.DocumentName ?? "Unknown Document",
					DocumentUrl=d.URI,
					UploadedDate = d.UploadedDate,
					VerificationStatus = d.VerificationStatus
				}).ToList() ?? new List<CitizenDocumentViewDTO>()
			};
		}

		//for officer to view application details

		public async Task<ApplicationDetails> GetApplicationDetails(int applicationId)
		{
			return await
				_applicationRepository.GetApplicationDetails(applicationId);


		}

		public async Task<List<ApplicationResponseDTO>> GetAllApplicationsAsync()

		{

			var applications = await _applicationRepository.GetAllAsync();


			var result = applications.Select(a => new ApplicationResponseDTO
			{
				ApplicationId = a.ApplicationID,

				UserId = a.UserId,

				ServiceID = a.ServiceID,


				ApplicationStatus = a.ApplicationStatus,

				SubmittedDate = a.SubmittedDate

			}).ToList();


			return result;

		}
	}
}