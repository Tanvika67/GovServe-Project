using GovServe_Project.DTOs;
using GovServe_Project.Models;
using GovServe_Project.Repository.Interface;
using GovServe_Project.Services.Interfaces;
using static NuGet.Packaging.PackagingConstants;
using GovServe_Project.Controllers.CitizenController;
using GovServe_Project.Services.Interfaces.CitizenService_Interface;
using GovServe_Project.Repository.Interface.CitizenRepository_Interface;
using GovServe_Project.Services.Interfaces.CitizenService_Interface;
using GovServe_Project.DTOs.CitizenDTO;
using GovServe_Project.Models.CitizenModels;

namespace GovServe_Project.Services.Service_Implementation.CitizenService_Implementation
{
	public class CitizenDocumentService : ICitizenDocumentService
	{
		private readonly ICitizenDocumentRepository _repository;
		private readonly IWebHostEnvironment _env;

		public CitizenDocumentService(ICitizenDocumentRepository repository,
									  IWebHostEnvironment env)
		{
			_repository = repository;
			_env = env;
		}

		// Upload Document
		public async Task<bool> UploadDocumentAsync(UploadCitizenDocumentDTO model)
		{
			if (model.URI == null || model.URI.Length == 0)
				return false;

			var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

			if (!Directory.Exists(folderPath))
				Directory.CreateDirectory(folderPath);

			var fileName = Guid.NewGuid().ToString() +
						   Path.GetExtension(model.URI.FileName);

			var filePath = Path.Combine(folderPath, fileName);

			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await model.URI.CopyToAsync(stream);
			}

			var document = new CitizenDocument
			{
				ApplicationID = model.ApplicationID,
				DocumentID = model.DocumentID,
				//DocumentName = model.DocumentName,
				URI = "uploads/" + fileName,
				VerificationStatus = "Submitted",
				UploadedDate = DateTime.Now
			};

			await _repository.AddAsync(document);

			return true;
		}

		public async Task<List<UploadCitizenDocumentResponseDTO>> GetMyAllDocuments(int userId)
		{
			var documents = await _repository.GetMyAllDocuments(userId);

			return documents.Select(d => new UploadCitizenDocumentResponseDTO
			{
				CitizenDocumentID = d.CitizenDocumentID,
				ApplicationID = d.ApplicationID,
			
				//URI = "uploads/" + fileName,
				UploadedDate = d.UploadedDate,
				VerificationStatus = d.VerificationStatus
			}).ToList();
		}

		public async Task<List<UploadCitizenDocumentResponseDTO>> GetDocumentsByApplicationId(int applicationId)
		{
			var documents = await _repository.GetDocumentsByApplicationId(applicationId);

			return documents.Select(d => new UploadCitizenDocumentResponseDTO
			{
				CitizenDocumentID = d.CitizenDocumentID,
				ApplicationID = d.ApplicationID,
				//DocumentName = d.DocumentName,
				//URI = "uploads/" + fileName,
				UploadedDate = d.UploadedDate,
				VerificationStatus = d.VerificationStatus
			}).ToList();
		}


		// View Document Status
		public async Task<string> GetDocumentStatusAsync(int documentId)
		{
			var doc = await _repository.GetByIdAsync(documentId);

			if (doc == null)
				return null;

			return doc.VerificationStatus;
		}

		// Delete Document
		public async Task<bool> DeleteDocumentAsync(int documentId)
		{
			var doc = await _repository.GetByIdAsync(documentId);

			if (doc == null)
				return false;

			await _repository.DeleteAsync(doc);

			return true;
		}

		public async Task<string> ApproveDocument(int CitizenDocumentID)
		{
			var doc = await _repository.GetByIdAsync(CitizenDocumentID);

			if (doc == null)
				return "Document Not Found";

			doc.VerificationStatus = "Approved";

			await _repository.Update(doc);

			return "Document Approved Successfully";
		}

		public async Task<string> RejectDocument(int CitizenDocumentID)
		{
			var doc = await _repository.GetByIdAsync(CitizenDocumentID);

			if (doc == null)
				return "Document Not Found";

			doc.VerificationStatus = "Rejected";

			await _repository.Update(doc);

			return "Document Rejected Successfully";
		}
	}
}
