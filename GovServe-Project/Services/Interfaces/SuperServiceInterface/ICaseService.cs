using GovServe_Project.Models;
using GovServe_Project.DTOs.OfficerDTO;

namespace GovServe_Project.Services.Interfaces
{
	using GovServe_Project.DTOs.SupervisorDTO;
	using GovServe_Project.Models;

	public interface ICaseService
	{
		Task<IEnumerable<Case>> GetAllCasesAsync();
		Task<IEnumerable<Case>> GetPendingCasesAsync();
		Task<IEnumerable<Case>> GetActiveCasesAsync();
		Task<IEnumerable<Case>> GetEscalatedCasesAsync();
		Task<object> GetDashboardAsync();

		Task<string> CreateCaseAsync(CreateCaseDto dto);
		Task<string> AssignCaseAsync(int caseId, int officerId, int officerDeptId);
		Task<string> ReassignCaseAsync(int caseId, int newOfficerId);
		Task<string> AutoEscalateAsync();

		Task<List<Case>> ViewAssignedCases(int officerId);

		Task<string> OpenCase(int caseId);
		Task<string> ApproveCase(int caseId);
		Task<string> Reject(int caseId, string reason);
		Task<DashboardCountcs> GetDashboardCountsAsync(int departmentId);


	}
}