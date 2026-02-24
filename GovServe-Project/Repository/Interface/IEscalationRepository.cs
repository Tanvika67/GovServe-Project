using GovServe_Project.Models;

namespace GovServe_Project.Repository.Interface
{
	public interface IEscalationRepository
	{
		Task CreateEscalationAsync(Escalation escalation);
		Task<int> GetCountAsync();
	}
}
