using GovServe_Project.Models;
using GovServe_Project.Models.CitizenModels;
namespace GovServe_Project.Repository.Interface.CitizenRepository_Interface
{
	public interface IApplicationRepository
	{
	
		Task CreateAsync(Application application);

		Task<Application> GetByIdAsync(int ApplicationId);

		Task<List<Application>> GetByUserIdAsync(int userId);

		Task DeleteAsync(Application ApplicationId);
	}

}
