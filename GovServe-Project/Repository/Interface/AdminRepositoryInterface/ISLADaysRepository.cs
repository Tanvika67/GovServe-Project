using GovServe_Project.Models.AdminModels;

namespace GovServe_Project.Repository.Interface.AdminRepositoryInterface
{
    public interface ISLADayRepository
    {
        Task<IEnumerable<SLADays>> GetAllAsync();
        Task<SLADays?> GetByIdAsync(int id);
        Task<SLADays?> GetByServiceAndRoleAsync(int serviceId, int roleId);
        Task AddAsync(SLADays slaDay);
        Task UpdateAsync(SLADays slaDay);
        Task DeleteAsync(SLADays slaDay);
    }
}