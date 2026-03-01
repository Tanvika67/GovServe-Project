using GovServe_Project.Models.SuperModels;

namespace GovServe_Project.Services.Interfaces.SuperServiceInterface
{
	public interface IEscalationService
	{
		Task<string> CheckSLAAndEscalateAsync(int caseId);
		Task SendSLAWarningsAsync();
		Task<string> AutoEscalateAsync();
		Task<int> GetEscalationCountAsync();
	}
}