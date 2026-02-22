using GovServe_Project.Models;
namespace GovServe_Project.Repository.Interface
{
	public interface IApplicationRepository
	{
		Task AddApplication(Application app);

		Task<List<Application>> GetByUser(int userId);

		Task<Application> GetById(int id);

		Task Delete(Application app);
	}

}
