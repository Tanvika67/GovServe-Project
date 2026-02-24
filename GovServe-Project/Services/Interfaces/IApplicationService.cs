using GovServe_Project.DTOs;
using GovServe_Project.Models;
namespace GovServe_Project.Services.Interfaces
{
	public interface IApplicationService
	{

		Task<string> CreateApplicationAsync(CreateApplicationDTO dto);


		Task<List<ApplicationResponseDTO>> GetMyApplicationsAsync(int userId);


		Task<string> GetApplicationStatusAsync(int applicationId);


		Task<bool> DeleteApplicationAsync(int applicationId);
	}
}
