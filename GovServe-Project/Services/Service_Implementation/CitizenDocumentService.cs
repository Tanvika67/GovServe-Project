using GovServe_Project.Models;
using GovServe_Project.Repository.Interface;
using GovServe_Project.Services.Interfaces;

namespace GovServe_Project.Services.Service_Implementation
{
	public class CitizenDocumentService : ICitizenDocumentService
	{
		private readonly ICitizenDocumentRepository _repository;

		// Inject Repository Layer
		public CitizenDocumentService(ICitizenDocumentRepository repository)
		{
			_repository = repository;
		}

		// Upload document logic
		public async Task Upload(CitizenDocument doc)
		{
			doc.UploadedDate = DateTime.Now;
			doc.VerificationStatus = "Pending";

			await _repository.Upload(doc);
		}

		// Get documents application wise
		public async Task<List<CitizenDocument>> MyDocuments(int applicationId)
		{
			return await _repository.GetByApplication(applicationId);
		}

		// Re-upload logic
		public async Task ReUpload(int id, string fileName)
		{
			var data = await _repository.GetById(id);

			if (data != null)
			{
				data.FilePath = fileName;
				data.VerificationStatus = "Pending";

				await _repository.Update(data);
			}
		}

		// Delete logic
		public async Task Delete(int id)
		{
			var data = await _repository.GetById(id);

			if (data != null)
			{
				await _repository.Delete(data);
			}
		}
	}
}