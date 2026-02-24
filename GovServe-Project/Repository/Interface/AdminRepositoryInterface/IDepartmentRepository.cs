
using GovServe_Project.Models.AdminModels;

namespace GovServe_Project.Repository.Interface.AdminRepositoryInterface
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllAsync();
        Task<Department?> GetByIdAsync(int id);
        Task<Department> AddAsync(Department department);
        Task<Department?> UpdateAsync(Department department);
        Task<bool> DeleteAsync(int id);
    }
}