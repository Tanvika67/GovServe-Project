using GovServe_Project.Models;

namespace GovServe_Project.Repository.Interface
{
	public interface ICaseRepository
	{
		Task CreateAsync(Case model);
		Task<List<Case>> GetAllAsync();
		Task<Case> GetByIdAsync(int caseId);
		Task<List<Case>> GetByStatusAsync(string status);
		Task<List<Case>> GetEscalatedAsync();
		Task<List<Case>> GetSlaBreachedCasesAsync();

		Task UpdateAsync(Case caseData);

		Task<int> CountByStatusAsync(string status);
		Task<int> CountEscalatedAsync();

	}
}



