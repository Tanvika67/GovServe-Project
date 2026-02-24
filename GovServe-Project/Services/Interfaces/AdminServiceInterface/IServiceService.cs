using GovServe_Project.DTOs.Admin;

namespace GovServe_Project.Services.Interfaces.AdminServiceInterface
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceResponseDTO>> GetAllAsync();
        Task<ServiceResponseDTO> GetByIdAsync(int id);
        Task<ServiceResponseDTO> CreateAsync(ServiceDTO dto);
        Task<ServiceResponseDTO> UpdateAsync(int id, ServiceDTO dto);
        Task DeleteAsync(int id);
    }
}