using GovServe_Project.DTOs.AdminDTO;

namespace GovServe_Project.Services.Interfaces.AdminServiceInterface
{
    public interface ISLARecordService
    {
        Task<IEnumerable<SLARecordResponseDTO>> GetAllAsync();
        Task<SLARecordResponseDTO> GetByIdAsync(int id);
        Task<SLARecordResponseDTO> CreateAsync(SLARecordDTO dto);
        Task <SLARecordResponseDTO>UpdateAsync(int id, SLARecordDTO dto);
        Task DeleteAsync(int id);
    }
}
