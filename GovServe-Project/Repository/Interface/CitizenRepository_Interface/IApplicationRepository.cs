using GovServe_Project.DTOs.OfficerDTO;
using GovServe_Project.Models;
using GovServe_Project.Models.CitizenModels;
using GovServe_Project.Models.SuperModels;
namespace GovServe_Project.Repository.Interface.CitizenRepository_Interface
{
	public interface IApplicationRepository
	{
	
		Task CreateAsync(Application application);

		Task<Application> GetByIdAsync(int ApplicationId);
		Task<Application> GetApplicationWithDocuments(int applicationId);
		Task<List<Application>> GetByUserIdAsync(int userId);

		Task DeleteAsync(Application ApplicationId);


		Task UpdateAsync(Application application);

		

		//Task DeleteAsync(Application application);
		Task<ApplicationDetails> GetApplicationDetails(int ApplicationId);
	}

}
