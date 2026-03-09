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

			//   Create Application
			var app = new Application()
			{
				ServiceID = service.ServiceID,   
				UserId = dto.UserId,
				ServiceName = dto.ServiceName,
				DepartmentID = dto.DepartmentID,	
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
				ServiceID = a.ServiceID,
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

		//Approved Application

		public async Task<List<Case>>
			GetAssignedCase(int officerId)
		{
			return await _applicationRepository.GetAssignedCases(officerId);
		}

		public async Task<List<Case>>
			GetApprovedCase(int officerId)
		{
			return await _applicationRepository.GetApprovedCases(officerId);
		}

		public async Task<List<Case>>
			GetPendingCase(int officerId)
		{
			return await _applicationRepository.GetPendingCases(officerId);
		}

		public async Task<List<Case>>
			GetRejectedCase(int officerId)
		{
			return await _applicationRepository.GetRejectedCases(officerId);
		}

		public async Task<List<Case>>
			GetResubmittedCase(int officerId)
		{
			return await _applicationRepository.GetResubmittedCases(officerId);
		}

		public Task<List<Case>> GetAssignedCases(int officerId)
		{
			return _applicationRepository.GetAssignedCases(officerId);
		}

		public Task<List<Case>> GetApprovedCases(int officerId)
		{
			return _applicationRepository.GetApprovedCases(officerId);
		}

		public Task<List<Case>> GetPendingCases(int officerId)
		{
			return _applicationRepository.GetPendingCases(officerId);
		}

		public Task<List<Case>> GetRejectedCases(int officerId)
		{
			return _applicationRepository.GetRejectedCases(officerId);
		}

		public Task<List<Case>> GetResubmittedCases(int officerId)
		{
			return _applicationRepository.GetResubmittedCases(officerId);
		}

		public async Task<bool> ApprovedCase(int CaseId, int officerId)
		{
			var casedata = await _applicationRepository.GetCaseById(CaseId);
			if (casedata == null || casedata.AssignedOfficerId != officerId)
				return false;

			casedata.Status = "Approved";
			casedata.CompletedDate = DateTime.Now;
			casedata.RejectionReason = null;

			await _applicationRepository.UpdateCase(casedata);
			return true;
		}

		public async Task<bool> RejectCase(int CaseId, int officerId, string reason)
		{

			var casedata = await _applicationRepository.GetCaseById(CaseId);
			if (casedata == null || casedata.AssignedOfficerId != officerId)
				return false;

			casedata.Status = "Rejected";
			casedata.RejectionReason = reason;
			casedata.CompletedDate = DateTime.Now;

			await _applicationRepository.UpdateCase(casedata);
			return true;
		}

		public Task<bool> GetApproveCase(int CaseId, int officerId)
		{
			return ApprovedCase(CaseId, officerId);
		}

		public Task<Case> GetCaseById(int CaseId)
		{
			return _applicationRepository.GetCaseById(CaseId);
		}

		public async Task<Case> UpdateCase(Case casedata)
		{
			await _applicationRepository.UpdateCase(casedata);
			return casedata;
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

