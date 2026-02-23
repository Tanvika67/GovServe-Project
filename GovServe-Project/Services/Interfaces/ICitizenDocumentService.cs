using GovServe_Project.Models;
namespace GovServe_Project.Services.Interfaces
{
	public interface ICitizenDocumentService
	{
		Task Upload(CitizenDocument doc);

		Task<List<CitizenDocument>> MyDocuments(int applicationId);

		Task Delete(int id);
	}
}
