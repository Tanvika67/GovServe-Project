using GovServe_Project.Models;
using GovServe_Project.DTOs.OfficerDTO;


namespace GovServe_Project.Repository.Interface
{
	public interface ICaseRepository
	{
		Task<IEnumerable<Case>> GetAllAsync();
		Task<IEnumerable<Case>> GetByStatusAsync(string status);
		Task<Case> GetByIdAsync(int id);

		Task AddAsync(Case c);
		void Update(Case c);

		Task SaveAsync();

		//officer work
		Task<List<Case>> GetAssignedCases(int officerId);
		Task<Case?> GetCaseById(int caseId);

		Task UpdateCase(Case caseObj);
		Task<DashboardCountcs> GetDashboardCountsAsync(int departmentId);

		Task<string> Reject(int caseId, string reason);
	}
}



