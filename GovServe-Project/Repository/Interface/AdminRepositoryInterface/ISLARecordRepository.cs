using GovServe_Project.Enum;
using GovServe_Project.Models.AdminModels;

namespace GovServe_Project.Repository.Interface.AdminRepositoryInterface
{
    public interface ISLARecordRepository
    {
        Task<IEnumerable<SLARecords>> GetAllAsync();
        Task<SLARecords> GetByIdAsync(int id);
        Task<IEnumerable<SLARecords>> GetByStatusAsync(SLAStatus status);
        Task AddAsync(SLARecords record);
        void Update(SLARecords record);
        void Delete(SLARecords record);
        Task SaveAsync();
    }

}
