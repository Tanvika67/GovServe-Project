using GovServe_Project.DTOs.Admin;
using GovServe_Project.Exceptions;
using GovServe_Project.Models.AdminModels;
using GovServe_Project.Repository.Interface.AdminRepositoryInterface;
using GovServe_Project.Services.Interfaces.AdminServiceInterface;

namespace GovServe_Project.Services.Service_Implementation.AdminServiceImplementation
{
    public class RequiredDocumentService : IRequiredDocumentService
    {
        private readonly IRequiredDocumentRepository _repository;

        public RequiredDocumentService(IRequiredDocumentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RequiredDocumentResponseDTO>> GetAllAsync()
        {
            var documents = await _repository.GetAllAsync();

            return documents.Select(d => new RequiredDocumentResponseDTO
            {
                DocumentID = d.DocumentID,
                ServiceID = d.ServiceID,
                DocumentName = d.DocumentName,
                Mandatory = d.Mandatory
            });
        }

        public async Task<RequiredDocumentResponseDTO> GetByIdAsync(int id)
        {
            var document = await _repository.GetByIdAsync(id);

            if (document == null)
                throw new NotFoundException("Required document not found.");

            return new RequiredDocumentResponseDTO
            {
                DocumentID = document.DocumentID,
                ServiceID = document.ServiceID,
                DocumentName = document.DocumentName,
                Mandatory = document.Mandatory
            };
        }

        public async Task<RequiredDocumentResponseDTO> CreateAsync(RequiredDocumentDTO dto)
        {
            var document = new RequiredDocument
            {
                ServiceID = dto.ServiceID,
                DocumentName = dto.DocumentName,
                Mandatory = dto.Mandatory
            };

            await _repository.AddAsync(document);

            return await GetByIdAsync(document.DocumentID);
        }

        public async Task<RequiredDocumentResponseDTO> UpdateAsync(int id, RequiredDocumentDTO dto)
        {
            var document = await _repository.GetByIdAsync(id);

            if (document == null)
                throw new NotFoundException("Required document not found.");

            document.ServiceID = dto.ServiceID;
            document.DocumentName = dto.DocumentName;
            document.Mandatory = dto.Mandatory;

            await _repository.UpdateAsync(document);

            return await GetByIdAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            var document = await _repository.GetByIdAsync(id);

            if (document == null)
                throw new NotFoundException("Required document not found.");

            await _repository.DeleteAsync(document);
        }
    }
}
