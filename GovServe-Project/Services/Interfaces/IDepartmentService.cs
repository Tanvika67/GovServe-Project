using GovServe_Project.DTOs;
using GovServe_Project.Models;

namespace GovServe_Project.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetAllAsync();
        Task<Department> GetByIdAsync(int id);
        Task<Department> CreateAsync(DepartmentDTO dto);
        Task<Department> UpdateAsync(int id, DepartmentDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}