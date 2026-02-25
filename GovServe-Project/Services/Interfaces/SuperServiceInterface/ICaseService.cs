using GovServe_Project.Models;

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
		
	}
}