
using GovServe_Project.Models.AdminModels;

namespace GovServe_Project.Repository.Interface.AdminRepositoryInterface
{
    public interface IEligibilityRuleRepository
    {
        Task<IEnumerable<EligibilityRule>> GetAllAsync();
        Task<EligibilityRule?> GetByIdAsync(int id);
        Task AddAsync(EligibilityRule rule);
        Task UpdateAsync(EligibilityRule rule);
        Task DeleteAsync(EligibilityRule rule);
    }
}