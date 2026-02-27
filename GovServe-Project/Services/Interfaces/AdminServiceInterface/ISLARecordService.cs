using GovServe_Project.DTOs.AdminDTO;

namespace GovServe_Project.Services.Interfaces.AdminServiceInterface
{
    public interface ISLARecordService
    {
        Task<IEnumerable<SLARecordResponseDto>> GetAllAsync();
        Task<SLARecordResponseDto> GetByIdAsync(int id);
        Task<IEnumerable<SLARecordResponseDto>> GetBreachedCasesAsync();
        Task<IEnumerable<SLARecordResponseDto>> GetOnTimeCasesAsync();
        Task<SLARecordResponseDto> CreateAsync(SLARecordCreateDto dto);
        Task DeleteAsync(int id);
    }
}
