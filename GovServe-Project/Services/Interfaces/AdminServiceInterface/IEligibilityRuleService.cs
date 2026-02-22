using GovServe_Project.DTOs.Admin;

namespace GovServe_Project.Services.Interfaces.AdminServiceInterface
{
    public interface IEligibilityRuleService
    {
        Task<IEnumerable<EligibilityRuleResponseDTO>> GetAllAsync();
        Task<EligibilityRuleResponseDTO> GetByIdAsync(int id);
        Task<EligibilityRuleResponseDTO> CreateAsync(EligibilityRuleDTO dto);
        Task<EligibilityRuleResponseDTO> UpdateAsync(int id, EligibilityRuleDTO dto);
        Task DeleteAsync(int id);
    }
}
