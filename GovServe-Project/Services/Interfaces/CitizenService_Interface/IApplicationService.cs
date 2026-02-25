using GovServe_Project.Models;
using GovServe_Project.DTOs;
using GovServe_Project.DTOs.CitizenDTO;

namespace GovServe_Project.Services.Interfaces.CitizenService_Interface
{
	public interface IApplicationService
	{
	
		Task<string> CreateApplicationAsync(CreateApplicationDTO dto);

	
		Task<List<ApplicationResponseDTO>> GetMyApplicationsAsync(int userId);

		
		Task<string> GetApplicationStatusAsync(int ApplicationId);

		
		Task<bool> DeleteApplicationAsync(int ApplicationId);
	}
}
