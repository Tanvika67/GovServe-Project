using GovServe_Project.Models.SuperModels;
using GovServe_Project.DTOs.SupervisorDTO;
namespace GovServe_Project.Services.Interfaces
{
	public interface ICaseService
	{
		Task<IEnumerable<Case>> GetAllCasesAsync();
		Task<IEnumerable<Case>> GetPendingCasesAsync();
		Task<IEnumerable<Case>> GetActiveCasesAsync();
		Task<IEnumerable<Case>> GetEscalatedCasesAsync();
		Task<List<Case>> GetSLABreachedCasesAsync();
		Task<object> GetDashboardAsync();

		Task<string> CreateCaseAsync(CreateCaseDto dto);
		Task<string> AssignCaseAsync(int caseId, int officerId, int officerDeptId);
		Task<string> ReassignCaseAsync(int caseId, int newOfficerId);
		Task<string> AutoEscalateAsync();
		
	}
}