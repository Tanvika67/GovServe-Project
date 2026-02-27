using GovServe_Project.DTOs.OfficerDTO;
using GovServe_Project.Models;
using GovServe_Project.Models.CitizenModels;
using GovServe_Project.Models.SuperModels;
namespace GovServe_Project.Repository.Interface.CitizenRepository_Interface
{
	public interface IApplicationRepository
	{
	
		Task CreateAsync(Application application);

		Task<Application> GetByIdAsync(int ApplicationId);

		Task<List<Application>> GetByUserIdAsync(int userId);

		Task DeleteAsync(Application ApplicationId);

		Task<List<Case>> GetAssignedCases(int officerId);

		Task<List<Case>> GetApprovedCases(int officerId);

		Task<List<Case>> GetPendingCases(int officerId);

		Task<List<Case>> GetRejectedCases(int officerId);

		Task<List<Case>> GetResubmittedCases(int officerId);

		Task<Case?> GetCaseById(int CaseId);

		Task UpdateCase(Case casedata);

		//Task DeleteAsync(Application application);
		Task<ApplicationDetails> GetApplicationDetails(int ApplicationId);
	}

}
