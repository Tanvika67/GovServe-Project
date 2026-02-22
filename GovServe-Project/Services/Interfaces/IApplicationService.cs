using GovServe_Project.Models;
namespace GovServe_Project.Services.Interfaces
{
	public interface IApplicationService
	{
		Task CreateApplication(Application app);

		Task<List<Application>> MyApplications(int userId);

		Task<Application> ApplicationStatus(int id);

		Task DeleteApplication(int id);
	}
}
