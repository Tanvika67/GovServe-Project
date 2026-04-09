using GovServe_Project.Models;
using GovServe_Project.Models.AdminModels;
using GovServe_Project.Models.CitizenModels;

namespace GovServe_Project.Repository.Interface.CitizenRepository_Interface

{

	public interface ICitizenDocumentRepository

	{

		Task AddAsync(CitizenDocument document);

		Task<List<CitizenDocument>> GetMyAllDocuments(int userId);

		Task<List<CitizenDocument>> GetDocumentsByApplicationId(int applicationId);

		//GET RequiredDocument 
		Task<List<RequiredDocument>> GetRequiredDocumentsByService(int serviceId);
		Task<CitizenDocument> GetByIdAsync(int id);

		Task DeleteAsync(CitizenDocument document);

		//Task<CitizenDocument> GetById(int CitizenDocumentID);

		Task Update(CitizenDocument document);

	}

}

