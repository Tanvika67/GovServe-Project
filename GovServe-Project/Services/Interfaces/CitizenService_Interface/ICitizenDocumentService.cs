using GovServe_Project.DTOs;
using GovServe_Project.DTOs.CitizenDTO;
using GovServe_Project.Models;
using GovServe_Project.Models.AdminModels;
using GovServe_Project.Models.CitizenModels;
namespace GovServe_Project.Services.Interfaces.CitizenService_Interface
{
	public interface ICitizenDocumentService
	{
		Task<bool> UploadDocumentAsync(UploadCitizenDocumentDTO model);

		Task<List<UploadCitizenDocumentResponseDTO>> GetMyAllDocuments(int userId);

		Task<List<UploadCitizenDocumentResponseDTO>> GetDocumentsByApplicationId(int applicationId);
		Task<List<RequiredDocument>> GetRequiredDocumentsByService(int serviceId);
		Task<string> GetDocumentStatusAsync(int documentId);
		Task<bool> UpdateDocumentAsync(int citizenDocumentId, UploadCitizenDocumentDTO model);
		Task<bool> DeleteDocumentAsync(int documentId);

		Task<string> ApproveDocument(int CitizenDocumentID);

		Task<string> RejectDocument(int CitizenDocumentID);
	}
}
