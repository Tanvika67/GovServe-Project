using GovServe_Project.DTOs.Admin;
using GovServe_Project.DTOs.AdminDTO;

namespace GovServe_Project.Services.Interfaces.AdminServiceInterface
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceResponseDTO>> GetAllAsync();
        Task<ServiceResponseDTO> GetByIdAsync(int id);
        Task<IEnumerable<ServiceResponseDTO>> GetActiveAsync();

        Task<ServiceResponseDTO> CreateAsync(ServiceDTO dto);
        Task<ServiceResponseDTO> UpdateAsync(int id, ServiceDTO dto);
        Task DeleteAsync(int id);

        Task<IEnumerable<ServiceResponseDTO>> SearchByDepartmentAsync(string departmentName);

        Task<int> GetServiceCountAsync();
        Task<int> GetActiveServiceCountAsync();
        Task<object> GetActiveVsTotalAsync();
    }
}