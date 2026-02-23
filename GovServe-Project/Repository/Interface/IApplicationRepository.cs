using GovServe_Project.Models;
namespace GovServe_Project.Repository.Interface
{
	public interface IApplicationRepository
	{

		Task CreateAsync(Application application);

		Task<Application> GetByIdAsync(int id);

		Task<List<Application>> GetByUserIdAsync(int userId);

		Task DeleteAsync(Application application);
	}

}
