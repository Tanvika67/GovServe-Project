using GovServe_Project.Models.AdminModels;
using GovServe_Project.Models.SuperModels;
namespace GovServe_Project.Repository.Interface.SuperRepositoryInterface
{
	public interface IEscalationRepository
	{
		Task CreateAsync(Escalation escalation);
		Task<List<Escalation>> GetAllAsync();
		Task<SLARecords> GetByCaseIdAsync(int caseId);
		Task<int> GetEscalationCountAsync();
		Task<List<SLARecords>> GetSLABreachedCasesAsync();
	}
}