
using GovServe_Project.Models.AdminModels;

namespace GovServe_Project.Repository.Interface.AdminRepositoryInterface
{
    public interface IServiceRepository
    {
        Task<IEnumerable<Service>> GetAllAsync();
        Task<Service?> GetByIdAsync(int id);
        Task AddAsync(Service service);
        Task UpdateAsync(Service service);
        Task DeleteAsync(Service service);
    }
}