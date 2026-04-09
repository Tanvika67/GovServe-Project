using GovServe_Project.Controllers.CitizenController;
using GovServe_Project.DTOs;
using GovServe_Project.DTOs.CitizenDTO;
using GovServe_Project.DTOs.OfficerDTO;
using GovServe_Project.Models;
using GovServe_Project.Models.CitizenModels;
using GovServe_Project.Models.SuperModels;

namespace GovServe_Project.Services.Interfaces.CitizenService_Interface
{
	public interface IApplicationService
	{

		Task<Application> CreateApplicationAsync(CreateApplicationDTO dto);

	
		Task<List<ApplicationResponseDTO>> GetMyApplicationsAsync(int userId);

		Task<string> GetApplicationStatusAsync(int ApplicationId);
		
		Task<bool> DeleteApplicationAsync(int ApplicationId);

		Task<ApplicationDetailsDTO> GetApplicationDetailsAsync(int id);
		Task<List<ApplicationResponseDTO>> GetAllApplicationsAsync();
	}
}
