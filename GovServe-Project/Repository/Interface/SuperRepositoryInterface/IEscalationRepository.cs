using GovServe_Project.Models;
namespace GovServe_Project.Repository.Interface
{
	public interface IEscalationRepository
	{
		Task CreateAsync(Escalation escalation);
		Task<List<Escalation>> GetAllAsync();
		Task<int> GetEscalationCountAsync();
	}
}