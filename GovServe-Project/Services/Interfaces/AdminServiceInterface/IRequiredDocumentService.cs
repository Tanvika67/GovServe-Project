using GovServe_Project.DTOs.Admin;

namespace GovServe_Project.Services.Interfaces.AdminServiceInterface
{
    public interface IRequiredDocumentService
    {
        Task<IEnumerable<RequiredDocumentResponseDTO>> GetAllAsync();
        Task<RequiredDocumentResponseDTO> GetByIdAsync(int id);
        Task<RequiredDocumentResponseDTO> CreateAsync(RequiredDocumentDTO dto);
        Task<RequiredDocumentResponseDTO> UpdateAsync(int id, RequiredDocumentDTO dto);
        Task DeleteAsync(int id);
    }
}