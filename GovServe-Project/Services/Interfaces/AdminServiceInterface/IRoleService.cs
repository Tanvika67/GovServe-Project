using GovServe_Project.DTOs;
using GovServe_Project.DTOs.AdminDTO;

namespace GovServe_Project.Services.Interfaces.AdminServiceInterface
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleResponseDto>> GetAllAsync();
        Task<RoleResponseDto> GetByIdAsync(int id);
        Task<RoleResponseDto> CreateAsync(RoleCreateDto dto);
        Task<RoleResponseDto> UpdateAsync(int id, RoleCreateDto dto);
        Task DeleteAsync(int id);
    }
}
