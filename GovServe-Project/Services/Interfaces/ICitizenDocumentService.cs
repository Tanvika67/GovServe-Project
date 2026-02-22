using GovServe_Project.Models;
namespace GovServe_Project.Services.Interfaces
{
	public interface ICitizenDocumentService
	{
		Task Upload(CitizenDocument doc);

		Task<List<CitizenDocument>> MyDocuments(int applicationId);

		Task ReUpload(int id, string fileName);

		Task Delete(int id);
	}
}
