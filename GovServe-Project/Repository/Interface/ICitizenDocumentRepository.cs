using GovServe_Project.Models;

namespace GovServe_Project.Repository.Interface
{
	public interface ICitizenDocumentRepository
	{
		Task Upload(CitizenDocument doc);

		Task<List<CitizenDocument>> GetByApplication(int applicationId);

		Task<CitizenDocument> GetById(int id);

		Task Update(CitizenDocument doc);

		Task Delete(CitizenDocument doc);
	}
}
