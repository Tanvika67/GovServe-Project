using GovServe_Project.DTOs.AdminDTO;

namespace GovServe_Project.Services.Interfaces.AdminServiceInterface
{
    public interface ISLADayService
    {
        Task<IEnumerable<SLADayResponseDto>> GetAllAsync();
        Task<SLADayResponseDto> GetByIdAsync(int id);
        Task<SLADayResponseDto> CreateAsync(SLADayCreateDto dto);
        Task UpdateAsync(int id, SLADayCreateDto dto);
        Task DeleteAsync(int id);
    }
}
