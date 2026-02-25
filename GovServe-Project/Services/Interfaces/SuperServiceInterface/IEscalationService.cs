using GovServe_Project.Models;

namespace GovServe_Project.Services.Interfaces
{
	public interface IEscalationService
	{
		Task<string> EscalateCaseAsync(int caseId, int newOfficerId, int supervisorId, string reason);
		Task<int> GetEscalationCountAsync();
	}
}