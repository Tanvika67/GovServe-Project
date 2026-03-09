using GovServe_Project.DTOs;
using GovServe_Project.Models;
using GovServe_Project.DTOs.CitizenDTO;
namespace GovServe_Project.Services.Interfaces.CitizenService_Interface
{
	public interface ICitizenDocumentService
	{
		Task<string> UploadDocumentAsync(UploadCitizenDocumentDTO dto);

		Task<string> GetDocumentStatusAsync(int documentId);

		Task<bool> DeleteDocumentAsync(int documentId);

		Task<string> ApproveDocument(int CitizenDocumentID);
		Task<string> RejectDocument(int CitizenDocumentID);
	}
}
