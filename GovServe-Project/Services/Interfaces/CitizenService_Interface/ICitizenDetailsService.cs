using GovServe_Project.Services.Citizen;
using GovServe_Project.Models.CitizenModels;
using System.Threading.Tasks;
using GovServe_Project.DTOs.CitizenDTO;

namespace GovServe_Project.Services.Citizen
{
	public interface ICitizenDetailsService
	{
		Task<CitizenDetails> CreatePersonalDetailsAsync(CreateCitizenDetailsDTO dto);
		Task<CitizenDetails> UpdatePersonalDetailsAsync(UpdateCitizenDetailsDTO dto);
		Task<CitizenDetails> GetByApplicationIdAsync(int applicationId);
		Task<CitizenDetails> GetByIdAsync(int personalDetailId);
	}
}