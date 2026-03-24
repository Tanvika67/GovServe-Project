using GovServe_Project.DTOs.Admin;
using GovServe_Project.Enum;
using GovServe_Project.Exceptions;
using GovServe_Project.Models.AdminModels;
using GovServe_Project.Repository.Interface.AdminRepositoryInterface;
using GovServe_Project.Repository.Repository_Implentation.AdminRepositoryImplementation;
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
                ServiceName = d.Service?.ServiceName ?? "",
                DocumentName = d.DocumentName,
                Mandatory = d.Mandatory ? "Yes" : "No"
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
                ServiceName = document.Service?.ServiceName ?? "",
                DocumentName = document.DocumentName,
                Mandatory = document.Mandatory ? "Yes" : "No"
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


        //Search Required Document by service name
        public async Task<IEnumerable<RequiredDocumentResponseDTO>> SearchByServiceNameAsync(string serviceName)
        {
            var documents = await _repository.GetByServiceNameAsync(serviceName);

            if (!documents.Any())
                throw new NotFoundException("No eligibility rules found for this service.");

            return documents.Select(d => new RequiredDocumentResponseDTO
            {
                DocumentID = d.DocumentID,
                ServiceName = d.Service?.ServiceName ?? "",
                DocumentName = d.DocumentName,
                Mandatory = d.Mandatory ? "Yes" : "No"
            });

        }
    }
}
