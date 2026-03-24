using GovServe_Project.Models.SuperModels;
using GovServe_Project.DTOs.SupervisorDTO;
using GovServe_Project.Models;
using GovServe_Project.DTOs.OfficerDTO;

namespace GovServe_Project.Services.Interfaces
{
	public interface ICaseService
	{
		Task<string> CreateCaseAsync(CreateCaseDto dto);
		Task<IEnumerable<Case>> GetAllCasesAsync();
		Task<Case> GetCaseDetails(int caseId);
		Task<IEnumerable<Case>> GetActiveCasesAsync();
		Task<List<Case>> GetSLABreachedCasesAsync();
		Task<object> GetDashboardAsync();
		Task<string> ReassignCaseAsync(int caseId, int newOfficerId);
	    Task<string> ReassignEscalatedCaseAsync(int caseId, int newOfficerId);
		Task<List<OfficerStatisticsDto>> GetOfficerStatisticsAsync();
		Task<DashboardStatsDto> GetDashboardStatsAsync();
		Task<string> UpdateCaseStatus(int caseId, string status);

		

		// Officer/assigned cases
		//Task<List<Case>> ViewAssignedCases(int officerId);

		//Task<string> ApproveCase(int caseId);
		//Task<string> Reject(int caseId, string reason);
		//Task<List<Case>> GetResubmittedCases(int AssignedOfficerId);
		//Task<DashboardCountcs> GetDashboardCountsAsync(int departmentId);
		//Task OpenCase(int caseId);


		Task<IEnumerable<Case>> GetAssignedCasesAsync(int officerId);
		Task<Case?> GetCaseByIdAsync(int caseId);
		Task<string> ApproveCaseAsync(int caseId);
		Task<string> RejectCaseAsync(int caseId, string reason);
		Task<IEnumerable<Case>> GetResubmittedCasesAsync(int officerId);
		Task<object> GetOfficerDashboardAsync(int officerId);



	}
}