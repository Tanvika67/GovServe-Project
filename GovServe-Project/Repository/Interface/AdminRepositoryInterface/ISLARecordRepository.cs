using GovServe_Project.Enum;
using GovServe_Project.Models.AdminModels;

namespace GovServe_Project.Repository.Interface.AdminRepositoryInterface
{
    public interface ISLARecordRepository
    {
        Task<IEnumerable<SLARecord>> GetAllAsync();
        Task<SLARecord?> GetByIdAsync(int id);
        Task<IEnumerable<SLARecord>> GetByStatusAsync(SLAStatus status);
        Task AddAsync(SLARecord record);
        void Update(SLARecord record);
        void Delete(SLARecord record);
        Task SaveAsync();
    }

}
