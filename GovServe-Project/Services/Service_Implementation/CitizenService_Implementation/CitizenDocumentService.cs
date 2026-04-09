using GovServe_Project.Controllers.CitizenController;
using GovServe_Project.DTOs;
using GovServe_Project.DTOs.CitizenDTO;
using GovServe_Project.Exceptions;
using GovServe_Project.Models;
using GovServe_Project.Models.AdminModels;
using GovServe_Project.Models.CitizenModels;
using GovServe_Project.Repository.Interface;
using GovServe_Project.Repository.Interface.CitizenRepository_Interface;
using GovServe_Project.Services.Interfaces;
using GovServe_Project.Services.Interfaces.CitizenService_Interface;
using GovServe_Project.Services.Interfaces.CitizenService_Interface;
using static NuGet.Packaging.PackagingConstants;

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
				throw new BadRequestException("No file was uploaded."); // Generic bool aivaji Exception throw kela

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
			    UserId = model.UserId,
				ApplicationID = model.ApplicationID,
				DocumentID = model.DocumentID,
				URI = "uploads/" + fileName,
				VerificationStatus = "Submitted",
				UploadedDate = DateTime.Now
			};

			try
			{
				await _repository.AddAsync(document);
			}
			catch (Exception)
			{
				throw new BadRequestException("Invalid Application ID or Document ID provided.");
			}

			return true;
		}

		// Get My Documents
		public async Task<List<UploadCitizenDocumentResponseDTO>> GetMyAllDocuments(int userId)
		{
			var documents = await _repository.GetMyAllDocuments(userId);

			if (documents == null || !documents.Any())
				throw new NotFoundException($"No documents found for User ID {userId}.");

			return documents.Select(d => new UploadCitizenDocumentResponseDTO
			{
			    ApplicationID = d.ApplicationID,
				DocumentName = d.RequiredDocument?.DocumentName ?? "Unknown Document",
				DocumentUrl = d.URI,
				UploadedDate = d.UploadedDate,
				VerificationStatus = d.VerificationStatus
			}).ToList();
		}


		// Get Documents by Application ID
		public async Task<List<UploadCitizenDocumentResponseDTO>> GetDocumentsByApplicationId(int applicationId)
		{
			var documents = await _repository.GetDocumentsByApplicationId(applicationId);

			if (documents == null || !documents.Any())
				throw new NotFoundException($"No documents found for Application ID {applicationId}.");

			return documents.Select(d => new UploadCitizenDocumentResponseDTO
			{
			    UserId = d.UserId,
				CitizenDocumentID = d.CitizenDocumentID,
				ApplicationID = d.ApplicationID,
				UploadedDate = d.UploadedDate,
				VerificationStatus = d.VerificationStatus
			}).ToList();
		}

		//GET Required Documents
		public async Task<List<RequiredDocument>> GetRequiredDocumentsByService(int serviceId)
		{
			var requiredDocs = await _repository.GetRequiredDocumentsByService(serviceId);

			if (requiredDocs == null || !requiredDocs.Any())
				throw new NotFoundException($"No required documents found for Service ID {serviceId}.");

			return requiredDocs;
		}

		// View Document Status
		public async Task<string> GetDocumentStatusAsync(int documentId)
		{
			var doc = await _repository.GetByIdAsync(documentId);

			if (doc == null)
				throw new NotFoundException($"Document with ID {documentId} not found.");

			return doc.VerificationStatus;
		}

		// Update Document (Replace existing file)
		public async Task<bool> UpdateDocumentAsync(int citizenDocumentId, UploadCitizenDocumentDTO model)
		{
			// Fetch existing record
			var existingDoc = await _repository.GetByIdAsync(citizenDocumentId);

			if (existingDoc == null)
				throw new NotFoundException($"Document with ID {citizenDocumentId} not found.");

			// Validate new file input
			if (model.URI == null || model.URI.Length == 0)
				throw new BadRequestException("No new file provided for update.");

			//  Delete the old physical file from server
			var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingDoc.URI);
			if (File.Exists(oldFilePath))
			{
				File.Delete(oldFilePath);
			}

			// Save the new physical file
			var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
			var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.URI.FileName);
			var filePath = Path.Combine(folderPath, fileName);

			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await model.URI.CopyToAsync(stream);
			}

			//  Update properties
			existingDoc.URI = "uploads/" + fileName;
			existingDoc.DocumentID = model.DocumentID;
			existingDoc.VerificationStatus = "Pending"; // Reset status for re-verification
			existingDoc.UploadedDate = DateTime.Now;

			try
			{
				await _repository.Update(existingDoc);
				return true;
			}
			catch (Exception)
			{
				throw new BadRequestException("Database update failed while saving new document details.");
			}
		}

		// Delete Document
		public async Task<bool> DeleteDocumentAsync(int documentId)
		{
			var doc = await _repository.GetByIdAsync(documentId);

			if (doc == null)
				throw new NotFoundException($"Cannot delete. Document with ID {documentId} not found.");

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