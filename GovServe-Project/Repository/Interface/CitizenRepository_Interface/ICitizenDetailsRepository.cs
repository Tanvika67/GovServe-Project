using GovServe_Project.Models.CitizenModels;
using System.Threading.Tasks;

namespace GovServe_Project.Repositories.Citizen
{
	public interface ICitizenDetailsRepository
	{
		Task<CitizenDetails> CreateAsync(CitizenDetails details);
		Task<CitizenDetails> UpdateAsync(CitizenDetails details);
		Task<CitizenDetails> GetByApplicationIdAsync(int applicationId);
		Task<CitizenDetails> GetByIdAsync(int personalDetailId);
	}
}