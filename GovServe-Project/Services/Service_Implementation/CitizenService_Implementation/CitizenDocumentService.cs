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
		public async Task<string> UploadDocumentAsync(UploadCitizenDocumentDTO dto)
		{
			// Validate input
			if (dto is null) return "Invalid request.";
			if (dto.URI is null || dto.URI.Length == 0) return "File not selected.";
			if (dto.ApplicationID <= 0) return "Invalid ApplicationID.";
			if (string.IsNullOrWhiteSpace(dto.DocumentName)) dto.DocumentName = "Document";

			// Always available base path (do NOT rely on WebRootPath unless you guarantee wwwroot exists)
			var contentRoot = _env.ContentRootPath;
			if (string.IsNullOrWhiteSpace(contentRoot))
				throw new InvalidOperationException("ContentRootPath is not available.");

			// Build target folder: {ContentRoot}/Uploads/CitizenDocuments/{ApplicationID}
			var baseFolder = "Uploads";
			var entityFolder = "CitizenDocuments";
			var targetDir = Path.Combine(contentRoot, baseFolder, entityFolder, dto.ApplicationID.ToString());

			// Ensure directory exists
			Directory.CreateDirectory(targetDir);

			// Unique & safe file name (keeps extension)
			var ext = Path.GetExtension(dto.URI.FileName);
			// You may whitelist certain extensions if needed
			var uniqueName = $"{Guid.NewGuid():N}{ext}";

			// Full system path to save
			var savePath = Path.Combine(targetDir, uniqueName);

			// Save file to disk
			using (var stream = new FileStream(savePath, FileMode.Create, FileAccess.Write, FileShare.None))
			{
				await dto.URI.CopyToAsync(stream);
			}

			// Persist a relative path (portable); or store only file name if you prefer
			var relativePath = Path.Combine(baseFolder, entityFolder, dto.ApplicationID.ToString(), uniqueName)
									.Replace('\\', '/');

			// Create entity
			var document = new CitizenDocument
			{
				ApplicationID = dto.ApplicationID,
				DocumentName = dto.DocumentName,
				// If you want only the file name, set URI = uniqueName.
				// If you want a relative path to reconstruct a URL later, store relativePath.
				URI = relativePath,
				UploadedDate = DateTime.UtcNow,
				VerificationStatus = "Uploaded"
			};

			await _repository.AddAsync(document);

			return "Document Uploaded Successfully";
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
