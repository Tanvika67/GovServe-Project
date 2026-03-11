using GovServe_Project.DTOs;
using GovServe_Project.DTOs.CitizenDTO;
using GovServe_Project.DTOs.OfficerDTO;
using GovServe_Project.Models;
using GovServe_Project.Models.SuperModels;

namespace GovServe_Project.Services.Interfaces.CitizenService_Interface
{
	public interface IApplicationService
	{
	
		Task<string> CreateApplicationAsync(CreateApplicationDTO dto);

	
		Task<List<ApplicationResponseDTO>> GetMyApplicationsAsync(int userId);

		
		Task<string> GetApplicationStatusAsync(int ApplicationId);
		
		Task<bool> DeleteApplicationAsync(int ApplicationId);

		Task<List<Case>> GetAssignedCases(int officerId);

		Task<List<Case>> GetApprovedCases(int officerId);

		Task<List<Case>> GetPendingCases(int officerId);

		Task<List<Case>> GetRejectedCases(int officerId);

		Task<List<Case>> GetResubmittedCases(int officerId);


		Task<bool> GetApproveCase(int CaseId, int officerId);

		Task<bool> RejectCase(int CaseId, int officerId, string reason);

		Task<Case> GetCaseById(int CaseId);

		Task<Case> UpdateCase(Case casedata);

		Task<ApplicationDetails> GetApplicationDetails(int applicationId);

	}
}
