using GovServe_Project.DTOs.Admin;
using GovServe_Project.DTOs.AdminDTO;

namespace GovServe_Project.Services.Interfaces.AdminServiceInterface
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentResponseDTO>> GetAllAsync();
        Task<DepartmentResponseDTO> GetByIdAsync(int id);
        Task<DepartmentResponseDTO> CreateAsync(DepartmentDTO dto);
        Task<DepartmentResponseDTO> UpdateAsync(int id, DepartmentDTO dto);
        Task<bool> DeleteAsync(int id);

        Task<int> GetTotalCountAsync();
    }
}