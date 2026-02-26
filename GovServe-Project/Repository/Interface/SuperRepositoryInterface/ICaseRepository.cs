using GovServe_Project.Models.SuperModels;

namespace GovServe_Project.Repository.Interface.SuperRepositoryInterface
{
	public interface ICaseRepository
	{
		Task<IEnumerable<Case>> GetAllAsync();
		Task<IEnumerable<Case>> GetByStatusAsync(string status);
		Task<Case> GetByIdAsync(int id);
		Task<List<Case>> GetSLABreachedCasesAsync();

		Task AddAsync(Case c);
		void Update(Case c);

		Task SaveAsync();
	}
}



