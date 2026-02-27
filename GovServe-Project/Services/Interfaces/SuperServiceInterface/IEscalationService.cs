using GovServe_Project.Models.SuperModels;

namespace GovServe_Project.Services.Interfaces.SuperServiceInterface
{
	public interface IEscalationService
	{
		Task<string> EscalateCaseAsync(int caseId, int newOfficerId, int supervisorId, string reason);
		Task<string> CheckSLAAndEscalateAsync(int caseId);
		Task<string> AutoEscalateAsync();
		Task<int> GetEscalationCountAsync();
	}
}